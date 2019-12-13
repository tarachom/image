using System;
using System.Text;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Інтерфейс для роботи з базою даних
    /// </summary>
    public interface IDataBase
    {
        #region EVENT

        //Собитие при ошибке
        event DataBaseStateHandler DataBaseExceptionEvent;

        //Собитие при добавленні нового образу
        event DataBaseStateHandler AddedNewImageEvent;

        //Собитие при видаленні образу
        event DataBaseStateHandler DeleteImageEvent;

        //Собитие при обновленні образу
        event DataBaseStateHandler UpdateImageEvent;

        //Собитие при пошуку образу по назві або по ІД
        event DataBaseStateHandler GetByNameOrIDImageEvent;

        #endregion

        #region CONNECT

        /// <summary>
        /// Строка підключення до бази даних
        /// </summary>
        string ConnectString { get; set; }

        /// <summary>
        /// Підключення
        /// </summary>
        /// <returns>true якщо все ок</returns>
        bool Connect();

        /// <summary>
        /// Закриття підключення
        /// </summary>
        /// <returns>true якщо все ок</returns>
        bool Close();

        #endregion

        #region IMAGE

        /// <summary>
        /// Добавлення нового образу
        /// </summary>
        /// <param name="i">Образ</param>
        /// <returns>true якщо все ок</returns>
        bool InsertImage(Image i);

        /// <summary>
        /// Обновлення образу
        /// </summary>
        /// <param name="i">Образ</param>
        /// <returns>true якщо все ок</returns>
        bool UpdateImage(Image i);

        /// <summary>
        /// Видалення образу
        /// </summary>
        /// <param name="i">Образ</param>
        /// <returns>true якщо все ок</returns>
        bool DeleteImage(Image i);

        /// <summary>
        /// Пошук образу по ІД
        /// </summary>
        /// <param name="ID">ІД образу</param>
        /// <returns>Образ</returns>
        Image GetImageByID(long ID);

        /// <summary>
        /// Пошук образу по назві
        /// </summary>
        /// <param name="Name">Назва</param>
        /// <param name="whereContextID">Контекст</param>
        /// <returns>Список образів</returns>
        List<Image> GetImageByName(string Name, int whereContextID = 0);

        /// <summary>
        /// Загружає список всіх образів
        /// </summary>
        /// <returns>Список образів</returns>
        List<Image> LoadAllImage();

        /// <summary>
        /// Загружає список образів в межах контексту
        /// </summary>
        /// <param name="whereContextID">Контекст</param>
        /// <returns>Список образів</returns>
        List<Image> LoadAllImageByContext(int whereContextID);

        /// <summary>
        /// Загружає список базових образів
        /// </summary>
        /// <param name="ID">Ід образу</param>
        /// <returns>Список образів</returns>
        List<ImageBase> GetListImageBaseByID(long ID);

        /// <summary>
        /// Загружає список базових образів по назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">Контекст</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Список образів</returns>
        List<ImageBase> GetListImageBaseByName(string Name, int whereContextID = 0, int limit = 0);

        /// <summary>
        /// Пошук базових образів по назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">ІД контексту</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Список знайдених образів</returns>
        List<ImageBase> SearchImageBaseByName(string Name, int whereContextID = 0, int limit = 0);

        /// <summary>
        /// Загружає список базових образів
        /// </summary>
        /// <param name="imageList">Список для результатів</param>
        /// <param name="whereContextID">Контекст</param>
        /// <param name="limit">Ліміт</param>
        void LoadAllImageBase(List<ImageBase> imageList, int whereContextID = 0, int limit = 0);

        #endregion

        #region CONTEXT

        /// <summary>
        /// Добавляє новий контекст в базу
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <returns>true якщо все ок</returns>
        bool InsertContext(ImageContext context);

        /// <summary>
        /// Обновляє контекст
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <returns>true якщо все ок</returns>
        bool UpdateContext(ImageContext context);

        /// <summary>
        /// Видаляє контекст
        /// </summary>
        /// <param name="contextID">Ід контексту</param>
        /// <returns>true якщо все ок</returns>
        bool DeleteContext(int contextID);

        /// <summary>
        /// Загружає список всіх контекстів
        /// </summary>
        /// <param name="listContext">Список для результатів</param>
        void LoadAllContextList(List<ImageContext> listContext);

        /// <summary>
        /// Шукає контекст по ІД
        /// </summary>
        /// <param name="ID">ІД контексту</param>
        /// <returns>Контекст</returns>
        ImageContext GetImageContexByID(int ID);

        /// <summary>
        /// Шукає контекст по назві
        /// </summary>
        /// <param name="ContextName">Назва контексту</param>
        /// <returns>Контекст</returns>
        ImageContext GetImageContextByName(string ContextName);

        #endregion

        #region Pictures

        bool AddPictureTemplate(PicturesTemplate PicturesTemplate);
        bool DeletePictureTemplate(int PicturesTemplateID);

        void LoadAllPicturesTemplateList(List<PicturesTemplate> templateList);
        void LoadAllPicturesBaseList(List<PicturesBase> picturesBaseList);

        bool InsertPicture(Pictures picture);
        bool UpdatePicture(Pictures picture);
        bool DeletePicture(int PictureID);

        PicturesBase GetPicturesBaseByID(int ID);
        PicturesBase GetPicturesBaseByName(string pictureName);
        Pictures GetPicturesByID(int ID);
        Pictures GetPicturesByName(string pictureName);

        #endregion

        #region OTHER

        /// <summary>
        /// Журнал собитий
        /// </summary>
        /// <param name="messList">Список повідомлень</param>
        void LoadAllEventJournal(List<EventJournalMessage> messList);

        void SearchPicturesOrImages(string Table, string text, int limit, List<SearchElement> result);

        /// <summary>
        /// Отримує список образів для яких даний образ виступає інградієнтом
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>Список назв образів</returns>
        List<string> GetImageIngradientyLink(Image image);

        #endregion

        #region SYSTEM FUNCTION

        /// <summary>
        /// Загружає список динамічно створених таблиць (img_%) в базі даних
        /// </summary>
        /// <returns>Список таблиць</returns>
        List<string> GetDinamicTableList();

        /// <summary>
        /// Загружає список полів для таблиці
        /// </summary>
        /// <param name="TableName">Назва таблиці</param>
        /// <returns>Список полів</returns>
        List<TableColumnInfo> GetDinamicTableColumnList(string TableName);

        /// <summary>
        /// Загружає список індексів для таблиці
        /// </summary>
        /// <param name="TableName">Назва таблиці</param>
        /// <returns>Список індексів</returns>
        List<TableIndexInfo> GetDinamicTableIndexList(string TableName);

        /// <summary>
        /// Виконує запит в базі даних
        /// </summary>
        /// <param name="query">Запит</param>
        /// <param name="query_param">Параметри запиту</param>
        /// <returns></returns>
        int ExecuteNonSQLQuery(string query, Dictionary<string, string> query_param = null);

        /// <summary>
        /// Видаляє дані з основних таблиць
        /// </summary>
        void DelAll();

        #endregion

        #region SEARCH

        List<SearchRowData> SearchShema(string query);
        List<SearchRowData> SearchDirectData(string query);
        List<SearchRowData> SearchEntryData(string query);

        void GetTracks(List<Track> Tracks, List<string> listShemaA, List<string> listShemaB);

        #endregion
    }
}
