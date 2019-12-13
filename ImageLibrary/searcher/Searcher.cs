using System;
using System.Collections.Generic;
using System.Xml;

namespace ImageLibrary
{
    /// <summary>
    /// Клас ШУКАЧ для роботи з пошуковим запитом
    /// </summary>
    public class Searcher
    {
        /// <summary>
        /// Інтерфейс для роботи з базою даних
        /// </summary>
        private IDataBase m_Data;

        /// <summary>
        /// Запис ХМЛ файлів
        /// </summary>
        private XmlWriter XMLTableShema;

        //Папка ХМЛ файлів
        private string m_FolderSaveXML;

        /// <summary>
        /// Результат пошуку слова (слово, список результатів)
        /// </summary>
        private Dictionary<string, List<SearchRowData>> SearchWordsResultAll;

        //Ранжовані результати 
        private List<SearchRowData> WordsShema;  //Список слів зі схеми
        private List<SearchRowData> WordsDirect; //Список прямого співпадіння
        private List<SearchRowData> WordsEntry;  //Список входження

        //Списки ІД Схеми
        private List<string> WordsShemaID;       //Список ІД схеми слів зі схеми
        private List<string> WordsDirectShemaID; //Список ІД слів із прямого співпадіння
        private List<string> WordsEntryShemaID;  //Список ІД слів із входження

        //Треки (переходи)
        private List<Track> TracksShemaShema;           //Схема - схема
        private List<Track> TracksShemaDataDirect;      //Схема та даними прямого співпадіння
        private List<Track> TracksShemaDataEntry;       //Схема та даними входження
        private List<Track> TracksDataDirectDataDirect; //Даними прямого співпадіння та даними прямого співпадіння
        private List<Track> TracksDataDirectDataEntry;  //Даними прямого співпадіння та даними входження
        private List<Track> TracksDataEntryDataEntry;   //Даними входження та даними входження

        //Первинний список дерева пошуку (ІД схеми, обєкт)
        private Dictionary<string, TreeSearch> listTreeSearch;

        //private Dictionary<string, string> listUnion;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Data">Підключення до бази даних</param>
        /// <param name="query">Пошуковий запит</param>
        /// <param name="FolderSaveXML">Папка для звітів</param>
        public Searcher(IDataBase Data, string query, string FolderSaveXML)
        {
            m_Data = Data;

            Query = query.ToLower();

            m_FolderSaveXML = FolderSaveXML;

            Words = new string[] { };
        }

        /// <summary>
        /// Пошуковий запит
        /// </summary>
        private string Query { get; set; }

        /// <summary>
        /// Масив слів
        /// </summary>
        private string[] Words { get; set; }

        /// <summary>
        /// Аналіз запиту
        /// </summary>
        public void Analize()
        {
            SplitQueryToWords();
            RemoveDuplicates();
            AnalizeWords();

            if (Words.Length > 0)
            {
                SearchWordsResultAll = new Dictionary<string, List<SearchRowData>>();

                foreach (string word in Words)
                {
                    List<SearchRowData> listItemResult = SearchWord(word);

                    if (listItemResult != null)
                        SearchWordsResultAll.Add(word, listItemResult);
                }

                if (SearchWordsResultAll.Count > 0)
                {
                    RangeWordsResult();
                    GetAllTracks();
                    CreateFirstObjectTree();
                    WorkTracks();

                    SaveTree();
                }
            }
        }

        #region 5.



        #endregion

        #region 4. Запис дерева пошуку

