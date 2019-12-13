using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    public class PicturesBase
    {
        private string m_Name;
        private string m_Description;
        private int m_ID;

        public PicturesBase() { }

        public PicturesBase(string name)
        {
            this.Name = name;
        }

        public PicturesBase(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// ID
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
        /// Назва картинки
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
        /// Опис картинки
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

        public  override string ToString()
        {
            return this.Name;
        }
    }
}
