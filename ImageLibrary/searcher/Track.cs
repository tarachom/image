using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Клас описує перехід (чи доріжку) між образами
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Track() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="imageA">Образ А</param>
        /// <param name="imageB">Образ В</param>
        /// <param name="bridge">Мостик</param>
        /// <param name="levelA">Рівень А</param>
        /// <param name="levelB">Рівень В</param>
        /// <param name="pathA">Шлях А</param>
        /// <param name="pathB">Шлях В</param>
        public Track(string imageA, string imageB, string bridge, string levelA, string levelB,string pathA, string pathB)
        {
            ImageA = imageA;
            ImageB = imageB;
            Bridge = bridge;
            LevelA = levelA;
            LevelB = levelB;
            PathA = pathA;
            PathB = pathB;
        }

        /// <summary>
        /// Образ А
        /// </summary>
        public string ImageA { get; set; }

        /// <summary>
        /// Образ B
        /// </summary>
        public string ImageB { get; set; }

        /// <summary>
        /// Мостик
        /// </summary>
        public string Bridge { get; set; }

        /// <summary>
        /// Рівень А
        /// </summary>
        public string LevelA { get; set; }

        /// <summary>
        /// Рівень B
        /// </summary>
        public string LevelB { get; set; }

        /// <summary>
        /// Шлях А
        /// </summary>
        public string PathA { get; set; }

        /// <summary>
        /// Шлях B
        /// </summary>
        public string PathB { get; set; }
    }

}
