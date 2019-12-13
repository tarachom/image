using System;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для результатів пошуку
    /// </summary>
    public class SearchRowData
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SearchRowData() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="query">Запит</param>
        /// <param name="shema_id">ІД схеми</param>
        /// <param name="name">Назва</param>
        /// <param name="data_id">ІД даних</param>
        /// <param name="isShema">Чи це схема?</param>
        /// <param name="isDirect">Чи це пряме співпадіння?</param>
        /// <param name="isEntry">Чи це входження?</param>
        public SearchRowData(string query, string shema_id, string name, string data_id, bool isShema, bool isDirect, bool isEntry)
        {
            Query = query;
            DataID = data_id;
            ShemaID = shema_id;
            Name = name;

            IsShema = isShema;
            IsDirect = isDirect;
            IsEntry = isEntry;
        }

        /// <summary>
        /// Запит
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// ІД запису в даних
        /// </summary>
        public string DataID { get; set; }

        /// <summary>
        /// Назва
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ід схеми
        /// </summary>
        public string ShemaID { get; set; }

        /// <summary>
        /// Це схема?
        /// </summary>
        public bool IsShema { get; set; }

        /// <summary>
        /// Це пряме співпадіння?
        /// </summary>
        public bool IsDirect { get; set; }

        /// <summary>
        /// Це входження?
        /// </summary>
        public bool IsEntry { get; set; }
    }
}
