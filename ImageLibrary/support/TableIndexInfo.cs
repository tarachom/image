using System;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для інформації про індекс в базі даних
    /// </summary>
    public class TableIndexInfo
    {
        /// <summary>
        /// Назва таблиці
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Назва поля індексу
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Назва індексу
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Тип індексу
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Чи Нуль
        /// </summary>
        public string Null { get; set; }

        /// <summary>
        /// Чи не унікальний... тут треба уточнити!!!
        /// </summary>
        public int NonUnigue { get; set; }        
    }
}
