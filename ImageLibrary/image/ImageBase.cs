using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Базові елементи образу, без колекцій
    /// </summary>
    public class ImageBase
    {
        private long m_ID;
        private string m_Name;
        private string m_Description;
        private string m_Synonymy;
        
        //Мітки
        private bool m_Pointer;      //Вказівник
        private bool m_Intermediate; //Посередник
        private bool m_Plural;       //Множина (підтаблиця)

        private ImageContext m_Context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ImageBase()
        {
            DefaultField();

            Context = new ImageContext();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">ІД образу</param>
        /// <param name="name">Назва образу</param>
        public ImageBase(long id, string name)
        {
            DefaultField();

            ID = id;
            Name = name;
        }

        /// <summary>
        /// Конструктор базових елементів образу
        /// </summary>
        /// <param name="id">Ід образу</param>
        /// <param name="name">Назва образу</param>
        /// <param name="context">Контекст</param>
        public ImageBase(long id, string name, ImageContext context)
        {
            DefaultField();

            ID = id;
            Name = name;
            Context = context;
        }

        /// <summary>
        /// Функція задає значення по замовчуванню. Викликається з конструкторів.
        /// </summary>
        private void DefaultField()
        {
            Description = "";
            Synonymy = "";
        }

        /// <summary>
        /// ID образу
        /// </summary>
        public long ID
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
        /// Назва образу
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
        /// Контекст для образу
        /// </summary>
        public ImageContext Context
        {
            get
            {
                return m_Context;
            }

            set
            {
                m_Context = value;
            }
        }

        /// <summary>
        /// Опис
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
        /// Синоніми
        /// </summary>
        public string Synonymy
        {
            get
            {
                return m_Synonymy;
            }

            set
            {
                m_Synonymy = value;
            }
        }

        /// <summary>
        /// Мітка що образ є вказівником
        /// </summary>
        public bool Pointer
        {
            get
            {
                return m_Pointer;
            }

            set
            {
                m_Pointer = value;
            }
        }

        /// <summary>
        /// Мітка що образ є посередником
        /// </summary>
        public bool Intermediate
        {
            get
            {
                return m_Intermediate;
            }

            set
            {
                m_Intermediate = value;
            }
        }

        /// <summary>
        /// Мітка що образ є множина
        /// </summary>
        public bool Plural
        {
            get
            {
                return m_Plural;
            }

            set
            {
                m_Plural = value;
            }
        }

        /// <summary>
        /// Переоприділення ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string contextInfo = "";

            if (this.Context != null)
                if (this.Context.Name != null)
                {
                    contextInfo = this.Context.Name;

                    if (this.Context.Description != null)
                        if (this.Context.Description.Length > 0)
                            contextInfo += " - (" + this.Context.Description + ")";
                }

            return this.Name + (contextInfo.Length > 0 ? " <" + contextInfo + ">" : "");
        }
    }
}
