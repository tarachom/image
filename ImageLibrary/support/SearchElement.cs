using System;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для результатів пошуку
    /// </summary>
    public class SearchElement
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ід елементу</param>
        /// <param name="name">Назва</param>
        /// <param name="context">Назва контексту</param>
        public SearchElement(int id, string name, string context = "")
        {
            Name = name;
            ID = id;
            ContextName = context;
        }

        /// <summary>
        /// ІД елементу
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Назва елементу
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Назва контексту
        /// </summary>
        public string ContextName { get; set; }

        /// <summary>
        /// Переоприділення ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + (ContextName.Length > 0 ? " <" + ContextName + ">" : "");
        }
    }
}
