using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    public class PicturesTemplate
    {
        private string m_Template;
        private int m_ID;

        public PicturesTemplate() { }

        public PicturesTemplate(string templateText)
        {
            this.Template = templateText;
        }

        public PicturesTemplate(int id, string templateText)
        {
            this.ID = id;
            this.Template = templateText;
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
        /// Заготовка
        /// </summary>
        public string Template
        {
            get
            {
                return m_Template;
            }

            set
            {
                m_Template = value;
            }
        }
    }
}
