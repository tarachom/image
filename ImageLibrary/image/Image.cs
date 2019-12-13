using System;
using System.Collections.Generic;

namespace ImageLibrary
{
    /// <summary>
    /// Атрибути образу (максимум 255)
    /// </summary>
    public enum ImageAtribute
    {
        /// <summary>
        /// Атрибут вказує на те що образ є реальним обєктом
        /// </summary>
        Object = 1,

        /// <summary>
        /// Атрибут вказує на то що образ є процесом
        /// </summary>
        Process = 2,

        /// <summary>
        /// Атрибут вказує на то що образ є багатозначним, 
        /// тобто містить вкладені підобрази які повніше розкривають суть образу
        /// </summary>
        Multy = 3,

        /// <summary>
        /// Атрибут вказує на то що образ є абстрактним
        /// </summary>
        Abstract = 4
    }

    /// <summary>
    /// Образ
    /// </summary>
    public class Image: ImageBase
    {
        private List<ImageAtribute> m_atributes;
        private List<ImageBase> m_ingradienty;
        private List<CharacterystykaItem> m_characterystyka;
        private ImageContext m_LinkContext;
        private ImageBase m_PointerImage;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Image()
        {
            m_atributes = new List<ImageAtribute>();
            m_ingradienty = new List<ImageBase>();
            m_characterystyka = new List<CharacterystykaItem>();
        }

        /// <summary>
        /// Список атрибутів
        /// </summary>
        public List<ImageAtribute> Atributes
        {
            get
            {
                return m_atributes;
            }
        }

        /// <summary>
        /// Список характеристик
        /// Тобто список додаткових полів які потрібні для даного образу
        /// </summary>
        public List<CharacterystykaItem> Characterystyka
        {
            get
            {
                return m_characterystyka;
            }
        }

        /// <summary>
        /// Список інградієнтів
        /// </summary>
        public List<ImageBase> Ingradienty
        {
            get
            {
                return m_ingradienty;
            }
        }

        /// <summary>
        /// Контекст на який ссилається образ
        /// Тобто контекст ширше відображає суть даного образу
        /// </summary>
        public ImageContext LinkContext
        {
            get
            {
                return m_LinkContext;
            }

            set
            {
                m_LinkContext = value;
            }
        }

        /// <summary>
        /// Вказівник на образ
        /// </summary>
        public ImageBase PointerImage
        {
            get
            {
                return m_PointerImage;
            }

            set
            {
                m_PointerImage = value;
            }
        }
    }
}
