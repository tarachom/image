using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для трансформації схеми образів Images
    /// 1. Створює таблиці в базі даних
    /// 2. Створює веб інтерфейс на основі схеми образів
    /// 3. Індексує схему та дані
    /// 4. Прокладає маршрути між образами
    /// </summary>
    public class Transformer
    {
        /// <summary>
        /// Інтерфейс для роботи з базою даних
        /// </summary>
        private IDataBase m_Data;

        private string m_FolderSaveXML;    //Папка для основних ХМЛ файлів
        private string m_FolderXSL;        //Папка для шаблонів
        private string m_FolderWeb;        //Папка для веб інтефейсу
        private string m_FolderTree;       //Папка для дерева образів
        private string m_FolderPHPClasses; //Папка для класів PHP

        /// <summary>
        /// Запис ХМЛ файлів
        /// </summary>
        private XmlWriter XMLTableShema;

        /// <summary>
        /// Список для побудови дерева образів
        /// Використовується для фіксації пройдених пунктів при рекурсивному переборі образів
        /// щоб не проходити по тих самих маршрутах і відповідно щоб не було зациклення
        /// </summary>
        private List<long> TreeImageStation;
        private int Level = 0;         //Рівень вкладеності
        private List<string> ListPath;
        private List<Bridge> ListBridge;//Список знайдених маршрутів

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Data">Інтерфейс бази даних</param>
        /// <param name="FolderSaveXML">Щлях до папку куди будуь зберігатися всі ХМЛ файли та файли веб інтерфейсу</param>
        public Transformer(IDataBase Data, string FolderSaveXML)
        {
            m_Data = Data;
            m_FolderSaveXML = FolderSaveXML;

            m_FolderWeb = FolderSaveXML + "Web" + "\\";
            m_FolderXSL = FolderSaveXML + "XSL" + "\\";
            m_FolderTree = FolderSaveXML + "Tree" + "\\";
        }

        /// <summary>
        /// Функція будує необхідну інформацію для трасформації бази даних
        /// </summary>
        public void Build()
        {
            Console.WriteLine("===Схема===");

            Console.WriteLine("Побудова схеми");
            BuildShema();

            Console.WriteLine("Порівняльний аналіз");
            Сomparison();

            Console.WriteLine("Побудова запитів");
            CreateSQL();

            Console.WriteLine("Виконання запитів");
            ExecuteSQL();

            //Console.WriteLine("\n===Індексація===");
            //Indexes();

            //Console.WriteLine("\n===Побудова дерева образів===");
            //BuildTreeImages();

            //Console.WriteLine("\n===Обчислення маршрутів===");
            //ComparisonTreeImage();

            //Console.WriteLine("\n===Запис маршрутів у базу===");
            //WriteDataTreck();

            //Console.WriteLine("\n===Побудова веб-інтерфейсу===");
            //BuildWebInterface();
        }

        #region INDEX

        /// <summary>
        /// Індексатор
        /// </summary>
        public void Indexes()
        {
            Console.Write("Обновлення флажків: ");

            int result = 0;

            result = m_Data.ExecuteNonSQLQuery("UPDATE `image_indexes_shema` SET `IsExist` = 0");
            Console.Write("[схема " + result.ToString() + "]");

            result = m_Data.ExecuteNonSQLQuery("UPDATE `image_indexes_data` SET `IsExist` = 0");
            Console.Write("[дані " + result.ToString() + "]\n");

            //Параметри запиту
            Dictionary<string, string> query_param = new Dictionary<string, string>();
            query_param.Add("ID", "");
            query_param.Add("Name", "");

            Console.Write("Загрузка схеми: [" + m_FolderSaveXML + "shema.xml] ");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            Console.Write("загружено\n");

            //Таблиці схеми
            XPathNodeIterator nodesTable = xpDocNavigator.Select("/shema/image/table[@type = 'table' and @context = 1]");
            while (nodesTable.MoveNext())
            {
                string atrTableID = nodesTable.Current.GetAttribute("id", "");
                string atrTableName = nodesTable.Current.GetAttribute("name", "");

                //Console.Write(atrTableName);

                query_param["ID"] = atrTableID;
                query_param["Name"] = atrTableName;

                //Console.Write("(");

                result = m_Data.ExecuteNonSQLQuery("INSERT INTO `image_indexes_shema` (`ID`, `Name`, `IsExist`) VALUES (@ID, @Name, 1) " +
                                                   "ON DUPLICATE KEY UPDATE `Name` = @Name, `IsExist` = 1", query_param);

                //Console.Write(result + ", ");

                result = m_Data.ExecuteNonSQLQuery("REPLACE INTO `image`.`image_indexes_data` (`ID`, `ShemaID`, `Name`, `IsExist`) " +
                                                   "SELECT `ID`, " + atrTableID + " AS `ShemaID`, `Name`, 1 AS `IsExist` " +
                                                   "FROM `image_work`.`img_" + atrTableID + "`");
                //Console.Write(result + ") ");
            }

            //Console.Write("\n");
            Console.Write("Видалення устарівших: ");

            result = m_Data.ExecuteNonSQLQuery("DELETE FROM `image_indexes_shema` WHERE `IsExist` = 0");
            Console.Write("[схема " + result.ToString() + "]");

            result = m_Data.ExecuteNonSQLQuery("DELETE FROM `image_indexes_data` WHERE `IsExist` = 0");
            Console.Write("[дані " + result.ToString() + "]");

            Console.Write("\n");
        }

        #endregion

        #region TREE

        /// <summary>
        /// Побудова дерева образів
        /// </summary>
        private void BuildTreeImages()
        {
            //Папка
            Directory.CreateDirectory(m_FolderTree);

            //Список пройдених станцій
            TreeImageStation = new List<long>();

            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            Console.Write("Загрузка схеми: [" + m_FolderSaveXML + "shema.xml] ");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            Console.Write("загружено\n");

            //Таблиці схеми
            XPathNodeIterator nodesTable = xpDocNavigator.Select("/shema/image/table[@type = 'table' and @context = 1]");
            while (nodesTable.MoveNext())
            {
                TreeImageStation.Clear();

                string atrTableID = nodesTable.Current.GetAttribute("id", "");
                string atrTableName = nodesTable.Current.GetAttribute("name", "");

                Console.Write(atrTableName + " ");

                long imageID = long.Parse(atrTableID);
                Image imageItem = m_Data.GetImageByID(imageID);

                XMLTableShema = XmlWriter.Create(m_FolderTree + "img_" + atrTableID + ".xml", XMLSettings);
                XMLTableShema.WriteComment(DateTime.Now.ToString());
                XMLTableShema.WriteStartElement("root");

                BuildTreeImageItem(imageItem);

                XMLTableShema.WriteEndElement();
                XMLTableShema.Close();
            }

            Console.Write("\n");
        }

        /// <summary>
        /// Побудова дерева для окремого образу
        /// </summary>
        /// <param name="imageItem">Образ</param>
        private void BuildTreeImageItem(Image imageItem)
        {
            //Пошук пройденого пункту
            long resultFind = TreeImageStation.Find(x => x == imageItem.ID);

            if (resultFind != 0)
                return;
            else
                TreeImageStation.Add(imageItem.ID);

            Level++;

            XMLTableShema.WriteStartElement("image");
            XMLTableShema.WriteAttributeString("id", imageItem.ID.ToString());
            XMLTableShema.WriteAttributeString("name", imageItem.Name);
            XMLTableShema.WriteAttributeString("level", Level.ToString());

            //Вказівник
            if (imageItem.Pointer)
            {
                XMLTableShema.WriteAttributeString("type", "pointer");

                Image imagePointer = m_Data.GetImageByID(imageItem.PointerImage.ID);

                XMLTableShema.WriteAttributeString("pid", imagePointer.ID.ToString());
                XMLTableShema.WriteAttributeString("pname", imagePointer.Name);

                BuildTreeImageItem(imagePointer);
            }
            //Множина
            else if (imageItem.Plural)
            {
                XMLTableShema.WriteAttributeString("type", "plural");

                Image imagePointer = m_Data.GetImageByID(imageItem.PointerImage.ID);

                XMLTableShema.WriteAttributeString("pid", imagePointer.ID.ToString());
                XMLTableShema.WriteAttributeString("pname", imagePointer.Name);

                //Для множини пока не углубляємось.
                //Тільки інформація про те що це множина.
            }
            //Посередник
            else if (imageItem.Intermediate)
            {
                //Посередник ще не готовий !!!

                XMLTableShema.WriteAttributeString("type", "intermediate");

                //...
            }
            else
            {
                if (imageItem.LinkContext != null)
                {
                    //Поля розширення
                    List<Image> contextImage = m_Data.LoadAllImageByContext(imageItem.LinkContext.ID);

                    foreach (Image contextImageItem in contextImage)
                        BuildTreeImageItem(contextImageItem);
                }

                //Інградієнти
                foreach (ImageBase imageIngradientItem in imageItem.Ingradienty)
                {
                    Image imageIngradient = m_Data.GetImageByID(imageIngradientItem.ID);
                    BuildTreeImageItem(imageIngradient);
                }
            }

            XMLTableShema.WriteEndElement();

            Level--;
        }

        /// <summary>
        /// Порівняння дерев образів і пошук маршрутів переходу
        /// </summary>
        private void ComparisonTreeImage()
        {
            Console.Write("Загрузка схеми: [" + m_FolderSaveXML + "shema.xml] ");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            Console.Write("загружено\n");

            Dictionary<string, string> tableListLocal = new Dictionary<string, string>();
            Dictionary<string, string> tableListDestination = new Dictionary<string, string>();

            XPathNodeIterator nodesTable = xpDocNavigator.Select("/shema/image/table[@type = 'table' and @context = 1]");
            while (nodesTable.MoveNext())
            {
                string atrTableID = nodesTable.Current.GetAttribute("id", "");
                string atrTableName = nodesTable.Current.GetAttribute("name", "");

                tableListLocal.Add(atrTableID, atrTableName);
                tableListDestination.Add(atrTableID, atrTableName);
            }

            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "tracks.xml", XMLSettings);
            XMLTableShema.WriteComment(DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("root");

            //Список для знайдених маршрутів
            ListBridge = new List<Bridge>();
            ListPath = new List<string>();

            foreach (KeyValuePair<string, string> tableItemLocal in tableListLocal)
            {
                Console.Write(tableItemLocal.Value + " ");

                //Загружаєм дерево першої таблиці
                XPathDocument xpDocLocal = new XPathDocument(m_FolderTree + "img_" + tableItemLocal.Key + ".xml");
                XPathNavigator xpDocNavigatorLocal = xpDocLocal.CreateNavigator();

                XPathNodeIterator rootLocal = xpDocNavigatorLocal.Select("/root");
                rootLocal.MoveNext();

                //Видаляєм із таблиці порівняння локальну таблицю
                tableListDestination.Remove(tableItemLocal.Key);

                foreach (KeyValuePair<string, string> tableItemDestination in tableListDestination)
                {
                    //Загружаєм дерево другої таблиці
                    XPathDocument xpDocDestination = new XPathDocument(m_FolderTree + "img_" + tableItemDestination.Key + ".xml");
                    XPathNavigator xpDocNavigatorDestination = xpDocDestination.CreateNavigator();

                    XPathNodeIterator rootDestination = xpDocNavigatorDestination.Select("/root");
                    rootDestination.MoveNext();

                    ListPath.Clear();
                    ListBridge.Clear();

                    RecursionFind(rootLocal.Current, rootDestination.Current);

                    if (ListBridge.Count > 0)
                    {
                        XMLTableShema.WriteStartElement("track");
                        XMLTableShema.WriteAttributeString("ida", tableItemLocal.Key);
                        XMLTableShema.WriteAttributeString("namea", tableItemLocal.Value);
                        XMLTableShema.WriteAttributeString("idb", tableItemDestination.Key);
                        XMLTableShema.WriteAttributeString("nameb", tableItemDestination.Value);

                        foreach (Bridge itemBridge in ListBridge)
                        {
                            XMLTableShema.WriteStartElement("bridge");
                            XMLTableShema.WriteAttributeString("id", itemBridge.ID);
                            XMLTableShema.WriteAttributeString("lvla", itemBridge.LevelA);
                            XMLTableShema.WriteAttributeString("lvlb", itemBridge.LevelB);
                            XMLTableShema.WriteAttributeString("pa", itemBridge.PathA);
                            XMLTableShema.WriteAttributeString("pb", itemBridge.PathB);
                            XMLTableShema.WriteAttributeString("fpa", itemBridge.FullPathA);
                            XMLTableShema.WriteAttributeString("fpb", itemBridge.FullPathB);
                            XMLTableShema.WriteEndElement();
                        }

                        XMLTableShema.WriteEndElement();
                    }
                }
            }

            Console.Write("\n");

            XMLTableShema.WriteEndElement();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Рекурсивний пошук маршрутів переходу
        /// </summary>
        /// <param name="localNavigator">Поточний навігатор по дереві образу</param>
        /// <param name="destinationNavigator">Навігатор для пошуку по дереві образу</param>
        private void RecursionFind(XPathNavigator localNavigator, XPathNavigator destinationNavigator)
        {
            XPathNodeIterator nodes = localNavigator.Select("image");

            while (nodes.MoveNext())
            {
                string atrImageId = nodes.Current.GetAttribute("id", "");
                string atrLocalLevel = nodes.Current.GetAttribute("level", "");
                string atrType = nodes.Current.GetAttribute("type", "");

                //Множину пока пропускаємо
                if (atrType == "plural")
                {
                    continue;
                }

                XPathNodeIterator nodesDestination = destinationNavigator.Select("//image[@id = " + atrImageId + "]");

                if (nodesDestination.Count > 0)
                {
                    nodesDestination.MoveNext();

                    string atrDestinationLevel = nodesDestination.Current.GetAttribute("level", "");

                    string fullPathA = GetFullPathForCurrentNode(nodes.Current);
                    string pathA = CreatePathWithFullPath(fullPathA);

                    string fullPathB = GetFullPathForCurrentNode(nodesDestination.Current);
                    string pathB = CreatePathWithFullPath(fullPathB);

                    Bridge itemBridge = new Bridge(atrImageId, atrLocalLevel, atrDestinationLevel, fullPathA, fullPathB, pathA, pathB);
                    ListBridge.Add(itemBridge);
                }
                else
                    RecursionFind(nodes.Current, destinationNavigator);
            }
        }

        /// <summary>
        /// Функція робить короткий шлях із повного шляху для мостика переходу
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private string CreatePathWithFullPath(string fullPath)
        {
            string path = "";

            int pStart = 0;
            int pEnd = 0;
            int length = 0;

            string[] itemsFullPath = fullPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string itemFullPath in itemsFullPath)
            {
                pStart = itemFullPath.IndexOf("@id", 0);
                if (pStart > 0)
                {
                    pStart += 3;

                    pStart = itemFullPath.IndexOf("=", pStart);
                    if (pStart > 0)
                    {
                        pStart += 1;

                        pEnd = itemFullPath.IndexOf("]", pStart);
                        if (pEnd > 0)
                        {
                            length = pEnd - pStart;
                            if (length > 0)
                            {
                                string shemaID = itemFullPath.Substring(pStart, length);
                                path += (path.Length > 0 ? "," : "") + shemaID;
                            }
                        }
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// Функція формує повний шлях для мостика переходу
        /// </summary>
        /// <param name="navigator">Навігатор</param>
        /// <returns></returns>
        private string GetFullPathForCurrentNode(XPathNavigator navigator)
        {
            string atrImageIdCur = navigator.GetAttribute("id", "");

            if (atrImageIdCur != "")
                atrImageIdCur = "[@id=" + atrImageIdCur + "]";

            string path = "/" + navigator.Name + atrImageIdCur + "/";

            while (navigator.MoveToParent())
            {
                if (navigator.Name != "")
                {
                    string atrImageId = navigator.GetAttribute("id", "");

                    if (atrImageId != "")
                        atrImageId = "[@id=" + atrImageId + "]";

                    path = "/" + navigator.Name + atrImageId + path;
                }
            }

            return path;
        }

        /// <summary>
        /// Запис маршрутів у базу даних
        /// </summary>
        private void WriteDataTreck()
        {
            Console.Write("Загрузка даних: [" + m_FolderSaveXML + "tracks.xml] ");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "tracks.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            Console.Write("загружено\n");

            Console.Write("Обновлення флажків: ");

            int result = 0;

            result = m_Data.ExecuteNonSQLQuery("UPDATE `image_tracks` SET `IsExist` = 0");
            Console.Write("[" + result.ToString() + "]\n");

            //Параметри запиту
            Dictionary<string, string> query_param = new Dictionary<string, string>();
            query_param.Add("imageA", "");
            query_param.Add("imageB", "");
            query_param.Add("bridge", "");
            query_param.Add("levelA", "");
            query_param.Add("levelB", "");
            query_param.Add("pathA", "");
            query_param.Add("pathB", "");
            query_param.Add("trackid", "");

            Console.Write("Обробка маршрутів\n");

            XPathNodeIterator nodesTrack = xpDocNavigator.Select("/root/track");
            while (nodesTrack.MoveNext())
            {
                string atrIDA = nodesTrack.Current.GetAttribute("ida", "");
                string atrIDB = nodesTrack.Current.GetAttribute("idb", "");

                XPathNodeIterator nodesBridge = nodesTrack.Current.Select("bridge");
                while (nodesBridge.MoveNext())
                {
                    string atrBridgeIDA = nodesBridge.Current.GetAttribute("id", "");
                    string atrBridgeLevelA = nodesBridge.Current.GetAttribute("lvla", "");
                    string atrBridgeLevelB = nodesBridge.Current.GetAttribute("lvlb", "");
                    string atrPathA = nodesBridge.Current.GetAttribute("pa", "");
                    string atrPathB = nodesBridge.Current.GetAttribute("pb", "");

                    if (atrPathA.Length > 255 || atrPathB.Length > 255)
                    {
                        throw new Exception("Шлях маршруту більший чим довжина поля в базі даних (255)");
                    }

                    query_param["imageA"] = atrIDA;
                    query_param["imageB"] = atrIDB;
                    query_param["bridge"] = atrBridgeIDA;
                    query_param["levelA"] = atrBridgeLevelA;
                    query_param["levelB"] = atrBridgeLevelB;
                    query_param["pathA"] = atrPathA;
                    query_param["pathB"] = atrPathB;
                    query_param["trackid"] = atrIDA + "-" + atrIDB + "-" + atrBridgeIDA;

                    //Прямий запис
                    result = m_Data.ExecuteNonSQLQuery(
                        "INSERT INTO `image_tracks` (`imageA`, `imageB`, `bridge`, `levelA`, `levelB`, `pathA`, `pathB`, `IsExist`, `trackid`) " +
                        "VALUES (@imageA, @imageB, @bridge, @levelA, @levelB, @pathA, @pathB, 1, @trackid) " +
                        "ON DUPLICATE KEY UPDATE `levelA` = @levelA, `levelB` = @levelB, `pathA` = @pathA, `pathB` = @pathB, `IsExist` = 1, `trackid` = @trackid", query_param);

                    //Дзеркальний запис (А і В мінаєм місцями)
                    result = m_Data.ExecuteNonSQLQuery(
                        "INSERT INTO `image_tracks` (`imageA`, `imageB`, `bridge`, `levelA`, `levelB`, `pathA`, `pathB`, `IsExist`, `trackid`) " +
                        "VALUES (@imageB, @imageA, @bridge, @levelB, @levelA, @pathB, @pathA, 1, @trackid) " +
                        "ON DUPLICATE KEY UPDATE `levelA` = @levelB, `levelB` = @levelA, `pathA` = @pathB, `pathB` = @pathA, `IsExist` = 1, `trackid` = @trackid", query_param);
                }
            }

            //Console.Write("\n");
            Console.Write("Видалення устарівших: ");

            result = m_Data.ExecuteNonSQLQuery("DELETE FROM `image_tracks` WHERE `IsExist` = 0");
            Console.Write("[" + result.ToString() + "]\n");
        }

        #endregion

        #region PHPClasses

        private void GeneratePHPClasses()
        {
            Console.WriteLine(" --> Папка для класів: " + m_FolderPHPClasses);
            Directory.CreateDirectory(m_FolderPHPClasses);

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Таблиці образів з абстрактного контексту
            XPathNodeIterator nodesImageShema = xpDocNavigator.Select("/shema/image/table[@context = 1 and @type = 'table']");
            while (nodesImageShema.MoveNext())
            {
                string atrTableID = nodesImageShema.Current.GetAttribute("id", "");
                string atrTableCode = nodesImageShema.Current.GetAttribute("code", "");
                string atrTableName = nodesImageShema.Current.GetAttribute("name", "");

                TextWriter phpClass = File.CreateText(m_FolderPHPClasses + atrTableCode);
                phpClass.WriteLine("class " + atrTableCode);
                phpClass.WriteLine("{");

                phpClass.WriteLine("public function __construct()");
                phpClass.WriteLine("{");
                phpClass.WriteLine("      // ...");
                phpClass.WriteLine("}");

                phpClass.WriteLine("}");
            }

        }

        #endregion

        #region WEB INTERFACE

        /// <summary>
        /// Побудова веб інтерфейсу
        /// </summary>
        private void BuildWebInterface()
        {
            Console.WriteLine(" --> Веб папка: " + m_FolderWeb);
            Directory.CreateDirectory(m_FolderWeb);

            Console.WriteLine(" --> Схема для першої сторінки");
            WebShemaForIndexPage();

            //Скопіювати створену схему у веб папку
            File.Copy(m_FolderSaveXML + "index.xml", m_FolderWeb + "include\\index.xml", true);

            //Шаблони
            WebTemplates();
        }

        //Функція будує схему для першої сторінки
        private void WebShemaForIndexPage()
        {
            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "index.xml", XMLSettings);
            XMLTableShema.WriteComment(DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("shema");
            XMLTableShema.WriteStartElement("image");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Таблиці образів з абстрактного контексту
            XPathNodeIterator nodesImageShema = xpDocNavigator.Select("/shema/image/table[@context = 1 and @type = 'table']");
            while (nodesImageShema.MoveNext())
            {
                XMLTableShema.WriteStartElement("table");

                string atrTableID = nodesImageShema.Current.GetAttribute("id", "");
                string atrTableCode = nodesImageShema.Current.GetAttribute("code", "");
                string atrTableName = nodesImageShema.Current.GetAttribute("name", "");

                XMLTableShema.WriteAttributeString("id", atrTableID);
                XMLTableShema.WriteAttributeString("code", atrTableCode);
                XMLTableShema.WriteAttributeString("name", atrTableName);

                XMLTableShema.WriteEndElement();
            }

            nodesImageShema = null;
            xpDoc = null;

            XMLTableShema.WriteEndElement();
            XMLTableShema.WriteEndElement();

            XMLTableShema.Flush();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Функція трансформує схему в веб інтерфейс за допопогою шаблонів
        /// </summary>
        private void WebTemplates()
        {
            Console.Write(" --> Шаблони: {");

            //
            // ЗАГРУЗКА ШАБЛОНІВ
            //

            Console.Write("image_list,");

            //Група списків
            XslCompiledTransform xslt_php_image_list = new XslCompiledTransform();
            xslt_php_image_list.Load(m_FolderXSL + "php_image_list.xsl");

            XslCompiledTransform xslt_style_image_list = new XslCompiledTransform();
            xslt_style_image_list.Load(m_FolderXSL + "style_image_list.xsl");

            Console.Write(" image_item,");

            //Група елементів
            XslCompiledTransform xslt_php_image_item = new XslCompiledTransform();
            xslt_php_image_item.Load(m_FolderXSL + "php_image_item.xsl");

            XslCompiledTransform xslt_style_image_item = new XslCompiledTransform();
            xslt_style_image_item.Load(m_FolderXSL + "style_image_item.xsl");

            Console.Write(" image_subtable_list,");

            //Група списків підтаблиць
            XslCompiledTransform xslt_php_image_subtable_list = new XslCompiledTransform();
            xslt_php_image_subtable_list.Load(m_FolderXSL + "php_image_subtable_list.xsl");

            XslCompiledTransform xslt_style_image_subtable_list = new XslCompiledTransform();
            xslt_style_image_subtable_list.Load(m_FolderXSL + "style_image_subtable_list.xsl");

            Console.Write(" image_subtable_item");

            //Група елементів підтаблиць
            XslCompiledTransform xslt_php_image_subtable_item = new XslCompiledTransform();
            xslt_php_image_subtable_item.Load(m_FolderXSL + "php_image_subtable_item.xsl");

            XslCompiledTransform xslt_style_image_subtable_item = new XslCompiledTransform();
            xslt_style_image_subtable_item.Load(m_FolderXSL + "style_image_subtable_item.xsl");

            Console.Write("}\n");

            //
            // СХЕМА
            //

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //
            // Таблиці
            //

            Console.WriteLine("Обробка таблиць");

            XPathNodeIterator nodesTables = xpDocNavigator.Select("/shema/image/table[@type = 'table']");
            while (nodesTables.MoveNext())
            {
                string atrTableId = nodesTables.Current.GetAttribute("id", "");
                string atrTableName = nodesTables.Current.GetAttribute("name", "");
                string atrTableCode = nodesTables.Current.GetAttribute("code", "");

                //Console.Write(" ---> table: " + atrTableName + " {");

                XsltArgumentList xsltArguments = new XsltArgumentList();
                xsltArguments.AddParam("table", "", atrTableId);

                //
                // СПИСКИ
                //

                //Console.Write("list");

                string result_file_dir = m_FolderWeb + atrTableId + "\\";
                Directory.CreateDirectory(result_file_dir);

                //PHP
                FileStream php_image_list = new FileStream(result_file_dir + "index.php", FileMode.Create);
                xslt_php_image_list.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, php_image_list);
                php_image_list.Close();

                //XSL
                FileStream xsl_image_list = new FileStream(result_file_dir + "index.xsl", FileMode.Create);
                xslt_style_image_list.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, xsl_image_list);
                xsl_image_list.Close();

                //Console.Write(" ok, ");

                //
                // ЕЛЕМЕНТИ
                //

                //Console.Write("element");

                string result_file_dir_item = m_FolderWeb + atrTableId + "\\item\\";
                Directory.CreateDirectory(result_file_dir_item);

                //PHP
                FileStream php_image_item = new FileStream(result_file_dir_item + "index.php", FileMode.Create);
                xslt_php_image_item.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, php_image_item);
                php_image_item.Close();

                //XSL
                FileStream xsl_image_item = new FileStream(result_file_dir_item + "index.xsl", FileMode.Create);
                xslt_style_image_item.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, xsl_image_item);
                xsl_image_item.Close();

                //Console.Write(" ok");

                //Console.Write("}\n");
            }

            //
            // ПІДТАБЛИЦІ
            //

            Console.WriteLine("Обробка підтаблиць");

            XPathNodeIterator nodesSubTables = xpDocNavigator.Select("/shema/image/table[@type = 'subtable']");
            while (nodesSubTables.MoveNext())
            {
                string atrTableId = nodesSubTables.Current.GetAttribute("id", "");
                string atrTableName = nodesSubTables.Current.GetAttribute("name", "");
                string atrTableCode = nodesSubTables.Current.GetAttribute("code", "");
                string atrTableParentId = "";

                //Пошук родітеля для таблиці
                XPathNodeIterator nodeParentTableID = xpDocNavigator.Select("/shema/image/table[@type = 'table']/subtable[@id = " + atrTableId + "]");
                if (nodeParentTableID.Count > 0)
                {
                    nodeParentTableID.MoveNext();
                    atrTableParentId = nodeParentTableID.Current.GetAttribute("parentid", "");
                }
                else
                    throw new Exception("Невдалось знайти родітеля для підтаблиці '" + atrTableName + "'");

                //Console.Write(" ---> subtable: " + atrTableName + " (родитель: " + atrTableParentId + ") {");

                XsltArgumentList xsltArguments = new XsltArgumentList();
                xsltArguments.AddParam("table", "", atrTableId);
                xsltArguments.AddParam("parent_table_id", "", atrTableId);

                //
                // СПИСКИ
                //

                //Console.Write("list");

                string result_file_dir_subtable_list = m_FolderWeb + atrTableId + "\\";
                Directory.CreateDirectory(result_file_dir_subtable_list);

                //PHP
                FileStream php_image_subtable_list = new FileStream(result_file_dir_subtable_list + "index.php", FileMode.Create);
                xslt_php_image_subtable_list.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, php_image_subtable_list);
                php_image_subtable_list.Close();

                //XSL
                FileStream xsl_image_subtable_list = new FileStream(result_file_dir_subtable_list + "index.xsl", FileMode.Create);
                xslt_style_image_subtable_list.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, xsl_image_subtable_list);
                xsl_image_subtable_list.Close();

                //Console.Write(" ok, ");

                //
                // ЕЛЕМЕНТИ
                //

                //Console.Write("element");

                string result_file_dir_subtable_item = m_FolderWeb + atrTableId + "\\item\\";
                Directory.CreateDirectory(result_file_dir_subtable_item);

                //PHP
                FileStream php_image_subtable_item = new FileStream(result_file_dir_subtable_item + "index.php", FileMode.Create);
                xslt_php_image_subtable_item.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, php_image_subtable_item);
                php_image_subtable_item.Close();

                //XSL
                FileStream xsl_image_subtable_item = new FileStream(result_file_dir_subtable_item + "index.xsl", FileMode.Create);
                xslt_style_image_subtable_item.Transform(m_FolderSaveXML + "shema.xml", xsltArguments, xsl_image_subtable_item);
                xsl_image_subtable_item.Close();

                //Console.Write(" ok");

                //Console.Write("}\n");
            }
        }

        #endregion

        #region SHEMA

        /// <summary>
        /// Функція виконує запити з файлу sql.xml сформовані на основі аналізу бази даних
        /// </summary>
        private void ExecuteSQL()
        {
            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "sql.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Переключення бази даних на WORK
            m_Data.ExecuteNonSQLQuery("USE `image_work`");

            //Запроси
            XPathNodeIterator nodesSQL = xpDocNavigator.Select("/sql/query");
            while (nodesSQL.MoveNext())
            {
                Console.WriteLine("Запрос: ");
                Console.WriteLine(nodesSQL.Current.Value);
                Console.WriteLine("Результат: " + m_Data.ExecuteNonSQLQuery(nodesSQL.Current.Value).ToString());

                Console.WriteLine();
            }

            //Переключення бази даних на основну
            m_Data.ExecuteNonSQLQuery("USE `image`");
        }

        /// <summary>
        /// Функція формує файл запитів sql.xml на основі порівняльної таблиці comparison.xml
        /// </summary>
        private void CreateSQL()
        {
            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "sql.xml", XMLSettings);
            XMLTableShema.WriteComment(DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("sql");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "comparison.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Таблиці
            XPathNodeIterator nodesTable = xpDocNavigator.Select("/comparison/table");
            while (nodesTable.MoveNext())
            {
                string atrTableState = nodesTable.Current.GetAttribute("state", "");
                string atrTableCode = nodesTable.Current.GetAttribute("code", "");
                string atrTableName = nodesTable.Current.GetAttribute("name", "");

                XPathNodeIterator nodesAll =
                       xpDocNavigator.Select("/comparison/table[@code = '" + atrTableCode + "' and (@state = 'exist' or @state = 'create')]" +
                                             "/field[@state = 'create' or @state = 'delete' or @index = 'create' or @index = 'delete']");
                if (nodesAll.Count == 0)
                    continue;

                XMLTableShema.WriteStartElement("query");

                if (atrTableState == "exist")
                {
                    //Оприділити чи є зміни в полях для таблиці
                    XPathNodeIterator nodesChangeField =
                        xpDocNavigator.Select("/comparison/table[@code = '" + atrTableCode + "']" +
                                              "/field[@state = 'create' or @state = 'delete' or @index = 'create' or @index = 'delete']");

                    if (nodesChangeField.Count > 0)
                    {
                        XMLTableShema.WriteString("ALTER TABLE `" + atrTableCode + "` \n");

                        while (nodesChangeField.MoveNext())
                        {
                            string atrFieldState = nodesChangeField.Current.GetAttribute("state", "");
                            string atrFieldCode = nodesChangeField.Current.GetAttribute("code", "");
                            string atrFieldType = nodesChangeField.Current.GetAttribute("type", "");
                            string atrFieldIndex = nodesChangeField.Current.GetAttribute("index", "");
                            string atrFieldName = nodesChangeField.Current.GetAttribute("name", "");

                            if (atrFieldState == "create")
                            {
                                if (nodesChangeField.CurrentPosition > 1)
                                    XMLTableShema.WriteString(", \n");

                                XMLTableShema.WriteString("ADD COLUMN `" + atrFieldCode + "` ");
                                if (atrFieldType == "auto_increment")
                                    XMLTableShema.WriteString("INTEGER UNSIGNED NOT NULL AUTO_INCREMENT");
                                else if (atrFieldType == "link" || atrFieldType == "integer")
                                    XMLTableShema.WriteString("INTEGER UNSIGNED NOT NULL DEFAULT 0");
                                else if (atrFieldType == "string")
                                    XMLTableShema.WriteString("VARCHAR(100) NOT NULL DEFAULT ''");
                                else
                                    throw new Exception("Неоприділений тип поля");

                                XMLTableShema.WriteString(" COMMENT '" + Qvote(atrFieldName) + "'");
                            }
                            else if (atrFieldState == "delete")
                            {
                                if (nodesChangeField.CurrentPosition > 1)
                                    XMLTableShema.WriteString(", \n");

                                XMLTableShema.WriteString("DROP COLUMN `" + atrFieldCode + "` ");
                            }

                            //Index
                            if (atrFieldIndex == "create")
                            {
                                if ((nodesChangeField.CurrentPosition > 1) || atrFieldState == "create" || atrFieldState == "delete")
                                    XMLTableShema.WriteString(", \n");

                                XMLTableShema.WriteString("ADD INDEX `index_" + atrTableCode + "_" + atrFieldCode + "` (`" + atrFieldCode + "`)");
                            }
                            else if (atrFieldIndex == "delete")
                            {
                                if ((nodesChangeField.CurrentPosition > 1) || atrFieldState == "create" || atrFieldState == "delete")
                                    XMLTableShema.WriteString(", \n");

                                XMLTableShema.WriteString("DROP INDEX `index_" + atrTableCode + "_" + atrFieldCode + "`");
                            }
                        }
                    }
                    nodesChangeField = null;
                }
                else if (atrTableState == "create")
                {
                    XMLTableShema.WriteString("CREATE TABLE `" + atrTableCode + "` ( \n");

                    //Поля
                    XPathNodeIterator nodesField = xpDocNavigator.Select("/comparison/table[@code = '" + atrTableCode + "']/field");
                    while (nodesField.MoveNext())
                    {
                        string atrFieldCode = nodesField.Current.GetAttribute("code", "");
                        string atrFieldType = nodesField.Current.GetAttribute("type", "");
                        string atrFieldIndex = nodesField.Current.GetAttribute("index", "");
                        string atrFieldName = nodesField.Current.GetAttribute("name", "");

                        if (nodesField.CurrentPosition > 1)
                            XMLTableShema.WriteString(", \n");

                        XMLTableShema.WriteString("`" + atrFieldCode + "` ");

                        if (atrFieldType == "auto_increment")
                            XMLTableShema.WriteString("INTEGER UNSIGNED NOT NULL AUTO_INCREMENT");
                        else if (atrFieldType == "link" || atrFieldType == "integer")
                            XMLTableShema.WriteString("INTEGER UNSIGNED NOT NULL DEFAULT 0");
                        else if (atrFieldType == "string")
                            XMLTableShema.WriteString("VARCHAR(100) NOT NULL DEFAULT ''");
                        else
                            throw new Exception("Неоприділений тип поля");

                        XMLTableShema.WriteString(" COMMENT '" + Qvote(atrFieldName) + "'");

                        //Index
                        if (atrFieldIndex == "create")
                        {
                            XMLTableShema.WriteString(", \n");
                            XMLTableShema.WriteString("INDEX `index_" + atrTableCode + "_" + atrFieldCode + "` (`" + atrFieldCode + "`)");
                        }
                    }
                    nodesField = null;

                    XMLTableShema.WriteString(", \n");
                    XMLTableShema.WriteString("PRIMARY KEY(`ID`)");
                    XMLTableShema.WriteString(" \n");
                    XMLTableShema.WriteString(") ENGINE = InnoDB CHARACTER SET utf8 COLLATE utf8_general_ci");
                    XMLTableShema.WriteString(" \n");
                    XMLTableShema.WriteString("COMMENT = '" + Qvote(atrTableName) + "'");
                }

                XMLTableShema.WriteEndElement();
            }

            XMLTableShema.WriteEndElement();
            XMLTableShema.Flush();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Функція екранує символи
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string Qvote(string text)
        {
            string result = text.Replace("'", "\\'");

            return result;
        }

        /// <summary>
        /// Функція порівнює таблиці бази даних з таблицями дерева образів
        /// </summary>
        private void Сomparison()
        {
            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "comparison.xml", XMLSettings);
            XMLTableShema.WriteComment(DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("comparison");

            XPathDocument xpDoc = new XPathDocument(m_FolderSaveXML + "shema.xml");
            XPathNavigator xpDocNavigator = xpDoc.CreateNavigator();

            //Таблиці образів
            XPathNodeIterator nodesImageShema = xpDocNavigator.Select("/shema/image/table");
            while (nodesImageShema.MoveNext())
            {
                XMLTableShema.WriteStartElement("table");

                string atrTableID = nodesImageShema.Current.GetAttribute("id", "");
                string atrTableCode = nodesImageShema.Current.GetAttribute("code", "");
                string atrTableName = nodesImageShema.Current.GetAttribute("name", "");

                //Пошук таблиці в базі
                XPathNodeIterator nodeTableDataShema = xpDocNavigator.Select("/shema/data/table[@name = '" + atrTableCode + "']");

                //Признак наявності таблиці
                int tableState = nodeTableDataShema.Count;

                XMLTableShema.WriteAttributeString("state", (tableState == 1 ? "exist" : "create"));
                XMLTableShema.WriteAttributeString("code", atrTableCode);
                XMLTableShema.WriteAttributeString("name", atrTableName);

                nodeTableDataShema = null;

                string expression_for_del_field = "";

                //Вибірка полів образу
                XPathNodeIterator nodesImageField = xpDocNavigator.Select("/shema/image/table[@id = " + atrTableID + "]/field");
                while (nodesImageField.MoveNext())
                {
                    string atrFieldID = nodesImageField.Current.GetAttribute("id", "");
                    string atrFieldCode = nodesImageField.Current.GetAttribute("code", "");
                    string atrFieldName = nodesImageField.Current.GetAttribute("name", "");
                    string atrFieldType = nodesImageField.Current.GetAttribute("type", "");
                    string atrFieldIndex = nodesImageField.Current.GetAttribute("index", "");

                    if (tableState == 1)
                        expression_for_del_field += (nodesImageField.CurrentPosition > 1 ? " and " : "") + "@name != '" + atrFieldCode + "'";

                    XMLTableShema.WriteStartElement("field");

                    //Пошук поля в базі
                    XPathNodeIterator nodeFieldDataShema =
                        xpDocNavigator.Select("/shema/data/table[@name = '" + atrTableCode + "']/field[@name = '" + atrFieldCode + "']");

                    XMLTableShema.WriteAttributeString("state", (nodeFieldDataShema.Count == 1 ? "exist" : "create"));
                    XMLTableShema.WriteAttributeString("code", atrFieldCode);
                    XMLTableShema.WriteAttributeString("name", atrFieldName);
                    XMLTableShema.WriteAttributeString("type", atrFieldType);

                    //Якщо поле індексується
                    if (atrFieldIndex == "1")
                    {
                        //Пошук індексу для поля
                        XPathNodeIterator nodeFieldIndexShema =
                            xpDocNavigator.Select("/shema/data/table[@name = '" + atrTableCode + "']/index[@field = '" + atrFieldCode + "']");

                        XMLTableShema.WriteAttributeString("index", (nodeFieldIndexShema.Count == 1 ? "exist" : "create"));
                    }

                    nodeFieldDataShema = null;

                    XMLTableShema.WriteEndElement();
                }

                nodesImageField = null;

                if (tableState == 1)
                {
                    //Пошук полів які треба видалити
                    XPathNodeIterator nodesDeleteFieldDataShema =
                           xpDocNavigator.Select("/shema/data/table[@name = '" + atrTableCode + "']/field[" + expression_for_del_field + "]");

                    while (nodesDeleteFieldDataShema.MoveNext())
                    {
                        string atrFieldName = nodesDeleteFieldDataShema.Current.GetAttribute("name", "");

                        XMLTableShema.WriteStartElement("field");

                        XMLTableShema.WriteAttributeString("state", "delete");
                        XMLTableShema.WriteAttributeString("code", atrFieldName);
                        XMLTableShema.WriteAttributeString("name", atrFieldName);

                        //Пошук індексу для поля
                        XPathNodeIterator nodeFieldIndexShema =
                            xpDocNavigator.Select("/shema/data/table[@name = '" + atrTableCode + "']/index[@field = '" + atrFieldName + "']");

                        if (nodeFieldIndexShema.Count == 1)
                            XMLTableShema.WriteAttributeString("index", "delete");

                        XMLTableShema.WriteEndElement();
                    }
                    nodesDeleteFieldDataShema = null;
                }

                XMLTableShema.WriteEndElement();
            }

            nodesImageShema = null;
            xpDoc = null;

            XMLTableShema.WriteEndElement();
            XMLTableShema.Flush();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Функція формує схему із таблиць бази даних і даних дерева образів
        /// </summary>
        private void BuildShema()
        {
            XmlWriterSettings XMLSettings = new XmlWriterSettings();
            XMLSettings.Encoding = System.Text.Encoding.UTF8;
            XMLSettings.Indent = true;

            XMLTableShema = XmlWriter.Create(m_FolderSaveXML + "shema.xml", XMLSettings);
            XMLTableShema.WriteComment("Схема - " + DateTime.Now.ToString());
            XMLTableShema.WriteStartElement("shema");

            Console.WriteLine(" --> схема образів");
            BuildImageShema();

            Console.WriteLine(" --> схема бази даних");
            BuildDataShema();

            XMLTableShema.WriteEndElement();
            XMLTableShema.Close();
        }

        /// <summary>
        /// Функція формує дані дерева образів
        /// </summary>
        private void BuildImageShema()
        {
            /*
            Схема побудови:

            Варіант 1. Якщо образ має мітку множина то для нього ввійдуть поля інградієнтів, характеритик. 
                       Поля із ссилки контексту невраховуються.

                       Образ множина це підтаблиця і відповідно вона містить інформацію про обєкт, а все додаткове 
                       вже міститься в самому обєкті.

            Варіант 2. Образ має мітку вказівник. Для такого образу нестворюється таблиця, так як це просто перенаправлення
                       на іншу таблицю.

            Варіант 3. Образ не містить міток вказівник і не є множина. Тоді для такого образу створюється таблиця. В неї 
                       входять поля  інградієнтів, характеритик і також поля із ссилки контексту.
                       Якщо в полях ссилки контексту міститься поле множина тоді вказується що це підтаблиця і поле нестворюється.
                       Якщо в полях ссилки контексту є поле вказівник, тоді поле створюється але з перенаправленням на таблицю із цього вказівника.

                       Відповідно виходить що ссилку на контекст можна не задавати якщо образ є множина або це вказівник.
            */

            //Структура бази даних
            XMLTableShema.WriteStartElement("image");

            //Всі образи
            List<Image> imageList = m_Data.LoadAllImage();
            foreach (Image ImageItem in imageList)
            {
                //Код таблиці
                string TableCode = "img_" + ImageItem.ID.ToString();

                //Для вказівника створювати таблицю непотрібно
                //pointer - просто запис що він є
                if (ImageItem.Pointer)
                {
                    ImageBase pointerImage = ImageItem.PointerImage;

                    if (pointerImage == null)
                        throw new Exception("Для образу " + ImageItem.Name + " не вказаний Вказівник");

                    /*
                    XMLTableShema.WriteStartElement("pointer");
                    XMLTableShema.WriteAttributeString("id", ImageItem.ID.ToString());
                    XMLTableShema.WriteAttributeString("name", ImageItem.Name);
                    XMLTableShema.WriteAttributeString("descr", ImageItem.Description);
                    XMLTableShema.WriteAttributeString("context_id", ImageItem.Context.ID.ToString());
                    XMLTableShema.WriteAttributeString("context_name", ImageItem.Context.Name);
                    XMLTableShema.WriteAttributeString("pointer_id", pointerImage.ID.ToString());
                    XMLTableShema.WriteAttributeString("pointer_name", pointerImage.Name);
                    XMLTableShema.WriteEndElement();
                    */

                    continue;
                }

                //Посередник
                if (ImageItem.Intermediate)
                {
                    continue;
                }

                //Ссилка
                ImageContext linkContextForXML = ImageItem.LinkContext;

                XMLTableShema.WriteStartElement("table");

                XMLTableShema.WriteAttributeString("type", (ImageItem.Plural ? "subtable" : "table"));
                XMLTableShema.WriteAttributeString("id", ImageItem.ID.ToString());
                XMLTableShema.WriteAttributeString("name", ImageItem.Name);
                XMLTableShema.WriteAttributeString("code", TableCode);
                XMLTableShema.WriteAttributeString("descr", ImageItem.Description);
                XMLTableShema.WriteAttributeString("context", ImageItem.Context.ID.ToString());

                if (linkContextForXML != null)
                {
                    //Додаткові атрибути для таблиці
                    XMLTableShema.WriteAttributeString("link_context", linkContextForXML.ID.ToString());
                }

                //Поле ID для всіх
                XMLTableShema.WriteStartElement("field");
                XMLTableShema.WriteAttributeString("name", "ID");
                XMLTableShema.WriteAttributeString("code", "ID");
                XMLTableShema.WriteAttributeString("descr", "ID");
                XMLTableShema.WriteAttributeString("type", "auto_increment");
                XMLTableShema.WriteEndElement();

                //Для множини створюються свої поля
                if (ImageItem.Plural)
                {
                    //Треба знайти родітельську таблицю
                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("name", "Родитель");
                    XMLTableShema.WriteAttributeString("code", "PARENTID");
                    XMLTableShema.WriteAttributeString("descr", "Родитель");
                    XMLTableShema.WriteAttributeString("type", "integer");
                    XMLTableShema.WriteAttributeString("index", "1");
                    XMLTableShema.WriteEndElement();

                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("name", "Об'єкт");
                    XMLTableShema.WriteAttributeString("code", "OBJECTID");
                    XMLTableShema.WriteAttributeString("descr", "Об'єкт");
                    XMLTableShema.WriteAttributeString("type", "link");
                    XMLTableShema.WriteAttributeString("table_link", "img_" + ImageItem.PointerImage.ID.ToString());
                    XMLTableShema.WriteAttributeString("index", "1");
                    XMLTableShema.WriteEndElement();
                }
                else
                {
                    //Поле Name для всіх (крім множини)
                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("name", "Назва");
                    XMLTableShema.WriteAttributeString("code", "NAME");
                    XMLTableShema.WriteAttributeString("descr", "Назва");
                    XMLTableShema.WriteAttributeString("type", "string");
                    XMLTableShema.WriteEndElement();
                }

                //Для образу множини ссилка на контекст не враховується
                //так як в підтаблиці не потрібні додаткові поля. Враховуються тільки характеритики та інградієнти
                if (!ImageItem.Plural)
                {
                    //Ссилки
                    if (linkContextForXML != null)
                    {
                        List<Image> imageLinkList = m_Data.LoadAllImageByContext(linkContextForXML.ID);
                        foreach (Image ImageLinkItem in imageLinkList)
                        {
                            ImageBase pointerImage = ImageLinkItem.PointerImage;

                            //Перевірити чи заповнений вказівник
                            if (ImageLinkItem.Pointer || ImageLinkItem.Plural)
                                if (pointerImage == null)
                                    throw new Exception("Для образу " + ImageLinkItem.Name + " не вказаний Вказівник");

                            //Якщо це вказівник 
                            if (ImageLinkItem.Pointer)
                            {
                                //Якщо є мітка вказівник, тоді вся інформація для поля
                                //береться із цього вказівника
                                XMLTableShema.WriteStartElement("field");
                                XMLTableShema.WriteAttributeString("id", pointerImage.ID.ToString());
                                XMLTableShema.WriteAttributeString("name", pointerImage.Name);
                                XMLTableShema.WriteAttributeString("code", "img_" + pointerImage.ID.ToString());
                                XMLTableShema.WriteAttributeString("descr", pointerImage.Description);
                                XMLTableShema.WriteAttributeString("type", "link");
                                XMLTableShema.WriteAttributeString("table_link", "img_" + pointerImage.ID.ToString());
                                XMLTableShema.WriteAttributeString("index", "1");
                                XMLTableShema.WriteEndElement();
                            }
                            //Якщо множина
                            else if (ImageLinkItem.Plural)
                            {
                                //Для множини створюється підтаблиця і так само вся інформації
                                //береться із вказівника
                                XMLTableShema.WriteStartElement("subtable");
                                XMLTableShema.WriteAttributeString("id", ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("parentid", ImageItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("name", ImageLinkItem.Name);
                                XMLTableShema.WriteAttributeString("code", "img_" + ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("descr", ImageLinkItem.Description);
                                XMLTableShema.WriteAttributeString("table_link", "img_" + ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteEndElement();
                            }
                            else
                            {
                                //В інших випадках інформація береться із самого образу
                                XMLTableShema.WriteStartElement("field");
                                XMLTableShema.WriteAttributeString("id", ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("name", ImageLinkItem.Name);
                                XMLTableShema.WriteAttributeString("code", "img_" + ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("descr", ImageLinkItem.Description);
                                XMLTableShema.WriteAttributeString("type", "link");
                                XMLTableShema.WriteAttributeString("table_link", "img_" + ImageLinkItem.ID.ToString());
                                XMLTableShema.WriteAttributeString("index", "1");
                                XMLTableShema.WriteEndElement(); //field
                            }
                        }
                    }
                }

                //Характеритики
                foreach (CharacterystykaItem characterystykaItem in ImageItem.Characterystyka)
                {
                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("id", "0");
                    XMLTableShema.WriteAttributeString("name", characterystykaItem.ItemName);
                    XMLTableShema.WriteAttributeString("code", "ch_" + characterystykaItem.Code);
                    XMLTableShema.WriteAttributeString("descr", characterystykaItem.ItemValue);
                    XMLTableShema.WriteAttributeString("type", "string");
                    XMLTableShema.WriteAttributeString("index", "0");
                    XMLTableShema.WriteEndElement();
                }

                //Інградієнти
                foreach (ImageBase ingradientItem in ImageItem.Ingradienty)
                {
                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("id", ingradientItem.ID.ToString());
                    XMLTableShema.WriteAttributeString("name", ingradientItem.Name);
                    XMLTableShema.WriteAttributeString("code", "ing_img_" + ingradientItem.ID.ToString());
                    XMLTableShema.WriteAttributeString("descr", ingradientItem.Description);
                    XMLTableShema.WriteAttributeString("type", "link");
                    XMLTableShema.WriteAttributeString("table_link", "img_" + ingradientItem.ID.ToString());
                    XMLTableShema.WriteAttributeString("index", "1");
                    XMLTableShema.WriteEndElement();
                }

                XMLTableShema.WriteEndElement();
            }

            XMLTableShema.WriteEndElement();
            XMLTableShema.Flush();
        }

        /// <summary>
        /// Функція формує дані таблиць бази даних
        /// </summary>
        private void BuildDataShema()
        {
            //Переключення бази даних (WORK)
            m_Data.ExecuteNonSQLQuery("USE `image_work`");

            //Структура бази даних
            XMLTableShema.WriteStartElement("data");

            List<string> TableList = m_Data.GetDinamicTableList();
            foreach (string Table in TableList)
            {
                XMLTableShema.WriteStartElement("table");
                XMLTableShema.WriteAttributeString("name", Table);

                List<TableColumnInfo> ColumnList = m_Data.GetDinamicTableColumnList(Table);
                foreach (TableColumnInfo columnInfo in ColumnList)
                {
                    XMLTableShema.WriteStartElement("field");
                    XMLTableShema.WriteAttributeString("name", columnInfo.Field);
                    XMLTableShema.WriteAttributeString("type", columnInfo.Type);
                    XMLTableShema.WriteAttributeString("null", columnInfo.Null);
                    XMLTableShema.WriteAttributeString("key", columnInfo.Key);
                    XMLTableShema.WriteAttributeString("default", columnInfo.Default);
                    XMLTableShema.WriteAttributeString("extra", columnInfo.Extra);
                    XMLTableShema.WriteEndElement();
                }

                List<TableIndexInfo> IndexList = m_Data.GetDinamicTableIndexList(Table);
                foreach (TableIndexInfo indexInfo in IndexList)
                {
                    XMLTableShema.WriteStartElement("index");
                    XMLTableShema.WriteAttributeString("table", indexInfo.Table);
                    XMLTableShema.WriteAttributeString("field", indexInfo.Field);
                    XMLTableShema.WriteAttributeString("type", indexInfo.Type);
                    XMLTableShema.WriteAttributeString("null", indexInfo.Null);
                    XMLTableShema.WriteAttributeString("key", indexInfo.Key);
                    XMLTableShema.WriteAttributeString("non_unigue", indexInfo.NonUnigue.ToString());
                    XMLTableShema.WriteEndElement();
                }

                XMLTableShema.WriteEndElement();
            }

            XMLTableShema.WriteEndElement();
            XMLTableShema.Flush();

            //Переключення бази даних (основна)
            m_Data.ExecuteNonSQLQuery("USE `image`");
        }

        #endregion
    }
}