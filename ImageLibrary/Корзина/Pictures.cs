using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    public class Pictures : PicturesBase
    {
        private List<PicturesBase> m_picturesPictureChild;
        private List<ImageBase> m_picturesImageChild;

        public Pictures()
        {
            m_picturesImageChild = new List<ImageBase>();
            m_picturesPictureChild = new List<PicturesBase>();
        }

        /// <summary>
        /// Колекція картинок
        /// </summary>
        public List<PicturesBase> PicturesPictureChild
        {
            get
            {
                return m_picturesPictureChild;
            }

            set
            {
                m_picturesPictureChild = value;
            }
        }

        /// <summary>
        /// Колекція образів
        /// </summary>
        public List<ImageBase> PicturesImageChild
        {
            get
            {
                return m_picturesImageChild;
            }

            set
            {
                m_picturesImageChild = value;
            }
        }
    }
}
