using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Клас для мостика переходу
    /// </summary>
    public class Bridge
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Bridge() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">ІД схеми</param>
        /// <param name="levelA">Рівень вкладеності схеми А</param>
        /// <param name="levelB">Рівень вкладеності схеми B</param>
        /// <param name="fullPathA">Повний шлях А</param>
        /// <param name="fullPathB">Повний шлях В</param>
        /// <param name="pathA">Короткий шлях А</param>
        /// <param name="pathB">Короткий шлях В</param>
        public Bridge(string id, string levelA, string levelB, string fullPathA, string fullPathB, string pathA, string pathB)
        {
            ID = id;
            LevelA = levelA;
            LevelB = levelB;
            FullPathA = fullPathA;
            FullPathB = fullPathB;
            PathA = pathA;
            PathB = pathB;
        }

        /// <summary>
        /// ІД схеми
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Рівень вкладеності схеми А
        /// </summary>
        public string LevelA { get; set; }

        /// <summary>
        /// Рівень вкладеності схеми B
        /// </summary>
        public string LevelB { get; set; }

        /// <summary>
        /// Повний шлях А
        /// </summary>
        public string FullPathA { get; set; }

        /// <summary>
        /// Повний шлях B
        /// </summary>
        public string FullPathB { get; set; }

        /// <summary>
        /// Короткий шлях А
        /// </summary>
        public string PathA { get; set; }

        /// <summary>
        /// Короткий шлях B
        /// </summary>
        public string PathB { get; set; }
    }
}
