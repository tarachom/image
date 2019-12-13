using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ImageLibrary
{
    /// <summary>
    /// База даних MySql
    /// </summary>
    public class _MySqlDataBase : IDataBase
    {
        private string m_ConnectString;
        private MySqlConnection m_Connect;

        /// <summary>
        /// Тип відбору
        /// </summary>
        private enum GetByVariant
        {
            /// <summary>
            /// Відбір по ІД
            /// </summary>
            ID,

            /// <summary>
            /// Відбір по назві
            /// </summary>
            Name,

            /// <summary>
            /// Пошук по назві
            /// </summary>
            SearchName,

            /// <summary>
            /// Відбір по контексті
            /// </summary>
            Context
        }

        #region EVENTS

        //Собитие при ошибке
        public event DataBaseStateHandler DataBaseExceptionEvent;

        //Собитие при добавленні нового образу
        public event DataBaseStateHandler AddedNewImageEvent;

        //Собитие при видаленні образу
        public event DataBaseStateHandler DeleteImageEvent;

        //Собитие при обновленні образу
        public event DataBaseStateHandler UpdateImageEvent;

        //Собитие при пошуку образу по назві або по ІД
        public event DataBaseStateHandler GetByNameOrIDImageEvent;

        #endregion

        #region CONNECT CLOSE

        /// <summary>
        /// Строка підключення
        /// </summary>
        public string ConnectString
        {
            get
            {
                return m_ConnectString;
            }
            set
            {
                m_ConnectString = value;
            }
        }

        /// <summary>
        /// Підключення до бази даних
        /// </summary>
        /// <returns>true якщо ок</returns>
        public bool Connect()
        {
            this.m_Connect = new MySqlConnection(this.ConnectString);

            try
            {
                this.m_Connect.Open();
                return true;
            }
            catch (MySqlException e)
            {
                if (DataBaseExceptionEvent != null)
                    DataBaseExceptionEvent(this, new DataBaseEventArgs("Проблема при підключенні", e.Message));

                return false;
            }
        }

        /// <summary>
        /// Закриття підключення
        /// </summary>
        /// <returns>true якщо ок</returns>
        public bool Close()
        {
            try
            {
                this.m_Connect.Close();
                return true;
            }
            catch (MySqlException e)
            {
                if (DataBaseExceptionEvent != null)
                    DataBaseExceptionEvent(this, new DataBaseEventArgs("Проблема при закритті підключення", e.Message));

                return false;
            }
        }

        #endregion

        #region EVENT_JOURNAL

        /// <summary>
        /// Функція загружає повідомлення з журналу
        /// </summary>
        /// <param name="messList">Список для повідомлень</param>
        public void LoadAllEventJournal(List<EventJournalMessage> messList)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            MySqlDataReader reader;

            myCommand.CommandText = "SELECT `id`, `datatime`, `event_type`, `event_message`, `event_description` " +
                                    "FROM `event_journal` " +
                                    "ORDER BY `id` DESC " +
                                    "LIMIT 50";

            try
            {
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    EventJournalMessage messItem = new EventJournalMessage();

                    messItem.ID = int.Parse(reader["id"].ToString());
                    messItem.EventDataTime = (DateTime)reader["datatime"];
                    messItem.EventType = (EventJournalMessageType)(int.Parse(reader["event_type"].ToString()));
                    messItem.Message = reader["event_message"].ToString();
                    messItem.Description = reader["event_description"].ToString();

                    messList.Add(messItem);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                //...
            }
        }

        #endregion

        #region INSERT UPDATE DELETE IMAGE

        /// <summary>
        /// Записує новий образ у базу
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>true якщо ок</returns>
        public bool InsertImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            // Добавлення образу `image`
            myCommand.CommandText = "INSERT INTO `image` (`Name`, `Context`, `Description`, `Synonymy`, `LinkContext`, `Pointer`, `Plural`, `Intermediate`, `PointerImage`) " +
                                    "VALUE (@Name, @Context, @Description, @Synonymy, @LinkContext, @Pointer, @Plural, @Intermediate, @PointerImage)";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Name", image.Name);
            myCommand.Parameters.AddWithValue("@Context", image.Context.ID);
            myCommand.Parameters.AddWithValue("@Description", image.Description);
            myCommand.Parameters.AddWithValue("@Synonymy", image.Synonymy);
            myCommand.Parameters.AddWithValue("@LinkContext", (image.LinkContext != null ? image.LinkContext.ID : 0));
            myCommand.Parameters.AddWithValue("@Pointer", image.Pointer);
            myCommand.Parameters.AddWithValue("@Plural", image.Plural);
            myCommand.Parameters.AddWithValue("@Intermediate", image.Intermediate);
            myCommand.Parameters.AddWithValue("@PointerImage", (image.PointerImage != null ? image.PointerImage.ID : 0));

            try
            {
                myCommand.ExecuteNonQuery();
                //Ід новоствореного елементу
                image.ID = myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі нового образу", e.Message);

                return false;
            }

            // Добавлення атрибутів `image_atributes`
            foreach (ImageAtribute atribute in image.Atributes)
            {

                myCommand.CommandText = "INSERT INTO `image_atributes` (`Atribute`, `Image`) VALUE (@Atribute, @Image)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Atribute", atribute);
                myCommand.Parameters.AddWithValue("@Image", image.ID.ToString());

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі атрибута образу (" + atribute.ToString() + ")", e.Message);

                    return false;
                }
            }

            // Добавлення `image_characterystyka`
            foreach (CharacterystykaItem characterystyka in image.Characterystyka)
            {
                myCommand.CommandText = "INSERT INTO `image_characterystyka` (`Image`, `Name`, `Value`, `Code`) VALUE (@Image, @Name, @Value, @Code)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Image", image.ID);
                myCommand.Parameters.AddWithValue("@Name", characterystyka.ItemName);
                myCommand.Parameters.AddWithValue("@Value", characterystyka.ItemValue);
                myCommand.Parameters.AddWithValue("@Code", characterystyka.Code);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі характеристики образу (" + characterystyka.ItemName + ")", e.Message);

                    return false;
                }
            }

            // Добавлення `image_ingradienty`
            foreach (ImageBase ingradient in image.Ingradienty)
            {

                myCommand.CommandText = "INSERT INTO `image_ingradienty` (`Image`, `Ingradient`) VALUE (@Image, @Ingradient)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Image", image.ID);
                myCommand.Parameters.AddWithValue("@Ingradient", ingradient.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі інградієнту образу (" + ingradient.Name + ")", e.Message);

                    return false;
                }
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі тразакції", e.Message);

                return false;
            }

            if (AddedNewImageEvent != null)
                AddedNewImageEvent(this, new DataBaseEventArgs("Добавлений новий образ в базу данних", image.Name));

            InfoReporting("Добавлений новий образ в базу данних: ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        /// <summary>
        /// Обновляє образ у базі
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>true якщо ок</returns>
        public bool UpdateImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            //
            // Обновлення самого образу
            //

            myCommand.CommandText = "UPDATE `Image` SET " +
                                           "`Name` = @Name, `Context` = @Context, " +
                                           "`Description` = @Description, `Synonymy` = @Synonymy, " +
                                           "`LinkContext` = @LinkContext, " +
                                           "`Pointer` = @Pointer, `Plural` = @Plural, `Intermediate` = @Intermediate, " +
                                           "`PointerImage` = @PointerImage " +
                                    "WHERE `ID` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);
            myCommand.Parameters.AddWithValue("@Name", image.Name);
            myCommand.Parameters.AddWithValue("@Context", image.Context.ID);
            myCommand.Parameters.AddWithValue("@Description", image.Description);
            myCommand.Parameters.AddWithValue("@Synonymy", image.Synonymy);
            myCommand.Parameters.AddWithValue("@LinkContext", (image.LinkContext != null ? image.LinkContext.ID : 0));
            myCommand.Parameters.AddWithValue("@Pointer", image.Pointer);
            myCommand.Parameters.AddWithValue("@Plural", image.Plural);
            myCommand.Parameters.AddWithValue("@Intermediate", image.Intermediate);
            myCommand.Parameters.AddWithValue("@PointerImage", (image.PointerImage != null ? image.PointerImage.ID : 0));

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при обновленні самого образу " + image.Name, e.Message);

                return false;
            }

            //
            // Обновлення Атрибутів
            //

            //Видалення всіх атрибутів образу
            myCommand.CommandText = "DELETE FROM `image_atributes` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні атрибутів для образу " + image.Name, e.Message);

                return false;
            }

            // Запис нових атрибутів `image_atributes`
            foreach (ImageAtribute atribute in image.Atributes)
            {
                myCommand.CommandText = "INSERT INTO `image_atributes` (`Atribute`, `Image`) VALUE (@Atribute, @ImageID) ";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Atribute", atribute);
                myCommand.Parameters.AddWithValue("@ImageID", image.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі атрибута образу (" + atribute.ToString() + ")", e.Message);

                    return false;
                }
            }

            //
            // Обновлення Характеристик
            //

            //Видалення всіх характеристик образу
            myCommand.CommandText = "DELETE FROM `image_characterystyka` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні характеристик для образу " + image.Name, e.Message);

                return false;
            }

            // Запис нових характеристик `image_characterystyka`
            foreach (CharacterystykaItem characterystyka in image.Characterystyka)
            {
                myCommand.CommandText = "INSERT INTO `image_characterystyka` (`Image`, `Name`, `Value`, `Code`) " +
                                        "VALUE (@ImageID, @Name, @Value, @Code) ";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@ImageID", image.ID);
                myCommand.Parameters.AddWithValue("@Name", characterystyka.ItemName);
                myCommand.Parameters.AddWithValue("@Value", characterystyka.ItemValue);
                myCommand.Parameters.AddWithValue("@Code", characterystyka.Code);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі характеристики образу (" + characterystyka.ItemName + ")", e.Message);

                    return false;
                }
            }

            //
            // Обновлення Інградієнтів
            //

            //Видалення всіх Інградієнтів образу
            myCommand.CommandText = "DELETE FROM `image_ingradienty` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні інградієнтів для образу " + image.Name, e.Message);

                return false;
            }

            // Запис нових інградієнтів образу `image_ingradienty`
            foreach (ImageBase ingradient in image.Ingradienty)
            {
                myCommand.CommandText = "INSERT INTO `image_ingradienty` (`Image`, `Ingradient`) VALUE (@ImageID, @Ingradient)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@ImageID", image.ID);
                myCommand.Parameters.AddWithValue("@Ingradient", ingradient.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі інградієнту образу (" + ingradient.Name + ")", e.Message);

                    return false;
                }
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі трзакції", e.Message);

                return false;
            }

            if (UpdateImageEvent != null)
                UpdateImageEvent(this, new DataBaseEventArgs("Обновлений образ ", image.Name));

            InfoReporting("Обновлений образ: ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        /// <summary>
        /// Функція видаляє образ та всі його складові та звязки
        /// </summary>
        /// <param name="image">Образ</param>
        /// <returns>true якщо вдалось видалити</returns>
        public bool DeleteImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //Відбір по ІД образу
            myCommand.Parameters.AddWithValue("@Image", image.ID.ToString());

            //Перевірити чи є ссилки на образ, видаляти тільки якщо на образ ніхто не ссилається
            try
            {
                //Шукаємо чи містять інші образи ссилку на наш образ в таблиці Інградієнти
                //Тобто чи виступає наш образ в ролі інградієнту для інших образів
                myCommand.CommandText = "SELECT count(`Image`) FROM `image_ingradienty` WHERE `Ingradient` = @image";

                if (int.Parse(myCommand.ExecuteScalar().ToString()) > 0)
                {
                    ErrorReporting("Видалити неможливо так як образ виступає в ролі інградієнту для інших образів", "");

                    return false;
                }

                //Тут можна зробити ще перевірки
                //...
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема обчисленні ссилок", e.Message);

                return false;
            }

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            // Видалення Атрибутів
            try
            {
                myCommand.CommandText = "DELETE FROM `image_atributes` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні атрибутів для образу " + image.Name, e.Message);

                return false;
            }

            // Видалення Характеристик
            try
            {
                myCommand.CommandText = "DELETE FROM `image_characterystyka` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні характеристик для образу " + image.Name, e.Message);

                return false;
            }

            // Видалення Інградієнтів
            try
            {
                myCommand.CommandText = "DELETE FROM `image_ingradienty` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні інградієнтів для образу " + image.Name, e.Message);

                return false;
            }

            // Видалення самого образу
            try
            {
                myCommand.CommandText = "DELETE FROM `image` WHERE `ID` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні образу " + image.Name, e.Message);

                return false;
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі тразакції", e.Message);

                return false;
            }

            if (DeleteImageEvent != null)
                DeleteImageEvent(this, new DataBaseEventArgs("Видалений образ '" + image.Name + "' з бази данних", ""));

            InfoReporting("Видалений образ з бази даних ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        #endregion

        #region PRIVAT FUNCTION

        /// <summary>
        /// Функція заповнює всі колекції для списку образів
        /// </summary>
        /// <param name="imageItemList">Список образів для яких треба заповнити колекції</param>
        /// <returns>true якщо все ок</returns>
        private bool FillImageCollections(List<Image> imageItemList)
        {
            if (imageItemList.Count == 0)
                return false;

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            MySqlDataReader reader;

            foreach (Image image in imageItemList)
            {
                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Image", image.ID.ToString());

                //
                //Атрибути
                //

                myCommand.CommandText = "SELECT `Atribute` FROM `image_atributes` WHERE `Image` = @Image";

                try
                {
                    reader = myCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        int artibute_index = int.Parse(reader["Atribute"].ToString());
                        image.Atributes.Add((ImageAtribute)artibute_index);
                    }
                    reader.Close();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема: Невдалось зчитати атрибути для образу", e.Message);

                    return false;
                }

                //
                //Характеристика
                //

                myCommand.CommandText = "SELECT `Name`, `Value`, `Code` FROM `image_characterystyka` WHERE `Image` = @Image";

                try
                {
                    reader = myCommand.ExecuteReader();
                    while (reader.Read())
                        image.Characterystyka.Add(
                            new CharacterystykaItem(reader["Name"].ToString(), reader["Value"].ToString(), reader["Code"].ToString())
                        );
                    reader.Close();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема: Невдалось зчитати характеристики для образу", e.Message);

                    return false;
                }

                //
                //Інградієнти
                //

                string ingradienty_qwery_build = "";
                int ingradienty_count = 0;

                myCommand.CommandText = "SELECT `Ingradient` FROM `image_ingradienty` WHERE `Image` = @Image";

                try
                {
                    reader = myCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        ingradienty_qwery_build += (ingradienty_count > 0 ? "," : "") + reader["Ingradient"].ToString();
                        ingradienty_count++;
                    }
                    reader.Close();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема: Невдалось зчитати інградієнти для образу", e.Message);

                    return false;
                }

                if (ingradienty_count > 0)
                {
                    if (ingradienty_count == 1)
                        ingradienty_qwery_build = " `image`.`ID` = " + ingradienty_qwery_build;
                    else
                        ingradienty_qwery_build = " `image`.`ID` IN (" + ingradienty_qwery_build + ")";

                    myCommand.CommandText = "SELECT `image`.`ID`, `image`.`Name`, `image`.`Context`, " +
                                            "       `image`.`Description`, `image`.`Synonymy`, " +
                                            "       `image_context`.`Name` as `ContextName`, " +
                                            "       `image_context`.`Description` as `ContexDesc` " +
                                            "FROM `image` " +
                                            "     LEFT JOIN `image_context` ON `image_context`.`ID` = `image`.`Context` " +
                                            "WHERE " + ingradienty_qwery_build;

                    myCommand.Parameters.Clear();

                    try
                    {
                        reader = myCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            ImageBase imageBase = new ImageBase(
                                long.Parse(reader["ID"].ToString()),
                                reader["Name"].ToString(),
                                new ImageContext(int.Parse(reader["Context"].ToString()),
                                                 reader["ContextName"].ToString(),
                                                 reader["ContexDesc"].ToString()));

                            imageBase.Description = reader["Description"].ToString();
                            imageBase.Synonymy = reader["Synonymy"].ToString();

                            image.Ingradienty.Add(imageBase);
                        }
                        reader.Close();
                    }
                    catch (MySqlException e)
                    {
                        ErrorReporting("Проблема: Невдалось зчитати інградієнти для образу", e.Message);

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Функція для пошуку образу по Ід або по назві в межах контексту, або просто в межах контексту
        /// </summary>
        /// <param name="variant">Опридялє тип пошуку: ID, Name, Context</param>
        /// <param name="whereValue">Значення критерію відбору</param>
        /// <param name="whereContextID">Відбір в межах контексту</param>
        /// <returns>Повертає список знайдених образів</returns>
        private List<Image> GetImageByVariant(GetByVariant variant, string whereValue, int whereContextID = 0)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            MySqlDataReader reader;
            List<Image> imageList = new List<Image>();

            myCommand.CommandText = "SELECT " +
                                    "`image`.`ID`, `image`.`Name`, `image`.`Context`, " +
                                    "`image`.`Description`, `image`.`Synonymy`, `image`.`LinkContext`, " +
                                    "`image`.`Pointer`, `image`.`Plural`, `image`.`Intermediate`, " +
                                    "`image_context`.`Name` as `ContextName`, " +
                                    "`image_context`.`Description` as `ContexDesc`, " +
                                    "`image_linkcontext`.`Name` AS `LinkContextName`, " +
                                    "`image_linkcontext`.`Description` AS `LinkContextDesc`, " +
                                    "`image`.`PointerImage`, `image_pointer`.`Name` AS `PointerName`" +
                                    "FROM `image` " +
                                    "LEFT JOIN `image_context` ON `image_context`.`ID` = `image`.`Context` " +
                                    "LEFT JOIN `image_context` AS `image_linkcontext` ON `image_linkcontext`.`ID` = `image`.`LinkContext` " +
                                    "LEFT JOIN `image` AS `image_pointer` ON `image_pointer`.`ID` = `image`.`PointerImage` ";

            if (variant == GetByVariant.ID)
            {
                myCommand.CommandText += "WHERE `image`.`ID` = @ID ";
                myCommand.Parameters.AddWithValue("@ID", whereValue);
            }
            else if (variant == GetByVariant.Name)
            {
                myCommand.CommandText += "WHERE `image`.`Name` = @Name ";
                myCommand.Parameters.AddWithValue("@Name", whereValue.Trim());

                if (whereContextID > 0)
                {
                    myCommand.CommandText += "AND `image`.`Context` = @Context ";
                    myCommand.Parameters.AddWithValue("@Context", whereContextID);
                }
            }
            else if (variant == GetByVariant.Context)
            {
                if (whereContextID > 0)
                {
                    myCommand.CommandText += "WHERE `image`.`Context` = @Context ";
                    myCommand.Parameters.AddWithValue("@Context", whereContextID);
                }
            }
            else
                throw new Exception("Невірно заданий критерій пошуку Images");

            try
            {
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    Image imageItem = new Image();

                    imageItem.ID = long.Parse(reader["ID"].ToString());
                    imageItem.Name = reader["Name"].ToString();
                    imageItem.Description = reader["Description"].ToString();
                    imageItem.Synonymy = reader["Synonymy"].ToString();

                    //Контекст
                    imageItem.Context = new ImageContext(int.Parse(reader["Context"].ToString()));
                    imageItem.Context.Name = reader["ContextName"].ToString();
                    imageItem.Context.Description = reader["ContexDesc"].ToString();

                    //Ссилка на контекст
                    int LinkContextID = int.Parse(reader["LinkContext"].ToString());

                    if (LinkContextID > 0)
                        imageItem.LinkContext = new ImageContext(
                            LinkContextID,
                            reader["LinkContextName"].ToString(),
                            reader["LinkContextDesc"].ToString());

                    //Мітки
                    imageItem.Pointer = bool.Parse(reader["Pointer"].ToString());
                    imageItem.Plural = bool.Parse(reader["Plural"].ToString());
                    imageItem.Intermediate = bool.Parse(reader["Intermediate"].ToString());

                    //Вказівник
                    long PointerImage = long.Parse(reader["PointerImage"].ToString());

                    if (PointerImage > 0)
                        imageItem.PointerImage = new ImageBase(PointerImage, reader["PointerName"].ToString());

                    imageList.Add(imageItem);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема: Невдалось зчитати образ(и)", e.Message);

                return null;
            }

            //Заповнити колекції
            if (FillImageCollections(imageList))
                return imageList;
            else
                return new List<Image>();
        }

        /// <summary>
        /// Функція для пошуку базового образу по Ід або по назві в межах контексту, або просто в межах контексту
        /// </summary>
        /// <param name="imageBaseList">Список куди будуть добавлені знайдені образи</param>
        /// <param name="variant">Опридялє тип пошуку: ID, Name, Context</param>
        /// <param name="whereValue">Значення критерію відбору</param>
        /// <param name="whereContextID">Відбір в межах контексту</param>
        /// <returns>Повертає список знайдених образів</returns>
        private bool GetImageBaseByVariant(List<ImageBase> imageBaseList, GetByVariant variant, string whereValue, int whereContextID = 0, int limit = 0)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            MySqlDataReader reader;

            myCommand.CommandText = "SELECT `image`.`ID`, `image`.`Name`, `image`.`Context`, " +
                                    "       `image`.`Description`, `image`.`Synonymy`, " +
                                    "       `image`.`Pointer`, `image`.`Plural`, `image`.`Intermediate`, " +
                                    "       `image_context`.`Name` as `ContextName`, " +
                                    "       `image_context`.`Description` as `ContexDesc` " +
                                    "FROM `image` " +
                                    "     LEFT JOIN `image_context` ON `image_context`.`ID` = `image`.`Context` ";

            if (variant == GetByVariant.ID)
            {
                myCommand.CommandText += "WHERE `image`.`ID` = @ID ";
                myCommand.Parameters.AddWithValue("@ID", whereValue);
            }
            else if (variant == GetByVariant.Name)
            {
                myCommand.CommandText += "WHERE `image`.`Name` = @Name ";
                myCommand.Parameters.AddWithValue("@Name", whereValue.Trim());

                if (whereContextID > 0)
                {
                    myCommand.CommandText += "AND `image`.`Context` = @Context ";
                    myCommand.Parameters.AddWithValue("@Context", whereContextID);
                }
            }
            else if (variant == GetByVariant.SearchName)
            {
                myCommand.CommandText += "WHERE `image`.`Name` LIKE @Name ";
                myCommand.Parameters.AddWithValue("@Name", whereValue.Trim());

                if (whereContextID > 0)
                {
                    myCommand.CommandText += "AND `image`.`Context` = @Context ";
                    myCommand.Parameters.AddWithValue("@Context", whereContextID);
                }
            }
            else if (variant == GetByVariant.Context)
            {
                myCommand.CommandText += "WHERE `image`.`Context` = @Context ";
                myCommand.Parameters.AddWithValue("@Context", whereContextID);
            }
            else
                throw new Exception("Невірно заданий критерій пошуку ImageBase");

            if (limit > 0)
                myCommand.CommandText += " LIMIT " + limit.ToString();

            try
            {
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    ImageBase imageBase = new ImageBase();

                    imageBase.ID = long.Parse(reader["ID"].ToString());
                    imageBase.Name = reader["Name"].ToString();
                    imageBase.Description = reader["Description"].ToString();
                    imageBase.Synonymy = reader["Synonymy"].ToString();

                    //Контекст
                    imageBase.Context = new ImageContext(
                        int.Parse(reader["Context"].ToString()),
                        reader["ContextName"].ToString(),
                        reader["ContexDesc"].ToString()
                    );

                    //Мітки
                    imageBase.Pointer = bool.Parse(reader["Pointer"].ToString());
                    imageBase.Plural = bool.Parse(reader["Plural"].ToString());
                    imageBase.Intermediate = bool.Parse(reader["Intermediate"].ToString());

                    imageBaseList.Add(imageBase);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку бази для образу по Назві або по Ід", e.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Функція шукає контекст
        /// </summary>
        /// <param name="variant">Варіант пошуку</param>
        /// <param name="whereValue">Значення для відбору</param>
        /// <returns>Контекст or null</returns>
        private ImageContext GetImageContextBy_ID_or_Name(GetByVariant variant, string whereValue)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name`, `Description` FROM `image_context` ";

            if (variant == GetByVariant.ID)
            {
                myCommand.CommandText += "WHERE `ID` = @ContextID ";
                myCommand.Parameters.AddWithValue("@ContextID", whereValue);
            }
            else if (variant == GetByVariant.Name)
            {
                myCommand.CommandText += "WHERE `Name` = @Name ";
                myCommand.Parameters.AddWithValue("@Name", whereValue);
            }
            else
                throw new Exception("Невірно заданий критерій пошуку Images");

            ImageContext imageContext = null;

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    imageContext = new ImageContext();

                    imageContext.ID = int.Parse(reader["ID"].ToString());
                    imageContext.Name = reader["Name"].ToString();
                    imageContext.Description = reader["Description"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку контексту по ІД", e.Message);
                return null;
            }

            return imageContext;
        }

        /// <summary>
        /// Функція записує в журнал нове повідомлення
        /// </summary>
        /// <param name="message">Повідомлення</param>
        private void Write_EventJournalMessage(EventJournalMessage message)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "INSERT INTO `event_journal` (`datatime`, `event_type`, `event_message`, `event_description`) " +
                                    "VALUES (@DataTime, @EventType, @EventMessage, @EventDescription)";

            myCommand.Parameters.AddWithValue("@DataTime", message.EventDataTime);
            myCommand.Parameters.AddWithValue("@EventType", message.EventType);
            myCommand.Parameters.AddWithValue("@EventMessage", message.Message);
            myCommand.Parameters.AddWithValue("@EventDescription", message.Description);

            try
            {
                myCommand.ExecuteNonQuery();
                message.ID = (int)myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                //
            }
        }

        /// <summary>
        /// Обгортка для повідомлень про помилки
        /// </summary>
        /// <param name="messsage">Повідомлення</param>
        /// <param name="description">Опис</param>
        private void ErrorReporting(string messsage, string description)
        {
            Write_EventJournalMessage(new EventJournalMessage(EventJournalMessageType.Error, messsage, description));

            if (DataBaseExceptionEvent != null)
                DataBaseExceptionEvent(this, new DataBaseEventArgs(messsage, description));
        }

        /// <summary>
        /// Обгортка для інформаційних повідомлень
        /// </summary>
        /// <param name="messsage"></param>
        private void InfoReporting(string messsage)
        {
            Write_EventJournalMessage(new EventJournalMessage(EventJournalMessageType.Info, messsage));
        }

        #endregion

        #region GET IMAGE, IMAGEBASE

        /// <summary>
        /// Пошук образу по назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <returns>Образ або null</returns>
        public List<Image> GetImageByName(string Name, int whereContextID = 0)
        {
            return GetImageByVariant(GetByVariant.Name, Name, whereContextID);
        }

        /// <summary>
        ///  Пошук образу по ID
        /// </summary>
        /// <param name="ID">ID образу</param>
        /// <returns>Образ або null</returns>
        public Image GetImageByID(long ID)
        {
            List<Image> result = GetImageByVariant(GetByVariant.ID, ID.ToString());

            if (result != null)
                if (result.Count > 0)
                    return result[0];

            return null;
        }

        /// <summary>
        /// Функція отримує список всіх образів
        /// </summary>
        /// <returns>Список образів або null</returns>
        public List<Image> LoadAllImage()
        {
            return GetImageByVariant(GetByVariant.Context, "", 0);
        }

        /// <summary>
        /// Функція отримує список всіх образів в розрізі контексту
        /// </summary>
        /// <param name="whereContextID">Ід контексту</param>
        /// <returns>Список із результатами або нулл</returns>
        public List<Image> LoadAllImageByContext(int whereContextID)
        {
            return GetImageByVariant(GetByVariant.Context, "", whereContextID);
        }

        /// <summary>
        /// Пошук бази для образу по ID
        /// </summary>
        /// <param name="ID">Значення для пошуку</param>
        /// <returns>Список базових обраів або null</returns>
        public List<ImageBase> GetListImageBaseByID(long ID)
        {
            List<ImageBase> imageBase = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBase, GetByVariant.ID, ID.ToString()))
                return imageBase;
            else
                return null;
        }

        /// <summary>
        /// Пошук бази для образу по Назві
        /// </summary>
        /// <param name="Name">Значення для пошуку</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Список базових обраів або null</returns>
        public List<ImageBase> GetListImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            List<ImageBase> imageBaseList = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBaseList, GetByVariant.Name, Name, whereContextID, limit))
                return imageBaseList;
            else
                return null;
        }

        /// <summary>
        /// Пошук базових образів по назві
        /// </summary>
        /// <param name="Name">Назва образу</param>
        /// <param name="whereContextID">ІД контексту</param>
        /// <param name="limit">Ліміт</param>
        /// <returns>Список знайдених образів</returns>
        public List<ImageBase> SearchImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            List<ImageBase> imageBaseList = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBaseList, GetByVariant.SearchName, Name, whereContextID, limit))
                return imageBaseList;
            else
                return null;
        }

        /// <summary>
        /// Загружає в список всі образи тільки з базовими полями
        /// </summary>
        /// <param name="imageList">Список для результатів</param>
        /// <param name="whereContextID">Ід контексту</param>
        /// <param name="limit">Ліміт</param>
        public void LoadAllImageBase(List<ImageBase> imageList, int whereContextID = 0, int limit = 0)
        {
            GetImageBaseByVariant(imageList, GetByVariant.Context, "", whereContextID, limit);
        }

        #endregion

        #region CONTEXT

        /// <summary>
        /// Функція добавляє новий контекст
        /// </summary>
        /// <param name="context"></param>
        /// <returns>true якщо все ок</returns>
        public bool InsertContext(ImageContext context)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //
            //Якщо явно вказаний первинний ключ для контексту тоді пробуємо створити такий елемент з оприділеним первинним ключем.
            //

            if (context.ID == 0)
            {
                myCommand.CommandText = "INSERT INTO `image_context` (`Name`, `Description`) VALUE (@Name, @Description)";
            }
            else
            {
                myCommand.CommandText = "INSERT INTO `image_context` (`ID`, `Name`, `Description`) VALUE (@ID, @Name, @Description)";
                myCommand.Parameters.AddWithValue("@ID", context.ID);
            }

            myCommand.Parameters.AddWithValue("@Name", context.Name);
            myCommand.Parameters.AddWithValue("@Description", context.Description);

            try
            {
                myCommand.ExecuteNonQuery();

                //Ід новоствореного елементу
                context.ID = (int)myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі нового контексту", e.Message);

                return false;
            }

            InfoReporting("Добавлений новий контекст: ID = " + context.ID.ToString() + ", Name = " + context.Name);

            return true;
        }

        /// <summary>
        /// Функція обновляє контекст
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <returns>true якщо все ок</returns>
        public bool UpdateContext(ImageContext context)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Обновлення контексту
            myCommand.CommandText = "UPDATE `image_context` SET `Name` = @Name, `Description` = @Description WHERE `ID` = @ContextID";

            myCommand.Parameters.AddWithValue("@Name", context.Name);
            myCommand.Parameters.AddWithValue("@Description", context.Description);
            myCommand.Parameters.AddWithValue("@ContextID", context.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при обновленні контексту", e.Message);

                return false;
            }

            InfoReporting("Обновлений контекст: ID = " + context.ID.ToString() + ", Name = " + context.Name);

            return true;
        }

        /// <summary>
        /// Функція видаляє контекст
        /// </summary>
        /// <param name="contextID">Контекст</param>
        /// <returns>true якщо все ок</returns>
        public bool DeleteContext(int contextID)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Видалення контексту
            myCommand.CommandText = "DELETE FROM `image_context` WHERE `ID` = @ContextID";

            myCommand.Parameters.AddWithValue("@ContextID", contextID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при видаленні контексту", e.Message);

                return false;
            }

            InfoReporting("Видалений контекст: ID = " + contextID.ToString());

            return true;
        }

        /// <summary>
        /// Функція шукає контекст по ІД
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Контекст або нулл</returns>
        public ImageContext GetImageContexByID(int ID)
        {
            return GetImageContextBy_ID_or_Name(GetByVariant.ID, ID.ToString());
        }

        /// <summary>
        /// Функція шукає контекст по Назві
        /// </summary>
        /// <param name="ContextName"></param>
        /// <returns>Контекст або нулл</returns>
        public ImageContext GetImageContextByName(string ContextName)
        {
            return GetImageContextBy_ID_or_Name(GetByVariant.Name, ContextName);
        }

        /// <summary>
        /// Функція загружає в список всі контексти
        /// </summary>
        /// <param name="listContext">Список для результатів</param>
        public void LoadAllContextList(List<ImageContext> listContext)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name`, `Description` " +
                                    "FROM `image_context` " +
                                    "ORDER BY `ID`";
            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    ImageContext context = new ImageContext();

                    context.ID = int.Parse(reader["ID"].ToString());
                    context.Name = reader["Name"].ToString();
                    context.Description = reader["Description"].ToString();

                    listContext.Add(context);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при вибірці списку контекстів", e.Message);
            }
        }

        #endregion

        #region PICTURES

        public bool AddPictureTemplate(PicturesTemplate PicturesTemplate)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "INSERT INTO `pictures_template`(`Template`) VALUES(@Template)";
            myCommand.Parameters.AddWithValue("@Template", PicturesTemplate.Template);

            try
            {
                myCommand.ExecuteNonQuery();
                PicturesTemplate.ID = (int)myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема запису заготовки для Pictures", e.Message);

                return false;
            }

            return true;
        }

        public bool DeletePictureTemplate(int PicturesTemplateID)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "DELETE FROM `pictures_template` WHERE `ID` = @ID";
            myCommand.Parameters.AddWithValue("@ID", PicturesTemplateID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема видалення заготовки для Pictures", e.Message);

                return false;
            }

            return true;
        }

        public void LoadAllPicturesTemplateList(List<PicturesTemplate> templateList)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Template` FROM `pictures_template`";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    PicturesTemplate pictureTemplate = new PicturesTemplate(
                        int.Parse(reader["ID"].ToString()),
                        reader["Template"].ToString());

                    templateList.Add(pictureTemplate);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема вибірки заготовок для Pictures", e.Message);
            }
        }

        public void LoadAllPicturesBaseList(List<PicturesBase> picturesBaseList)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name` FROM `pictures` ORDER BY `ID`";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    PicturesBase picturesBaseItem = new PicturesBase(
                        int.Parse(reader["ID"].ToString()),
                        reader["Name"].ToString());

                    picturesBaseList.Add(picturesBaseItem);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема вибірки PicturesBase", e.Message);
            }
        }

        public bool InsertPicture(Pictures picture)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            myCommand.CommandText = "INSERT INTO `pictures`(`Name`,`Description`) VALUES(@Name, @Description)";
            myCommand.Parameters.AddWithValue("@Name", picture.Name);
            myCommand.Parameters.AddWithValue("@Description", picture.Description);

            try
            {
                myCommand.ExecuteNonQuery();
                picture.ID = (int)myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема запису нового Pictures", e.Message);

                return false;
            }

            // Колекції Pictures
            foreach (PicturesBase picturesCollectionElement in picture.PicturesPictureChild)
            {

                myCommand.CommandText = "INSERT INTO `pictures_picturechild` (`Picture`, `PictureChild`) VALUE(@Picture, @PictureChild)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Picture", picture.ID);
                myCommand.Parameters.AddWithValue("@PictureChild", picturesCollectionElement.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі колекцій малюнків малюнка", e.Message);

                    return false;
                }
            }

            // Колекції Images
            foreach (ImageBase imageCollectionElement in picture.PicturesImageChild)
            {

                myCommand.CommandText = "INSERT INTO `pictures_imagechild` (`Picture`, `ImageChild`) VALUE(@Picture, @ImageChild)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Picture", picture.ID);
                myCommand.Parameters.AddWithValue("@ImageChild", imageCollectionElement.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі колекцій образів малюнка", e.Message);

                    return false;
                }
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі тразакції", e.Message);

                return false;
            }

            InfoReporting("Добавлений новий малюнок: ID = " + picture.ID.ToString() + ", Name = " + picture.Name);

            return true;
        }

        public bool UpdatePicture(Pictures picture)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            myCommand.CommandText = "UPDATE `pictures` SET `Name` = @Name, `Description` = @Description WHERE `ID` = @ID";

            myCommand.Parameters.AddWithValue("@Name", picture.Name);
            myCommand.Parameters.AddWithValue("@Description", picture.Description);
            myCommand.Parameters.AddWithValue("@ID", picture.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема обновлення запису Pictures", e.Message);

                return false;
            }

            //Очисти старі колекції Pictures
            myCommand.CommandText = "DELETE FROM `pictures_picturechild` WHERE `Picture` = @Picture";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Picture", picture.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні колекцій для малюнка", e.Message);

                return false;
            }

            //Нові Колекції Pictures
            foreach (PicturesBase picturesCollectionElement in picture.PicturesPictureChild)
            {

                myCommand.CommandText = "INSERT INTO `pictures_picturechild` (`Picture`, `PictureChild`) VALUE(@Picture, @PictureChild)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Picture", picture.ID);
                myCommand.Parameters.AddWithValue("@PictureChild", picturesCollectionElement.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі колекцій малюнків малюнка", e.Message);

                    return false;
                }
            }

            //Очисти старі колекції Images
            myCommand.CommandText = "DELETE FROM `pictures_imagechild` WHERE `Picture` = @Picture";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Picture", picture.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Помилка при видаленні колекцій для малюнка", e.Message);

                return false;
            }

            //Нові Колекції Images
            foreach (ImageBase imageCollectionElement in picture.PicturesImageChild)
            {

                myCommand.CommandText = "INSERT INTO `pictures_imagechild` (`Picture`, `ImageChild`) VALUE(@Picture, @ImageChild)";

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@Picture", picture.ID);
                myCommand.Parameters.AddWithValue("@ImageChild", imageCollectionElement.ID);

                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    ErrorReporting("Проблема при записі колекцій образів малюнка", e.Message);

                    return false;
                }
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі тразакції", e.Message);

                return false;
            }

            InfoReporting("Обновлений малюнок: ID = " + picture.ID.ToString() + ", Name = " + picture.Name);

            return true;
        }

        public bool DeletePicture(int PictureID)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // Транзакція
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при старті транзакції", e.Message);

                return false;
            }

            myCommand.Parameters.AddWithValue("@ID", PictureID);

            //Видалення колекцій Pictures
            myCommand.CommandText = "DELETE FROM `pictures_picturechild` WHERE `Picture` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема видалення колекцій Pictures", e.Message);

                return false;
            }

            //Видалення колекцій Images
            myCommand.CommandText = "DELETE FROM `pictures_imagechild` WHERE `Picture` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема видалення колекцій Pictures", e.Message);

                return false;
            }

            //Видалення самого малюнка
            myCommand.CommandText = "DELETE FROM `pictures` WHERE `ID` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема видалення Picture", e.Message);

                return false;
            }

            // Запис транзакції
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при записі тразакції", e.Message);

                return false;
            }

            InfoReporting("Видалений малюнок ID = " + PictureID.ToString());

            return true;
        }

        private PicturesBase GetPicturesBaseBy_ID_or_Name(GetByVariant variant, string Value)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name`, `Description` FROM `pictures` ";

            if (variant == GetByVariant.ID)
            {
                myCommand.CommandText += " WHERE `ID` = @ID";
                myCommand.Parameters.AddWithValue("@ID", Value);
            }
            else if (variant == GetByVariant.Name)
            {
                myCommand.CommandText += " WHERE `Name` = @Name";
                myCommand.Parameters.AddWithValue("@Name", Value);
            }
            else
                throw new Exception("Невірно заданий критерій пошуку PicturesBase");

            PicturesBase pictureBase = new PicturesBase();

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    pictureBase.ID = int.Parse(reader["ID"].ToString());
                    pictureBase.Name = reader["Name"].ToString();
                    pictureBase.Description = reader["Description"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку pictureBase", e.Message);

                return null;
            }

            return pictureBase;
        }

        public PicturesBase GetPicturesBaseByID(int ID)
        {
            return GetPicturesBaseBy_ID_or_Name(GetByVariant.ID, ID.ToString());
        }

        public PicturesBase GetPicturesBaseByName(string pictureName)
        {
            return GetPicturesBaseBy_ID_or_Name(GetByVariant.Name, pictureName);
        }

        private Pictures GetPicturesBy_ID_or_Name(GetByVariant variant, string Value)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name`, `Description` FROM `pictures` ";

            if (variant == GetByVariant.ID)
            {
                myCommand.CommandText += " WHERE `ID` = @ID";
                myCommand.Parameters.AddWithValue("@ID", Value);
            }
            else if (variant == GetByVariant.Name)
            {
                myCommand.CommandText += " WHERE `Name` = @Name";
                myCommand.Parameters.AddWithValue("@Name", Value);
            }
            else
                throw new Exception("Невірно заданий критерій пошуку Pictures");

            Pictures picture = null;

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    picture = new Pictures();

                    picture.ID = int.Parse(reader["ID"].ToString());
                    picture.Name = reader["Name"].ToString();
                    picture.Description = reader["Description"].ToString();
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку Pictures", e.Message);

                return null;
            }

            //
            //Якщо малюнок незнайдений то возврат
            //
            if (picture == null) return null;

            //Колекції Pictures
            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Picture", picture.ID);

            myCommand.CommandText = "SELECT `pictures_picturechild`.`PictureChild`, `pictures`.`Name`, `pictures`.`Description` " +
                                    "FROM `pictures_picturechild` " +
                                    "LEFT JOIN `pictures` ON `pictures`.`ID` = `pictures_picturechild`.`PictureChild` " +
                                    "WHERE `Picture` = @Picture";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    PicturesBase pictureBaseElement = new PicturesBase();
                    pictureBaseElement.ID = int.Parse(reader["PictureChild"].ToString());
                    pictureBaseElement.Name = reader["Name"].ToString();
                    pictureBaseElement.Description = reader["Description"].ToString();

                    picture.PicturesPictureChild.Add(pictureBaseElement);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема вибірки колекцій Pictures", e.Message);

                return null;
            }

            //Колекції Images
            myCommand.CommandText = "SELECT `pictures_imagechild`.`ImageChild`, " +
                                    "       `image`.`Name`, `image`.`Description`, `image`.`Synonymy`, `image`.`Context`, " +
                                    "       `image_context`.`Name` AS `ContextName` " +
                                    "FROM `pictures_imagechild` " +
                                    "LEFT JOIN `image` ON `image`.`ID` = `pictures_imagechild`.`ImageChild` " +
                                    "LEFT JOIN `image_context` ON `image_context`.`ID` = `image`.`Context` " +
                                    "WHERE `Picture` = @Picture";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    ImageBase ImageBaseElement = new ImageBase();
                    ImageBaseElement.ID = int.Parse(reader["ImageChild"].ToString());
                    ImageBaseElement.Name = reader["Name"].ToString();
                    ImageBaseElement.Description = reader["Description"].ToString();
                    ImageBaseElement.Synonymy = reader["Synonymy"].ToString();
                    ImageBaseElement.Context = new ImageContext(int.Parse(reader["Context"].ToString()), reader["ContextName"].ToString());

                    picture.PicturesImageChild.Add(ImageBaseElement);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема вибірки колекцій Pictures", e.Message);

                return null;
            }

            return picture;
        }

        public Pictures GetPicturesByID(int ID)
        {
            return GetPicturesBy_ID_or_Name(GetByVariant.ID, ID.ToString());
        }

        public Pictures GetPicturesByName(string pictureName)
        {
            return GetPicturesBy_ID_or_Name(GetByVariant.Name, pictureName);
        }

        #endregion

        #region CONTROL SEARCH

        /// <summary>
        /// Пошук для контола ControlSearch
        /// </summary>
        /// <param name="Table">Назва таблиці по якій буде пошук</param>
        /// <param name="text">Текст пошуку</param>
        /// <param name="limit">Обмеження результату пошуку</param>
        /// <param name="result">Словник з результатами (ID, Name)</param>
        public void SearchPicturesOrImages(string Table, string text, int limit, List<SearchElement> result)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            if (Table == "image")
            {
                myCommand.CommandText = "SELECT `image`.`ID`, `image`.`Name`, `image_context`.`Name` As `Context` FROM `image` " +
                                        "LEFT JOIN `image_context` ON `image_context`.`id` = `image`.`Context` " +
                                        "WHERE `image`.`Name` LIKE @TEXT ";
            }
            else if (Table == "pictures")
            {
                myCommand.CommandText = "SELECT `pictures`.`ID`, `pictures`.`Name`, '' As `Context` FROM `pictures` " +
                                        "WHERE `pictures`.`Name` LIKE @TEXT ";
            }
            else
                throw new Exception("Невірно задана таблиця для пошуку");

            myCommand.CommandText += (limit > 0 ? " LIMIT " + limit.ToString() : "");

            myCommand.Parameters.AddWithValue("@TEXT", text.Trim() + "%");

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                    result.Add(
                          new SearchElement(
                              int.Parse(reader["ID"].ToString()),
                              reader["Name"].ToString(),
                              reader["Context"].ToString()));

                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку", e.Message);
            }
        }

        #endregion

        #region SUPPOR FUNCTION

        /// <summary>
        /// Функція шукає назви образів для яких даний образ (переданий параметер) виступає в ролі інградієнту.
        /// Тобто функція шукає всі ссилки на даний образ.
        /// </summary>
        /// <param name="image">Образ для пошуку ссилок</param>
        /// <returns>Список назв образів які ссилаються на даний образ (переданий параметер)</returns>
        public List<string> GetImageIngradientyLink(Image image)
        {
            List<string> ImageIngradientyName = new List<string>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //Знайти і вивести список образів
            myCommand.CommandText = "SELECT `Image`.`Name` as `Image_name` " +
                                    "FROM `image_ingradienty` " +
                                    "      LEFT JOIN `image` ON  `Image`.`ID` = `image_ingradienty`.`Image`" +
                                    "WHERE `image_ingradienty`.`Ingradient` = @image";

            //Відбір по ІД образу
            myCommand.Parameters.AddWithValue("@Image", image.ID.ToString());

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                    ImageIngradientyName.Add(reader["Image_name"].ToString());

                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошуку ссилок образів. Функція GetImageIngradientyLink()", e.Message);
            }

            return ImageIngradientyName;
        }

        #endregion

        #region SYSTEM FUNCTION

        /// <summary>
        /// Функція повертає список динамічно створених таблиць образів
        /// </summary>
        /// <returns></returns>
        public List<string> GetDinamicTableList()
        {
            List<string> TableList = new List<string>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SHOW TABLES LIKE 'img_%'";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                    TableList.Add(reader[0].ToString());
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при вибірці списку динамічних таблиць образів", e.Message);
                return null;
            }

            return TableList;
        }

        /// <summary>
        /// Функція повертає список полів таблиці
        /// </summary>
        /// <param name="TableName">Назва таблиці</param>
        /// <returns></returns>
        public List<TableColumnInfo> GetDinamicTableColumnList(string TableName)
        {
            List<TableColumnInfo> TableColumnList = new List<TableColumnInfo>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SHOW COLUMNS FROM `" + TableName + "`";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    TableColumnInfo columnInfo = new TableColumnInfo();

                    columnInfo.Field = reader["Field"].ToString();
                    columnInfo.Type = reader["Type"].ToString();
                    columnInfo.Null = reader["Null"].ToString();
                    columnInfo.Key = reader["Key"].ToString();
                    columnInfo.Default = reader["Default"].ToString();
                    columnInfo.Extra = reader["Extra"].ToString();

                    TableColumnList.Add(columnInfo);
                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при вибірці списку полів таблиці " + TableName, e.Message);
                return null;
            }

            return TableColumnList;
        }

        /// <summary>
        /// Функція повертає список полів таблиці
        /// </summary>
        /// <param name="TableName">Назва таблиці</param>
        /// <returns></returns>
        public List<TableIndexInfo> GetDinamicTableIndexList(string TableName)
        {
            List<TableIndexInfo> TableIndexList = new List<TableIndexInfo>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SHOW INDEX FROM `" + TableName + "`";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    TableIndexInfo indexInfo = new TableIndexInfo();

                    indexInfo.Table = reader["Table"].ToString();
                    indexInfo.Field = reader["Column_name"].ToString();
                    indexInfo.Type = reader["Index_type"].ToString();
                    indexInfo.Key = reader["Key_name"].ToString();
                    indexInfo.NonUnigue = int.Parse(reader["Non_unique"].ToString());
                    indexInfo.Null = reader["Null"].ToString();

                    TableIndexList.Add(indexInfo);
                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при вибірці списку індексів таблиці " + TableName, e.Message);
                return null;
            }

            return TableIndexList;
        }

        /// <summary>
        /// Функція виконує запит в базі даних
        /// </summary>
        /// <param name="query">Запит</param>
        /// <param name="query_param">Параметри запиту</param>
        public int ExecuteNonSQLQuery(string query, Dictionary<string, string> query_param = null)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = query;

            if (query_param != null)
                foreach (KeyValuePair<string, string> query_param_item in query_param)
                    myCommand.Parameters.AddWithValue(query_param_item.Key, query_param_item.Value);

            int result = 0;

            try
            {
                result = myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при виконанні запиту", e.Message);
            }

            return result;
        }

        /// <summary>
        /// Функція видаляє дані з основних таблиць
        /// </summary>
        public void DelAll()
        {
            List<string> query = new List<string>();

            query.Add("DELETE FROM `image_atributes`");
            query.Add("DELETE FROM `image_characterystyka`");
            query.Add("DELETE FROM `image_ingradienty`");

            query.Add("DELETE FROM `pictures_imagechild`");
            query.Add("DELETE FROM `pictures_picturechild`");
            query.Add("DELETE FROM `pictures`");

            query.Add("DELETE FROM `image`");
            query.Add("DELETE FROM `image_context`");

            query.Add("ALTER TABLE `image` AUTO_INCREMENT = 1");
            query.Add("ALTER TABLE `image_context` AUTO_INCREMENT = 1");
            query.Add("ALTER TABLE `pictures` AUTO_INCREMENT = 1");

            foreach (string item in query)
            {
                Console.WriteLine(item);
                //!!!Console.WriteLine("Результат: " + ExecuteNonSQLQuery(item));
            }
        }

        #endregion

        #region SEARCH

        /// <summary>
        /// Прямий пошук в схемі
        /// </summary>
        public List<SearchRowData> SearchShema(string query)
        {
            List<SearchRowData> list = new List<SearchRowData>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `Name` FROM `image_indexes_shema` " +
                                    "WHERE `Name` LIKE @Query";

            myCommand.Parameters.AddWithValue("@Query", query);

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SearchRowData(query, reader["ID"].ToString(), reader["Name"].ToString(), "", true, false, false));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошку", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// Прямий пошук в даних
        /// </summary>
        public List<SearchRowData> SearchDirectData(string query)
        {
            List<SearchRowData> list = new List<SearchRowData>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `ShemaID`, `Name` FROM `image_indexes_data` " +
                                    "WHERE `Name` LIKE @Query";

            myCommand.Parameters.AddWithValue("@Query", query);

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SearchRowData(query, reader["ShemaID"].ToString(), reader["Name"].ToString(), reader["ID"].ToString(), false, true, false));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошку", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// Пошук входежня в даних
        /// </summary>
        public List<SearchRowData> SearchEntryData(string query)
        {
            List<SearchRowData> list = new List<SearchRowData>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            myCommand.CommandText = "SELECT `ID`, `ShemaID`, `Name` FROM `image_indexes_data` " +
                                    "WHERE `Name` LIKE @Query ";

            myCommand.Parameters.AddWithValue("@Query", "%" + query + "%");

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SearchRowData(query, reader["ShemaID"].ToString(), reader["Name"].ToString(), reader["ID"].ToString(), false, false, true));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при пошку", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// Функція шукає переходи між списком А та списком Б
        /// </summary>
        /// <param name="Tracks">Список результатів</param>
        /// <param name="listShemaA">Список А</param>
        /// <param name="listShemaB">Список Б</param>
        public void GetTracks(List<Track> Tracks, List<string> listShemaA, List<string> listShemaB)
        {
            if (listShemaA.Count == 0 || listShemaB.Count == 0)
                return;

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            int counter = 0;
            string query_where = " WHERE `imageA` IN (";

            foreach (string item in listShemaA)
            {
                query_where += (counter > 0 ? "," : "") + item;
                counter++;
            }

            counter = 0;
            query_where += ") AND `imageB` IN (";

            foreach (string item in listShemaB)
            {
                query_where += (counter > 0 ? "," : "") + item;
                counter++;
            }

            query_where += ") ";

            myCommand.CommandText = "SELECT `imageA`, `imageB`, `bridge`, `levelA`, `levelB`, `pathA`, `pathB`, `trackid` FROM `image_tracks` " +
                                    query_where +
                                    "GROUP BY `trackid` " +
                                    "ORDER BY `levelA` ASC, `levelB` ASC";

            try
            {
                MySqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    Tracks.Add(
                        new Track(
                            reader["imageA"].ToString(),
                            reader["imageB"].ToString(),
                            reader["bridge"].ToString(),
                            reader["levelA"].ToString(),
                            reader["levelB"].ToString(),
                            reader["pathA"].ToString(),
                            reader["pathB"].ToString()
                            )
                        );
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("Проблема при вибірці треків", e.Message);
            }
        }

        #endregion
    }
}