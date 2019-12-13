using System;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для інформації про поле в базі даних
    /// </summary>
    public class TableColumnInfo
    {
        /// <summary>
        /// Назва поля
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Тип поля
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Чи Нуль
        /// </summary>
        public string Null { get; set; }

        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Значення по замовчуванню
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// хз
        /// </summary>
        public string Extra { get; set; }
    }
}
