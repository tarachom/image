using System;

namespace ImageLibrary
{
    /// <summary>
    /// Тип повідомлення журналу реєстрації
    /// </summary>
    public enum EventJournalMessageType
    {
        /// <summary>
        /// Пусто. Просто повідомлення
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Помилка
        /// </summary>
        Error = 1,

        /// <summary>
        /// Попередження
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Інфрмаційне повідомлення
        /// </summary>
        Info = 3
    }

    /// <summary>
    /// Повідомлення журналу реєстрації
    /// </summary>
    public class EventJournalMessage
    {
        private int m_ID;
        private EventJournalMessageType m_EventType;
        private DateTime m_EventDataTime;
        private string m_Message;
        private string m_Description;

        /// <summary>
        /// Конструктор
        /// </summary>
        public EventJournalMessage()
        {
            DefaultValue();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="type">Тип повідомлення</param>
        /// <param name="message">Повідомлення</param>
        /// <param name="description">Опис</param>
        public EventJournalMessage(EventJournalMessageType type, string message, string description = "")
        {
            this.EventDataTime = DateTime.Now;
            this.EventType = type;
            this.Message = message;
            this.Description = description;
        }

        /// <summary>
        /// Значення по замовчуванню
        /// </summary>
        private void DefaultValue()
        {
            this.EventType = EventJournalMessageType.Empty;
            this.EventDataTime = DateTime.Now;
            this.Message = "";
            this.Description = "";
        }

        /// <summary>
        /// Ід повідомлення
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
        /// Тип повідомлення
        /// </summary>
        public EventJournalMessageType EventType
        {
            get
            {
                return m_EventType;
            }

            set
            {
                m_EventType = value;
            }
        }

        /// <summary>
        /// Дата та час повідомлення
        /// </summary>
        public DateTime EventDataTime
        {
            get
            {
                return m_EventDataTime;
            }

            set
            {
                m_EventDataTime = value;
            }
        }

        /// <summary>
        /// Повідомлення
        /// </summary>
        public string Message
        {
            get
            {
                return m_Message;
            }

            set
            {
                m_Message = value;
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
    }
}
