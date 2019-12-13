using System;

namespace ImageLibrary
{
    /// <summary>
    /// Контекст (група) в якій описується образ
    /// </summary>
    public class ImageContext
    {
        private string m_Name;
        private string m_Description;
        private int m_ID;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ImageContext()
        {
            DefaultField();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ід контексту</param>
        public ImageContext(int id)
        {
            ID = id;

            DefaultField();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ід контексту</param>
        /// <param name="name">Назва контексту</param>
        public ImageContext(int id, string name)
        {
            ID = ID;
            Name = name;
            Description = "";
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Назва контексту</param>
        /// <param name="description">Опис</param>
        public ImageContext(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ід контексту</param>
        /// <param name="name">Назва</param>
        /// <param name="description">Опис</param>
        public ImageContext(int id, string name, string description = "")
        {
            ID = id;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Функція задає значення по замовчуванню. 
        /// Викликається з конструкторів.
        /// </summary>
        private void DefaultField()
        {
            Name = "";
            Description = "";
        }

        /// <summary>
        /// Ід контексту
        /// </summary>
        public int ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        /// <summary>
        /// Назва контексту
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        /// <summary>
        /// Розширений опис
        /// </summary>
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        /// <summary>
        /// Переоприділення ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name + (this.Description.Length > 0 ? " <" + this.Description + ">" : "");
        }
    }
}