        /// <summary>
        /// Збереження дерева пошуку в ХМЛ файл
        /// </summary>
        private void SaveTree()
        {
            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "result_tree.xml", XMLSettings);
            XMLTableShema.WriteComment(DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("root");

            SaveSearchData();
            SaveTracks();

            XMLTableShema.WriteStartElement("tree");

            foreach (KeyValuePair<string, TreeSearch> item in listTreeSearch)
                SaveTreeElement(item.Value);

            XMLTableShema.WriteEndElement();

            XMLTableShema.WriteEndElement();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Запис одного елементу пошуку
        /// </summary>
        /// <param name="SearchRowDataItem">Елемент пошуку який треба записати</param>
        private void SaveItemSearchData(SearchRowData SearchRowDataItem)
        {
            XMLTableShema.WriteStartElement("row");
            XMLTableShema.WriteAttributeString("ShemaID", SearchRowDataItem.ShemaID);
            XMLTableShema.WriteAttributeString("DataID", SearchRowDataItem.DataID);
            XMLTableShema.WriteAttributeString("Name", SearchRowDataItem.Name);
            XMLTableShema.WriteAttributeString("IsShema", SearchRowDataItem.IsShema.ToString());
            XMLTableShema.WriteAttributeString("IsDirect", SearchRowDataItem.IsDirect.ToString());
            XMLTableShema.WriteAttributeString("IsEntry", SearchRowDataItem.IsEntry.ToString());
            //XMLTableShema.WriteAttributeString("Query", SearchRowDataItem.Query);
            XMLTableShema.WriteEndElement();
        }

        /// <summary>
        /// Записує результати пошуку
        /// </summary>
        private void SaveSearchData()
        {
            if (SearchWordsResultAll.Count > 0)
            {
                XMLTableShema.WriteStartElement("result_all");

                foreach (KeyValuePair<string, List<SearchRowData>> itemResult in SearchWordsResultAll)
                {
                    XMLTableShema.WriteStartElement("word");
                    XMLTableShema.WriteAttributeString("name", itemResult.Key);

                    foreach (SearchRowData SearchRowDataItem in itemResult.Value)
                        SaveItemSearchData(SearchRowDataItem);

                    XMLTableShema.WriteEndElement();
                }

                XMLTableShema.WriteEndElement();

                if (WordsShema.Count > 0)
                {
                    XMLTableShema.WriteStartElement("result_shema");

                    foreach (SearchRowData SearchRowDataItem in WordsShema)
                        SaveItemSearchData(SearchRowDataItem);

                    XMLTableShema.WriteEndElement();
                }

                if (WordsDirect.Count > 0)
                {
                    XMLTableShema.WriteStartElement("result_direct");

                    foreach (SearchRowData SearchRowDataItem in WordsDirect)
                        SaveItemSearchData(SearchRowDataItem);

                    XMLTableShema.WriteEndElement();
                }

                if (WordsEntry.Count > 0)
                {
                    XMLTableShema.WriteStartElement("result_entry");

                    foreach (SearchRowData SearchRowDataItem in WordsEntry)
                        SaveItemSearchData(SearchRowDataItem);

                    XMLTableShema.WriteEndElement();
                }
            }
        }

        /// <summary>
        /// Запис одного треку 
        /// </summary>
        /// <param name="itemTrack"></param>
        private void SaveItemTrack(Track itemTrack)
        {
            XMLTableShema.WriteStartElement("track");
            XMLTableShema.WriteAttributeString("Bridge", itemTrack.Bridge);
            XMLTableShema.WriteAttributeString("ImageA", itemTrack.ImageA);
            XMLTableShema.WriteAttributeString("ImageB", itemTrack.ImageB);
            XMLTableShema.WriteAttributeString("LevelA", itemTrack.LevelA);
            XMLTableShema.WriteAttributeString("LevelB", itemTrack.LevelB);
            //XMLTableShema.WriteAttributeString("FullPathA", itemTrack.FullPathA);
            //XMLTableShema.WriteAttributeString("FullPathB", itemTrack.FullPathB);
            XMLTableShema.WriteEndElement();
        }

        /// <summary>
        /// Запис треків
        /// </summary>
        private void SaveTracks()
        {
            XMLTableShema.WriteStartElement("tracks");

            if (TracksShemaShema.Count > 0)
            {
                XMLTableShema.WriteStartElement("shemashema");
                foreach (Track itemTrack in TracksShemaShema)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            if (TracksShemaDataDirect.Count > 0)
            {
                XMLTableShema.WriteStartElement("shemadatadirect");
                foreach (Track itemTrack in TracksShemaDataDirect)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            if (TracksShemaDataEntry.Count > 0)
            {
                XMLTableShema.WriteStartElement("shemadataentry");
                foreach (Track itemTrack in TracksShemaDataEntry)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            if (TracksDataDirectDataDirect.Count > 0)
            {
                XMLTableShema.WriteStartElement("datadirectdatadirect");
                foreach (Track itemTrack in TracksDataDirectDataDirect)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            if (TracksDataDirectDataEntry.Count > 0)
            {
                XMLTableShema.WriteStartElement("datadirectdataentry");
                foreach (Track itemTrack in TracksDataDirectDataEntry)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            if (TracksDataEntryDataEntry.Count > 0)
            {
                XMLTableShema.WriteStartElement("dataentrydataentry");
                foreach (Track itemTrack in TracksDataEntryDataEntry)
                    SaveItemTrack(itemTrack);
                XMLTableShema.WriteEndElement();
            }

            XMLTableShema.WriteEndElement();
        }

        /// <summary>
        /// Запис елемента дерева пошуку
        /// </summary>
        /// <param name="itemElement"></param>
        private void SaveTreeElement(TreeSearch itemElement)
        {
            XMLTableShema.WriteStartElement("node");
            XMLTableShema.WriteAttributeString("id", itemElement.ShemaID);

            if (itemElement.IsShema)
                XMLTableShema.WriteAttributeString("shema", "true");

            if (itemElement.IsDirect)
                XMLTableShema.WriteAttributeString("direct", "true");

            if (itemElement.IsEntry)
                XMLTableShema.WriteAttributeString("entry", "true");

            if (itemElement.Values.Count > 0)
            {
                foreach (KeyValuePair<string, string> value in itemElement.Values)
                {
                    XMLTableShema.WriteStartElement("name");
                    XMLTableShema.WriteAttributeString("dataid", value.Value);
                    XMLTableShema.WriteString(value.Key);
                    XMLTableShema.WriteEndElement();
                }
            }

            foreach (KeyValuePair<string, TreeSearch> child in itemElement.Child)
                SaveTreeElement(child.Value);

            XMLTableShema.WriteEndElement();
        }

        #endregion

        #region 3. Побудова дерева пошуку

        /// <summary>
        /// Функція поміщає в список первинні обєкти дерева пошуку
        /// </summary>
        private void CreateFirstObjectTree()
        {
            listTreeSearch = new Dictionary<string, TreeSearch>();

            if (WordsShema.Count > 0)
            {
                //Поміщаємо в дерево елементи із схеми
                foreach (SearchRowData itemSearchRowData in WordsShema)
                {
                    TreeSearch itemElement = new TreeSearch();

                    itemElement.Init();

                    itemElement.IsShema = true;
                    itemElement.ShemaID = itemSearchRowData.ShemaID;

                    listTreeSearch.Add(itemElement.ShemaID, itemElement);
                }
            }

            if (WordsDirect.Count > 0)
            {
                //Поміщаємо в дерево елементи із даних
                foreach (SearchRowData itemSearchRowData in WordsDirect)
                {
                    if (!listTreeSearch.ContainsKey(itemSearchRowData.ShemaID))
                    {
                        TreeSearch itemElement = new TreeSearch();

                        itemElement.Init();

                        itemElement.IsDirect = true;
                        itemElement.ShemaID = itemSearchRowData.ShemaID;

                        listTreeSearch.Add(itemElement.ShemaID, itemElement);
                    }
                }
            }

            if (WordsEntry.Count > 0)
            {
                //Поміщаємо в дерево елементи із входження
                foreach (SearchRowData itemSearchRowData in WordsEntry)
                {
                    if (!listTreeSearch.ContainsKey(itemSearchRowData.ShemaID))
                    {
                        TreeSearch itemElement = new TreeSearch();

                        itemElement.Init();

                        itemElement.IsEntry = true;
                        itemElement.ShemaID = itemSearchRowData.ShemaID;

                        listTreeSearch.Add(itemElement.ShemaID, itemElement);
                    }
                }
            }
        }

        /// <summary>
        /// Функція обробляє треки
        /// </summary>
        private void WorkTracks()
        {
            if (TracksShemaShema.Count > 0)
            {
                foreach (Track trackItem in TracksShemaShema)
                {
                    BuildTrack(trackItem);
                }
            }

            if (TracksShemaDataDirect.Count > 0)
            {
                foreach (Track trackItem in TracksShemaDataDirect)
                {
                    BuildTrack(trackItem);
                }
            }

            if (TracksShemaDataEntry.Count > 0)
            {
                foreach (Track trackItem in TracksShemaDataEntry)
                {
                    BuildTrack(trackItem);
                }
            }

            if (TracksDataDirectDataDirect.Count > 0)
            {
                foreach (Track trackItem in TracksDataDirectDataDirect)
                {
                    BuildTrack(trackItem);
                }
            }

            if (TracksDataDirectDataEntry.Count > 0)
            {
                foreach (Track trackItem in TracksDataDirectDataEntry)
                {
                    BuildTrack(trackItem);
                }
            }

            if (TracksDataEntryDataEntry.Count > 0)
            {
                foreach (Track trackItem in TracksDataEntryDataEntry)
                {
                    BuildTrack(trackItem);
                }
            }
        }

        /// <summary>
        /// Функція будує перехід
        /// </summary>
        /// <param name="trackItem">Перехід</param>
        private void BuildTrack(Track trackItem)
        {
            string bridge = trackItem.Bridge;

            string pathA = trackItem.PathA;
            string pathB = trackItem.PathB;

            BuildTreeNodesForPath(pathA, bridge);
            BuildTreeNodesForPath(pathB, bridge);

            //Тут можна розділити обробку на потоки

            //Може потрібно ще переходи між мостиками???
        }

        /// <summary>
        /// Функція вибудовує повне дерево для обєкта відповідно повного шляху.
        /// Для мостика переходу додатково шукаються значення
        /// </summary>
        /// <param name="path">Повний шлях</param>
        /// <param name="bridge">Мостик переходу</param>
        private void BuildTreeNodesForPath(string path, string bridge)
        {
            Dictionary<string, TreeSearch> locItemList = listTreeSearch;
            TreeSearch locItemElement = null;

            string[] itemsPath = path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string itemPath in itemsPath)
            {
                if (!locItemList.ContainsKey(itemPath))
                {
                    locItemElement = new TreeSearch();

                    locItemElement.Init();
                    locItemElement.ShemaID = itemPath;

                    locItemList.Add(itemPath, locItemElement);
                }
                else
                    locItemElement = locItemList[itemPath];

                locItemList = locItemElement.Child;

                if (itemPath == bridge)
                    UpdateValuesForTreeElement(locItemElement.Values, bridge);
            }
        }

        /// <summary>
        /// Функція шукає значення для мостика переходу
        /// </summary>
        /// <param name="values">Список значень</param>
        /// <param name="bridge">Мостик переходу</param>
        private void UpdateValuesForTreeElement(Dictionary<string, string> values, string bridge)
        {
            //Пошук в результатах прямого пошуку
            List<SearchRowData> resultFindDirect = WordsDirect.FindAll(x => x.ShemaID == bridge);
            foreach (SearchRowData itemSearchRowData in resultFindDirect)
            {
                if (!values.ContainsKey(itemSearchRowData.Name))
                    values.Add(itemSearchRowData.Name, itemSearchRowData.DataID);
            }

            //Пошук в результатах входження
            List<SearchRowData> resultFindEntry = WordsEntry.FindAll(x => x.ShemaID == bridge);
            foreach (SearchRowData itemSearchRowData in resultFindEntry)
            {
                if (!values.ContainsKey(itemSearchRowData.Name))
                    values.Add(itemSearchRowData.Name, itemSearchRowData.DataID);
            }
        }

        #endregion

        #region 2. Підготовка даних

        /// <summary>
        /// Функція отримує переходи
        /// </summary>
        private void GetAllTracks()
        {
            TracksShemaShema = new List<Track>();
            TracksShemaDataDirect = new List<Track>();
            TracksShemaDataEntry = new List<Track>();
            TracksDataDirectDataDirect = new List<Track>();
            TracksDataDirectDataEntry = new List<Track>();
            TracksDataEntryDataEntry = new List<Track>();

            if (WordsShemaID.Count > 0)
            {
                //Схема - Схема
                if (WordsShemaID.Count > 1)
                {
                    m_Data.GetTracks(TracksShemaShema, WordsShemaID, WordsShemaID);
                }

                //Схема - Дані
                if (WordsDirectShemaID.Count > 0)
                {
                    m_Data.GetTracks(TracksShemaDataDirect, WordsShemaID, WordsDirectShemaID);
                }

                //Схема - Входження
                if (WordsEntryShemaID.Count > 0)
                {
                    m_Data.GetTracks(TracksShemaDataEntry, WordsShemaID, WordsEntryShemaID);
                }
            }

            if (WordsDirectShemaID.Count > 0)
            {
                //Дані - Дані
                if (WordsDirectShemaID.Count > 1)
                {
                    m_Data.GetTracks(TracksDataDirectDataDirect, WordsDirectShemaID, WordsDirectShemaID);
                }

                //Дані - Входження
                if (WordsEntryShemaID.Count > 0)
                {
                    m_Data.GetTracks(TracksDataDirectDataEntry, WordsDirectShemaID, WordsEntryShemaID);
                }
            }

            if (WordsEntryShemaID.Count > 1)
            {
                //Входження - Входження
                if (WordsEntryShemaID.Count > 0)
                {
                    m_Data.GetTracks(TracksDataEntryDataEntry, WordsEntryShemaID, WordsEntryShemaID);
                }
            }
        }

        /// <summary>
        /// Функція розкладає і групує результати пошуку по списках
        /// </summary>
        private void RangeWordsResult()
        {
            WordsShema = new List<SearchRowData>();
            WordsDirect = new List<SearchRowData>();
            WordsEntry = new List<SearchRowData>();

            foreach (KeyValuePair<string, List<SearchRowData>> itemResult in SearchWordsResultAll)
            {
                foreach (SearchRowData SearchRowDataItem in itemResult.Value)
                {
                    if (SearchRowDataItem.IsShema)
                    {
                        WordsShema.Add(SearchRowDataItem);
                    }
                    else if (SearchRowDataItem.IsDirect)
                    {
                        WordsDirect.Add(SearchRowDataItem);
                    }
                    else if (SearchRowDataItem.IsEntry)
                    {
                        if (WordsEntry.Find(x => (x.ShemaID == SearchRowDataItem.ShemaID) && (x.DataID == SearchRowDataItem.DataID)) == null)
                            WordsEntry.Add(SearchRowDataItem);
                    }
                }
            }

            //Списки унікальних ІД схем
            WordsShemaID = new List<string>();
            WordsDirectShemaID = new List<string>();
            WordsEntryShemaID = new List<string>();

            //Схема
            if (WordsShema.Count > 0)
            {
                foreach (SearchRowData SearchRowDataItem in WordsShema)
                {
                    string ShemaID = SearchRowDataItem.ShemaID;

                    if (!WordsShemaID.Contains(ShemaID))
                        WordsShemaID.Add(ShemaID);
                }
            }

            //Прямий пошук
            if (WordsDirect.Count > 0)
            {
                foreach (SearchRowData SearchRowDataItem in WordsDirect)
                {
                    string ShemaID = SearchRowDataItem.ShemaID;

                    if (!WordsDirectShemaID.Contains(ShemaID))
                        WordsDirectShemaID.Add(ShemaID);
                }
            }

            //Входження
            if (WordsEntry.Count > 0)
            {
                foreach (SearchRowData SearchRowDataItem in WordsEntry)
                {
                    string ShemaID = SearchRowDataItem.ShemaID;

                    if (!WordsEntryShemaID.Contains(ShemaID))
                        WordsEntryShemaID.Add(ShemaID);
                }
            }
        }

        /// <summary>
        /// Функція шукає слово різними методами
        /// </summary>
        /// <param name="word">Слово</param>
        /// <returns>Список знайдених даних</returns>
        private List<SearchRowData> SearchWord(string word)
        {
            //Прямий пошук слова в схемі
            List<SearchRowData> listDirectWordShema = m_Data.SearchShema(word);

            //Якщо слово знайшлось в схемі (має бути одне)
            if (listDirectWordShema.Count > 0)
            {
                return listDirectWordShema;
            }
            else
            {
                //Прямий пошук слова в даних 
                List<SearchRowData> listDirectWordData = m_Data.SearchDirectData(word);

                //Якщо слово знайшлось в даних
                if (listDirectWordData.Count > 0)
                {
                    return listDirectWordData;
                }
                else
                {
                    //Якщо прямий пошук не дав результатів
                    //шукаємо входження слова в даних
                    List<SearchRowData> listEntryWordData = m_Data.SearchEntryData(word);

                    //Якщо є входження слова
                    if (listEntryWordData.Count > 0)
                    {
                        return listEntryWordData;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        #endregion

        #region 1. Підготовка слів

        /// <summary>
        /// Аналіз слів і видалення лишніх
        /// </summary>
        private void AnalizeWords()
        {
            if (Words.Length > 0)
            {
                //Видаляти слова які менше 3 символів
                //Перевірити чи слово є дійсно словом а не набором хирні
            }
        }

        /// <summary>
        /// Розбивка запиту на слова
        /// </summary>
        /// <returns></returns>
        private void SplitQueryToWords()
        {
            if (!String.IsNullOrWhiteSpace(Query))
            {
                Words = Query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Видаляє дублі
        /// </summary>
        private void RemoveDuplicates()
        {
            if (Words.Length > 0)
            {
                List<string> newList = new List<string>();

                foreach (string word in Words)
                    if (!newList.Contains(word))
                        newList.Add(word);

                Words = newList.ToArray();
            }
        }

        #endregion

    }
}
