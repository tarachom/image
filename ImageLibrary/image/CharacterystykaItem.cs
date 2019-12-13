using System;

namespace ImageLibrary
{
    /// <summary>
    /// Додаткове поле для образу
    /// </summary>
    public class CharacterystykaItem
    {
        /// <summary>
        /// Конструктор по замовчуванню
        /// </summary>
        public CharacterystykaItem()
        {
            ItemName = "";
            ItemValue = "";
            Code = "";
        }

        /// <summary>
        /// Конструктор з параметрами
        /// </summary>
        /// <param name="itemName">Назва поля</param>
        /// <param name="itemValue">Опис поля</param>
        /// <param name="code">Код поля</param>
        public CharacterystykaItem(string itemName, string itemValue, string code = "")
        {
            ItemName = itemName;
            ItemValue = itemValue;
            Code = code;
        }

        /// <summary>
        ///Код поля характеристики. 
        ///Використовується для бази даних
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Назва поля характеристики
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Значення поля характеристики
        /// </summary>
        public string ItemValue { get; set; }
    }
}
