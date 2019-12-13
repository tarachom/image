using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Ядро. 
    /// Через ядро відбувається взаємодія з базою даних.
    /// </summary>
    public class Kernel
    {
        private const int const_Abstract_Context = 1;
        private const int const_Union_Context = 2;
        private const int const_Intermediate_Context = 3;

        private IDataBase m_Data;

        #region CONSTRUCT

        /// <summary>
        /// Конструктор.
        /// Підключення до бази даних
        /// </summary>
        public Kernel()
        {
            m_Data = new MySqlDataBase();
            Data.ConnectString = "Database=image;Data Source=localhost;User Id=root;Password=1;";

            if (!Data.Connect())
                throw new Exception("Невдалось підключитись до бази");

            ServiceInformation();
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~Kernel()
        {
            Data.Close();
        }

        /// <summary>
        /// Інтерфейс бази даних
        /// </summary>
        private IDataBase Data
        {
            get
            {
                return m_Data;
            }
        }

        #endregion

        #region SERVICE

        /*
            Функції які виконуються ядром автоматично
        */

        /// <summary>
        /// Перевірка службової інформації
        /// </summary>
        private void ServiceInformation()
        {

            //Абстрактний контекст
            ImageContext contextAbstract = Data.GetImageContexByID(const_Abstract_Context);
            if (contextAbstract == null)
            {
                contextAbstract = new ImageContext(const_Abstract_Context, "~Abstract", "Базовий контекст");
                Data.InsertContext(contextAbstract);
            }

            //Обєднання
            ImageContext contextUnion = Data.GetImageContexByID(const_Union_Context);
            if (contextUnion == null)
            {
                contextUnion = new ImageContext(const_Union_Context, "~Union", "Контекст для обєднання");
                Data.InsertContext(contextUnion);
            }

            //Посередники
            ImageContext contextIntermediate = Data.GetImageContexByID(const_Intermediate_Context);
            if (contextIntermediate == null)
            {
                contextIntermediate = new ImageContext(const_Intermediate_Context, "~Intermediate", "Контекст для посередників");
                Data.InsertContext(contextIntermediate);
            }

            //
            // Автоматичне заповнення кодів для характеристик
            //

            //List<Image> imageList = Data.LoadAllImage();
            //foreach (Image image in imageList)
            //{

            //    foreach(CharacterystykaItem ch in image.Characterystyka)
            //    {
            //        ch.Code = ch.ItemName;
            //    }

            //    Data.UpdateImage(image);
            //}
        }

        /// <summary>
        /// Функція видаляє записи із основних таблиць
        /// </summary>
        public void DelAll()
        {
            Data.DelAll();
        }

        #endregion

        #region TRANSFORMATION

        /// <summary>
        /// Функція виконує трансформацію образів у таблиці бази даних та веб інтерфейс
        /// </summary>
        /// <param name="pathXmlDirectory">Шлях до папки в яку будуть записані ХМЛ файли та результати трансформації</param>
        public void Transformation(string pathXmlDirectory)
        {
            Transformer tranformer = new Transformer(m_Data, pathXmlDirectory);
            tranformer.Build();
        }

        #endregion

        #region JOURNAL

        /// <summary>
        /// Функція загружає журнал повідомлень
        /// </summary>
        /// <param name="messList"></param>
        public void LoadAllEventJournal(List<EventJournalMessage> messList)
        {
            Data.LoadAllEventJournal(messList);
        }

        #endregion

        #region IMAGE and IMAGEBASE

        /// <summary>
        /// Функція записує новий образ
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>true якщо все ок</returns>
        public bool InsertImage(Image image)
        {
            return Data.InsertImage(image);
        }

        /// <summary>
        /// Функція обновляє образ
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>true якщо все ок</returns>
        public bool UpdateImage(Image image)
        {
            return Data.UpdateImage(image);
        }

        /// <summary>
        /// Функція загружає всі образи в список в межах певного контексту
        /// </summary>
        /// <param name="whereContextID">Ід контексту</param>
        /// <returns>Список образів</returns>
        public List<Image> LoadAllImageByContext(int whereContextID)
        {
            return Data.LoadAllImageByContext(whereContextID);
        }

        /// <summary>
        /// Знаходить образи по назві розділені ';' 
        /// </summary>
        /// <param name="listImage">Список в який будуть записані результати пошуку</param>
        /// <param name="imageName">Строка з образами розділених ;</param>
        public void GetImagesByName(List<Image> listImage, string imageName)
        {
            string[] imageNameMas = imageName.Split(new string[] { ";" }, StringSplitOptions.None);

            for (int i = 0; i < imageNameMas.Length; i++)
                foreach (Image image in Data.GetImageByName(imageNameMas[i]))
                    listImage.Add(image);
        }

        /// <summary>
        /// Знаходить бази образів по назві розділені ';' 
        /// </summary>
        /// <param name="listImageBase">Список в який добавляються знайдені образи</param>
        /// <param name="imageName">Стрічка з назвами образів розділених ;</param>
        public void GetListImageBaseByName(List<ImageBase> listImageBase, string imageName)
        {
            string[] imageNameMas = imageName.Split(new string[] { ";" }, StringSplitOptions.None);

            for (int i = 0; i < imageNameMas.Length; i++)
                foreach (ImageBase imageBase in Data.GetListImageBaseByName(imageNameMas[i]))
                    if (imageBase != null) listImageBase.Add(imageBase);
        }

        /// <summary>
        /// ФУнкція шукає базовий образ по Назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Повертає перший знайдений образ</returns>
        public List<ImageBase> GetImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            return Data.GetListImageBaseByName(Name, whereContextID, limit);
        }

        /// <summary>
        /// Функція шукає базові образи по назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">Контекст</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Список знайдених образів</returns>
        public List<ImageBase> SearchImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            return Data.SearchImageBaseByName(Name, whereContextID, limit);
        }

        /// <summary>
        /// ФУнкція шукає базовий образ по ІД
        /// </summary>
        /// <param name="ID">Ід образу</param>
        /// <returns>Образ або нулл</returns>
        public ImageBase GetImageBaseByID(long ID)
        {
            List<ImageBase> result = Data.GetListImageBaseByID(ID);

            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        /// <summary>
        /// Загружає в список всі образи тільки з базовими полями
        /// </summary>
        /// <param name="listImage">Список для результатів</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <param name="limit">Ліміт</param>
        public void LoadAllImageBase(List<ImageBase> listImage, int whereContextID = 0, int limit = 0)
        {
            Data.LoadAllImageBase(listImage, whereContextID, limit);
        }

        /// <summary>
        /// Повертає повний образ по Ід
        /// </summary>
        /// <param name="ID">Ід образу</param>
        /// <returns>Образ або нулл</returns>
        public Image GetImageByID(long ID)
        {
            return Data.GetImageByID(ID);
        }

        /// <summary>
        /// Функція шукає образ по назві в межах контексту
        /// </summary>
        /// <param name="imageName">Назва образу</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <returns>Образ або нулл</returns>
        public Image GetImageByName(string imageName, int whereContextID)
        {
            List<Image> listImage = Data.GetImageByName(imageName, whereContextID);
            if (listImage != null)
            {
                if (listImage.Count > 0)
                {
                    if (listImage.Count == 1)
                        return listImage[0];
                    else
                    {
                        if (whereContextID > 0)
                            throw new Exception("Знайдено більше одного образу по назві '" + imageName + "' в межах контексту з ID = " + whereContextID.ToString());
                        else
                            return null; // !!! можна зробити return listImage[0];
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

        #endregion

        #region CONTEXT

        /// <summary>
        /// Записує новий контекст
        /// </summary>
        /// <param name="imageContext">Контекст</param>
        /// <returns>true якщо все ок</returns>
        public bool InsertImageContext(ImageContext imageContext)
        {
            return Data.InsertContext(imageContext);
        }

        /// <summary>
        /// Обновляє контекст
        /// </summary>
        /// <param name="imageContext">Контекст</param>
        /// <returns>true якщо все ок</returns>
        public bool UpdateImageContext(ImageContext imageContext)
        {
            return Data.UpdateContext(imageContext);
        }

        /// <summary>
        /// Функція видаляє контекст
        /// </summary>
        /// <param name="contextID">Ід контексту</param>
        /// <returns>true якщо все ок</returns>
        public bool DeleteImageContext(int contextID)
        {
            return Data.DeleteContext(contextID);
        }

        /// <summary>
        /// Загружає список всіх контекстів
        /// </summary>
        /// <param name="listContext">Список для результатів</param>
        public void LoadAllContextList(List<ImageContext> listContext)
        {
            Data.LoadAllContextList(listContext);
        }

        /// <summary>
        /// Функція шукає контекст по ІД
        /// </summary>
        /// <param name="ID">Ід контексту</param>
        /// <returns>Контекст або null</returns>
        public ImageContext GetImageContexByID(int ID)
        {
            return Data.GetImageContexByID(ID);
        }

        /// <summary>
        /// Функція шукає контекст по Назві
        /// </summary>
        /// <param name="ContextName">Назва контексту</param>
        /// <returns>Контекст або null</returns>
        public ImageContext GetImageContextByName(string ContextName)
        {
            return Data.GetImageContextByName(ContextName);
        }

        /// <summary>
        /// Функція шукає по назві контекст, якщо нема тоді створює новий
        /// </summary>
        /// <param name="contextName">Назва контексту</param>
        /// <returns>Контекст або null</returns>
        public ImageContext GetOrCreateNewContext(string contextName)
        {
            ImageContext imageContext = Data.GetImageContextByName(contextName);

            if (imageContext != null)
                return imageContext;
            else
            {
                imageContext = new ImageContext(contextName, "");

                if (Data.InsertContext(imageContext))
                    return imageContext;
                else
                    return null;
            }
        }

        #endregion

        #region PICTURES

        public bool AddPictureTemplate(PicturesTemplate PicturesTemplate)
        {
            return Data.AddPictureTemplate(PicturesTemplate);
        }

        public bool DeletePictureTemplate(int PicturesTemplateID)
        {
            return Data.DeletePictureTemplate(PicturesTemplateID);
        }

        public void LoadAllPicturesTemplateList(List<PicturesTemplate> templateList)
        {
            Data.LoadAllPicturesTemplateList(templateList);
        }

        public void LoadAllPicturesBaseList(List<PicturesBase> picturesBaseList)
        {
            Data.LoadAllPicturesBaseList(picturesBaseList);
        }

        public bool InsertPicture(Pictures pictureNew)
        {
            return Data.InsertPicture(pictureNew);
        }

        public bool UpdatePicture(Pictures picture)
        {
            return Data.UpdatePicture(picture);
        }

        public bool DeletePicture(int PictureID)
        {
            return Data.DeletePicture(PictureID);
        }

        public PicturesBase GetPicturesBaseByID(int ID)
        {
            return Data.GetPicturesBaseByID(ID);
        }

        public PicturesBase GetPicturesBaseByName(string pictureName)
        {
            return Data.GetPicturesBaseByName(pictureName);
        }

        public Pictures GetPicturesByID(int ID)
        {
            return Data.GetPicturesByID(ID);
        }

        public Pictures GetPicturesByName(string pictureName)
        {
            return Data.GetPicturesByName(pictureName);
        }

        #endregion

        #region SEARCH

        public void SearchTestAlfa(string query)
        {
            Searcher searcher = new Searcher(m_Data, query, @"D:\");

            searcher.Analize();
        }

        /// <summary>
        /// Пошук для юзер контола ControlSearch
        /// </summary>
        /// <param name="Table">Назва таблиці по якій буде пошук</param>
        /// <param name="text">Текст пошуку</param>
        /// <param name="limit">Обмеження результату пошуку</param>
        /// <param name="result">Список з результатами</param>
        public void SearchForControlSearch(string Table, string text, int limit, List<SearchElement> result)
        {
            Data.SearchPicturesOrImages(Table, text, limit, result);
        }

        #endregion

        #region Список обробки собитий

        // Обработчик ошибок
        private static void ErrorMessage(object sender, DataBaseEventArgs e)
        {
            Console.WriteLine("ERR " + e.Message + ": " + e.DescriptionMessage);
        }

        // Добавлений новий образ
        private static void AddedNewImageEventMessage(object sender, DataBaseEventArgs e)
        {
            Console.WriteLine("MSG " + e.Message + " -> '" + e.DescriptionMessage + "'");
        }

        // Видалений образ
        private static void DeleteImageEventMessage(object sender, DataBaseEventArgs e)
        {
            Console.WriteLine("MSG " + e.Message);
        }

        // Обновлений образ
        private static void UpdateImageEventMessage(object sender, DataBaseEventArgs e)
        {
            Console.WriteLine("MSG " + e.Message + "'" + e.DescriptionMessage + "'");
        }

        // Знайдений образ
        private static void GetByNameOrIDImageEventMessage(object sender, DataBaseEventArgs e)
        {
            Console.WriteLine("MSG " + e.Message);
        }

        #endregion

    }
}