using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для побудови дерева пошуку
    /// </summary>
    public class TreeSearch
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TreeSearch()
        {
            IsShema =
            IsDirect =
            IsEntry =
            m_IsInit = false;
        }

        private bool m_IsInit;

        /// <summary>
        /// Ініціалізація
        /// </summary>
        public void Init()
        {
            if (!m_IsInit)
            {
                Parent = new TreeSearch();
                Child = new Dictionary<string, TreeSearch>();
                Values = new Dictionary<string, string>();

                m_IsInit = true;
            }
        }

        /// <summary>
        /// Родитель даного елементу
        /// </summary>
        public TreeSearch Parent { get; set; }

        /// <summary>
        /// Діти даного елементу
        /// </summary>
        public Dictionary<string, TreeSearch> Child { get; set; }

        /// <summary>
        /// ІД схеми
        /// </summary>
        public string ShemaID { get; set; }

        /// <summary>
        /// Список значень (Назва, ІД Запису)
        /// </summary>
        public Dictionary<string, string> Values { get; set; }

        /// <summary>
        /// Чи це схема?
        /// </summary>
        public bool IsShema { get; set; }

        /// <summary>
        /// Чи це прямий пошук?
        /// </summary>
        public bool IsDirect { get; set; }

        /// <summary>
        /// Чи це пошук входження?
        /// </summary>
        public bool IsEntry { get; set; }
    }
}
