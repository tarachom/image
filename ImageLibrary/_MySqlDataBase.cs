using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace ImageLibrary
{
    /// <summary>
    /// ���� ����� MySql
    /// </summary>
    public class _MySqlDataBase : IDataBase
    {
        private string m_ConnectString;
        private MySqlConnection m_Connect;

        /// <summary>
        /// ��� ������
        /// </summary>
        private enum GetByVariant
        {
            /// <summary>
            /// ³��� �� ��
            /// </summary>
            ID,

            /// <summary>
            /// ³��� �� ����
            /// </summary>
            Name,

            /// <summary>
            /// ����� �� ����
            /// </summary>
            SearchName,

            /// <summary>
            /// ³��� �� ��������
            /// </summary>
            Context
        }

        #region EVENTS

        //������� ��� ������
        public event DataBaseStateHandler DataBaseExceptionEvent;

        //������� ��� ��������� ������ ������
        public event DataBaseStateHandler AddedNewImageEvent;

        //������� ��� �������� ������
        public event DataBaseStateHandler DeleteImageEvent;

        //������� ��� ��������� ������
        public event DataBaseStateHandler UpdateImageEvent;

        //������� ��� ������ ������ �� ���� ��� �� ��
        public event DataBaseStateHandler GetByNameOrIDImageEvent;

        #endregion

        #region CONNECT CLOSE

        /// <summary>
        /// ������ ����������
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
        /// ϳ��������� �� ���� �����
        /// </summary>
        /// <returns>true ���� ��</returns>
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
                    DataBaseExceptionEvent(this, new DataBaseEventArgs("�������� ��� ���������", e.Message));

                return false;
            }
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        /// <returns>true ���� ��</returns>
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
                    DataBaseExceptionEvent(this, new DataBaseEventArgs("�������� ��� ������� ����������", e.Message));

                return false;
            }
        }

        #endregion

        #region EVENT_JOURNAL

        /// <summary>
        /// ������� ������� ����������� � �������
        /// </summary>
        /// <param name="messList">������ ��� ����������</param>
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
        /// ������ ����� ����� � ����
        /// </summary>
        /// <param name="image">�����</param>
        /// <returns>true ���� ��</returns>
        public bool InsertImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

                return false;
            }

            // ���������� ������ `image`
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
                //�� �������������� ��������
                image.ID = myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ������ ������", e.Message);

                return false;
            }

            // ���������� �������� `image_atributes`
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
                    ErrorReporting("�������� ��� ����� �������� ������ (" + atribute.ToString() + ")", e.Message);

                    return false;
                }
            }

            // ���������� `image_characterystyka`
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
                    ErrorReporting("�������� ��� ����� �������������� ������ (" + characterystyka.ItemName + ")", e.Message);

                    return false;
                }
            }

            // ���������� `image_ingradienty`
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
                    ErrorReporting("�������� ��� ����� �����䳺��� ������ (" + ingradient.Name + ")", e.Message);

                    return false;
                }
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ���������", e.Message);

                return false;
            }

            if (AddedNewImageEvent != null)
                AddedNewImageEvent(this, new DataBaseEventArgs("���������� ����� ����� � ���� ������", image.Name));

            InfoReporting("���������� ����� ����� � ���� ������: ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        /// <summary>
        /// �������� ����� � ���
        /// </summary>
        /// <param name="image">�����</param>
        /// <returns>true ���� ��</returns>
        public bool UpdateImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

                return false;
            }

            //
            // ���������� ������ ������
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
                ErrorReporting("������� ��� ��������� ������ ������ " + image.Name, e.Message);

                return false;
            }

            //
            // ���������� ��������
            //

            //��������� ��� �������� ������
            myCommand.CommandText = "DELETE FROM `image_atributes` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �������� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ����� ����� �������� `image_atributes`
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
                    ErrorReporting("�������� ��� ����� �������� ������ (" + atribute.ToString() + ")", e.Message);

                    return false;
                }
            }

            //
            // ���������� �������������
            //

            //��������� ��� ������������� ������
            myCommand.CommandText = "DELETE FROM `image_characterystyka` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� ������������� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ����� ����� ������������� `image_characterystyka`
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
                    ErrorReporting("�������� ��� ����� �������������� ������ (" + characterystyka.ItemName + ")", e.Message);

                    return false;
                }
            }

            //
            // ���������� �����䳺���
            //

            //��������� ��� �����䳺��� ������
            myCommand.CommandText = "DELETE FROM `image_ingradienty` WHERE `Image` = @ImageID";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@ImageID", image.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �����䳺��� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ����� ����� �����䳺��� ������ `image_ingradienty`
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
                    ErrorReporting("�������� ��� ����� �����䳺��� ������ (" + ingradient.Name + ")", e.Message);

                    return false;
                }
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ��������", e.Message);

                return false;
            }

            if (UpdateImageEvent != null)
                UpdateImageEvent(this, new DataBaseEventArgs("���������� ����� ", image.Name));

            InfoReporting("���������� �����: ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        /// <summary>
        /// ������� ������� ����� �� �� ���� ������� �� ������
        /// </summary>
        /// <param name="image">�����</param>
        /// <returns>true ���� ������� ��������</returns>
        public bool DeleteImage(Image image)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //³��� �� �� ������
            myCommand.Parameters.AddWithValue("@Image", image.ID.ToString());

            //��������� �� � ������ �� �����, �������� ����� ���� �� ����� ���� �� ���������
            try
            {
                //������ �� ������ ���� ������ ������ �� ��� ����� � ������� �����䳺���
                //����� �� ������� ��� ����� � ��� �����䳺��� ��� ����� ������
                myCommand.CommandText = "SELECT count(`Image`) FROM `image_ingradienty` WHERE `Ingradient` = @image";

                if (int.Parse(myCommand.ExecuteScalar().ToString()) > 0)
                {
                    ErrorReporting("�������� ��������� ��� �� ����� ������� � ��� �����䳺��� ��� ����� ������", "");

                    return false;
                }

                //��� ����� ������� �� ��������
                //...
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��������� ������", e.Message);

                return false;
            }

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

                return false;
            }

            // ��������� ��������
            try
            {
                myCommand.CommandText = "DELETE FROM `image_atributes` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �������� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ��������� �������������
            try
            {
                myCommand.CommandText = "DELETE FROM `image_characterystyka` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� ������������� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ��������� �����䳺���
            try
            {
                myCommand.CommandText = "DELETE FROM `image_ingradienty` WHERE `Image` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �����䳺��� ��� ������ " + image.Name, e.Message);

                return false;
            }

            // ��������� ������ ������
            try
            {
                myCommand.CommandText = "DELETE FROM `image` WHERE `ID` = @Image";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� ������ " + image.Name, e.Message);

                return false;
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ���������", e.Message);

                return false;
            }

            if (DeleteImageEvent != null)
                DeleteImageEvent(this, new DataBaseEventArgs("��������� ����� '" + image.Name + "' � ���� ������", ""));

            InfoReporting("��������� ����� � ���� ����� ID = " + image.ID.ToString() + ", Name = " + image.Name);

            return true;
        }

        #endregion

        #region PRIVAT FUNCTION

        /// <summary>
        /// ������� �������� �� �������� ��� ������ ������
        /// </summary>
        /// <param name="imageItemList">������ ������ ��� ���� ����� ��������� ��������</param>
        /// <returns>true ���� ��� ��</returns>
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
                //��������
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
                    ErrorReporting("��������: ��������� ������� �������� ��� ������", e.Message);

                    return false;
                }

                //
                //��������������
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
                    ErrorReporting("��������: ��������� ������� �������������� ��� ������", e.Message);

                    return false;
                }

                //
                //�����䳺���
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
                    ErrorReporting("��������: ��������� ������� �����䳺��� ��� ������", e.Message);

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
                        ErrorReporting("��������: ��������� ������� �����䳺��� ��� ������", e.Message);

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ������� ��� ������ ������ �� �� ��� �� ���� � ����� ���������, ��� ������ � ����� ���������
        /// </summary>
        /// <param name="variant">������� ��� ������: ID, Name, Context</param>
        /// <param name="whereValue">�������� ������� ������</param>
        /// <param name="whereContextID">³��� � ����� ���������</param>
        /// <returns>������� ������ ��������� ������</returns>
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
                throw new Exception("������ ������� ������� ������ Images");

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

                    //��������
                    imageItem.Context = new ImageContext(int.Parse(reader["Context"].ToString()));
                    imageItem.Context.Name = reader["ContextName"].ToString();
                    imageItem.Context.Description = reader["ContexDesc"].ToString();

                    //������ �� ��������
                    int LinkContextID = int.Parse(reader["LinkContext"].ToString());

                    if (LinkContextID > 0)
                        imageItem.LinkContext = new ImageContext(
                            LinkContextID,
                            reader["LinkContextName"].ToString(),
                            reader["LinkContextDesc"].ToString());

                    //̳���
                    imageItem.Pointer = bool.Parse(reader["Pointer"].ToString());
                    imageItem.Plural = bool.Parse(reader["Plural"].ToString());
                    imageItem.Intermediate = bool.Parse(reader["Intermediate"].ToString());

                    //��������
                    long PointerImage = long.Parse(reader["PointerImage"].ToString());

                    if (PointerImage > 0)
                        imageItem.PointerImage = new ImageBase(PointerImage, reader["PointerName"].ToString());

                    imageList.Add(imageItem);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("��������: ��������� ������� �����(�)", e.Message);

                return null;
            }

            //��������� ��������
            if (FillImageCollections(imageList))
                return imageList;
            else
                return new List<Image>();
        }

        /// <summary>
        /// ������� ��� ������ �������� ������ �� �� ��� �� ���� � ����� ���������, ��� ������ � ����� ���������
        /// </summary>
        /// <param name="imageBaseList">������ ���� ������ �������� ������� ������</param>
        /// <param name="variant">������� ��� ������: ID, Name, Context</param>
        /// <param name="whereValue">�������� ������� ������</param>
        /// <param name="whereContextID">³��� � ����� ���������</param>
        /// <returns>������� ������ ��������� ������</returns>
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
                throw new Exception("������ ������� ������� ������ ImageBase");

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

                    //��������
                    imageBase.Context = new ImageContext(
                        int.Parse(reader["Context"].ToString()),
                        reader["ContextName"].ToString(),
                        reader["ContexDesc"].ToString()
                    );

                    //̳���
                    imageBase.Pointer = bool.Parse(reader["Pointer"].ToString());
                    imageBase.Plural = bool.Parse(reader["Plural"].ToString());
                    imageBase.Intermediate = bool.Parse(reader["Intermediate"].ToString());

                    imageBaseList.Add(imageBase);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ������ ���� ��� ������ �� ���� ��� �� ��", e.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// ������� ���� ��������
        /// </summary>
        /// <param name="variant">������ ������</param>
        /// <param name="whereValue">�������� ��� ������</param>
        /// <returns>�������� or null</returns>
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
                throw new Exception("������ ������� ������� ������ Images");

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
                ErrorReporting("�������� ��� ������ ��������� �� ��", e.Message);
                return null;
            }

            return imageContext;
        }

        /// <summary>
        /// ������� ������ � ������ ���� �����������
        /// </summary>
        /// <param name="message">�����������</param>
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
        /// �������� ��� ���������� ��� �������
        /// </summary>
        /// <param name="messsage">�����������</param>
        /// <param name="description">����</param>
        private void ErrorReporting(string messsage, string description)
        {
            Write_EventJournalMessage(new EventJournalMessage(EventJournalMessageType.Error, messsage, description));

            if (DataBaseExceptionEvent != null)
                DataBaseExceptionEvent(this, new DataBaseEventArgs(messsage, description));
        }

        /// <summary>
        /// �������� ��� ������������� ����������
        /// </summary>
        /// <param name="messsage"></param>
        private void InfoReporting(string messsage)
        {
            Write_EventJournalMessage(new EventJournalMessage(EventJournalMessageType.Info, messsage));
        }

        #endregion

        #region GET IMAGE, IMAGEBASE

        /// <summary>
        /// ����� ������ �� ����
        /// </summary>
        /// <param name="Name">����� ������</param>
        /// <param name="whereContextID">�� ���������</param>
        /// <returns>����� ��� null</returns>
        public List<Image> GetImageByName(string Name, int whereContextID = 0)
        {
            return GetImageByVariant(GetByVariant.Name, Name, whereContextID);
        }

        /// <summary>
        ///  ����� ������ �� ID
        /// </summary>
        /// <param name="ID">ID ������</param>
        /// <returns>����� ��� null</returns>
        public Image GetImageByID(long ID)
        {
            List<Image> result = GetImageByVariant(GetByVariant.ID, ID.ToString());

            if (result != null)
                if (result.Count > 0)
                    return result[0];

            return null;
        }

        /// <summary>
        /// ������� ������ ������ ��� ������
        /// </summary>
        /// <returns>������ ������ ��� null</returns>
        public List<Image> LoadAllImage()
        {
            return GetImageByVariant(GetByVariant.Context, "", 0);
        }

        /// <summary>
        /// ������� ������ ������ ��� ������ � ����� ���������
        /// </summary>
        /// <param name="whereContextID">�� ���������</param>
        /// <returns>������ �� ������������ ��� ����</returns>
        public List<Image> LoadAllImageByContext(int whereContextID)
        {
            return GetImageByVariant(GetByVariant.Context, "", whereContextID);
        }

        /// <summary>
        /// ����� ���� ��� ������ �� ID
        /// </summary>
        /// <param name="ID">�������� ��� ������</param>
        /// <returns>������ ������� ����� ��� null</returns>
        public List<ImageBase> GetListImageBaseByID(long ID)
        {
            List<ImageBase> imageBase = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBase, GetByVariant.ID, ID.ToString()))
                return imageBase;
            else
                return null;
        }

        /// <summary>
        /// ����� ���� ��� ������ �� ����
        /// </summary>
        /// <param name="Name">�������� ��� ������</param>
        /// <param name="whereContextID">�� ���������</param>
        /// <param name="limit">˳��</param>
        /// <returns>������ ������� ����� ��� null</returns>
        public List<ImageBase> GetListImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            List<ImageBase> imageBaseList = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBaseList, GetByVariant.Name, Name, whereContextID, limit))
                return imageBaseList;
            else
                return null;
        }

        /// <summary>
        /// ����� ������� ������ �� ����
        /// </summary>
        /// <param name="Name">����� ������</param>
        /// <param name="whereContextID">�� ���������</param>
        /// <param name="limit">˳��</param>
        /// <returns>������ ��������� ������</returns>
        public List<ImageBase> SearchImageBaseByName(string Name, int whereContextID = 0, int limit = 0)
        {
            List<ImageBase> imageBaseList = new List<ImageBase>();
            if (GetImageBaseByVariant(imageBaseList, GetByVariant.SearchName, Name, whereContextID, limit))
                return imageBaseList;
            else
                return null;
        }

        /// <summary>
        /// ������� � ������ �� ������ ����� � �������� ������
        /// </summary>
        /// <param name="imageList">������ ��� ����������</param>
        /// <param name="whereContextID">�� ���������</param>
        /// <param name="limit">˳��</param>
        public void LoadAllImageBase(List<ImageBase> imageList, int whereContextID = 0, int limit = 0)
        {
            GetImageBaseByVariant(imageList, GetByVariant.Context, "", whereContextID, limit);
        }

        #endregion

        #region CONTEXT

        /// <summary>
        /// ������� �������� ����� ��������
        /// </summary>
        /// <param name="context"></param>
        /// <returns>true ���� ��� ��</returns>
        public bool InsertContext(ImageContext context)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //
            //���� ���� �������� ��������� ���� ��� ��������� ��� ������� �������� ����� ������� � ���������� ��������� ������.
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

                //�� �������������� ��������
                context.ID = (int)myCommand.LastInsertedId;
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ������ ���������", e.Message);

                return false;
            }

            InfoReporting("���������� ����� ��������: ID = " + context.ID.ToString() + ", Name = " + context.Name);

            return true;
        }

        /// <summary>
        /// ������� �������� ��������
        /// </summary>
        /// <param name="context">��������</param>
        /// <returns>true ���� ��� ��</returns>
        public bool UpdateContext(ImageContext context)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ���������� ���������
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
                ErrorReporting("�������� ��� ��������� ���������", e.Message);

                return false;
            }

            InfoReporting("���������� ��������: ID = " + context.ID.ToString() + ", Name = " + context.Name);

            return true;
        }

        /// <summary>
        /// ������� ������� ��������
        /// </summary>
        /// <param name="contextID">��������</param>
        /// <returns>true ���� ��� ��</returns>
        public bool DeleteContext(int contextID)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ��������� ���������
            myCommand.CommandText = "DELETE FROM `image_context` WHERE `ID` = @ContextID";

            myCommand.Parameters.AddWithValue("@ContextID", contextID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� �������� ���������", e.Message);

                return false;
            }

            InfoReporting("��������� ��������: ID = " + contextID.ToString());

            return true;
        }

        /// <summary>
        /// ������� ���� �������� �� ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>�������� ��� ����</returns>
        public ImageContext GetImageContexByID(int ID)
        {
            return GetImageContextBy_ID_or_Name(GetByVariant.ID, ID.ToString());
        }

        /// <summary>
        /// ������� ���� �������� �� ����
        /// </summary>
        /// <param name="ContextName"></param>
        /// <returns>�������� ��� ����</returns>
        public ImageContext GetImageContextByName(string ContextName)
        {
            return GetImageContextBy_ID_or_Name(GetByVariant.Name, ContextName);
        }

        /// <summary>
        /// ������� ������� � ������ �� ���������
        /// </summary>
        /// <param name="listContext">������ ��� ����������</param>
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
                ErrorReporting("�������� ��� ������ ������ ���������", e.Message);
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
                ErrorReporting("�������� ������ ��������� ��� Pictures", e.Message);

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
                ErrorReporting("�������� ��������� ��������� ��� Pictures", e.Message);

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
                ErrorReporting("�������� ������ ��������� ��� Pictures", e.Message);
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
                ErrorReporting("�������� ������ PicturesBase", e.Message);
            }
        }

        public bool InsertPicture(Pictures picture)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

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
                ErrorReporting("�������� ������ ������ Pictures", e.Message);

                return false;
            }

            // �������� Pictures
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
                    ErrorReporting("�������� ��� ����� �������� ������� �������", e.Message);

                    return false;
                }
            }

            // �������� Images
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
                    ErrorReporting("�������� ��� ����� �������� ������ �������", e.Message);

                    return false;
                }
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ���������", e.Message);

                return false;
            }

            InfoReporting("���������� ����� �������: ID = " + picture.ID.ToString() + ", Name = " + picture.Name);

            return true;
        }

        public bool UpdatePicture(Pictures picture)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

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
                ErrorReporting("�������� ���������� ������ Pictures", e.Message);

                return false;
            }

            //������ ���� �������� Pictures
            myCommand.CommandText = "DELETE FROM `pictures_picturechild` WHERE `Picture` = @Picture";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Picture", picture.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �������� ��� �������", e.Message);

                return false;
            }

            //��� �������� Pictures
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
                    ErrorReporting("�������� ��� ����� �������� ������� �������", e.Message);

                    return false;
                }
            }

            //������ ���� �������� Images
            myCommand.CommandText = "DELETE FROM `pictures_imagechild` WHERE `Picture` = @Picture";

            myCommand.Parameters.Clear();
            myCommand.Parameters.AddWithValue("@Picture", picture.ID);

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("������� ��� �������� �������� ��� �������", e.Message);

                return false;
            }

            //��� �������� Images
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
                    ErrorReporting("�������� ��� ����� �������� ������ �������", e.Message);

                    return false;
                }
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ���������", e.Message);

                return false;
            }

            InfoReporting("���������� �������: ID = " + picture.ID.ToString() + ", Name = " + picture.Name);

            return true;
        }

        public bool DeletePicture(int PictureID)
        {
            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            // ����������
            try
            {
                myCommand.CommandText = "START TRANSACTION";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ����������", e.Message);

                return false;
            }

            myCommand.Parameters.AddWithValue("@ID", PictureID);

            //��������� �������� Pictures
            myCommand.CommandText = "DELETE FROM `pictures_picturechild` WHERE `Picture` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��������� �������� Pictures", e.Message);

                return false;
            }

            //��������� �������� Images
            myCommand.CommandText = "DELETE FROM `pictures_imagechild` WHERE `Picture` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��������� �������� Pictures", e.Message);

                return false;
            }

            //��������� ������ �������
            myCommand.CommandText = "DELETE FROM `pictures` WHERE `ID` = @ID";

            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��������� Picture", e.Message);

                return false;
            }

            // ����� ����������
            try
            {
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                ErrorReporting("�������� ��� ����� ���������", e.Message);

                return false;
            }

            InfoReporting("��������� ������� ID = " + PictureID.ToString());

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
                throw new Exception("������ ������� ������� ������ PicturesBase");

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
                ErrorReporting("�������� ��� ������ pictureBase", e.Message);

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
                throw new Exception("������ ������� ������� ������ Pictures");

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
                ErrorReporting("�������� ��� ������ Pictures", e.Message);

                return null;
            }

            //
            //���� ������� ����������� �� �������
            //
            if (picture == null) return null;

            //�������� Pictures
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
                ErrorReporting("�������� ������ �������� Pictures", e.Message);

                return null;
            }

            //�������� Images
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
                ErrorReporting("�������� ������ �������� Pictures", e.Message);

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
        /// ����� ��� ������� ControlSearch
        /// </summary>
        /// <param name="Table">����� ������� �� ��� ���� �����</param>
        /// <param name="text">����� ������</param>
        /// <param name="limit">��������� ���������� ������</param>
        /// <param name="result">������� � ������������ (ID, Name)</param>
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
                throw new Exception("������ ������ ������� ��� ������");

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
                ErrorReporting("�������� ��� ������", e.Message);
            }
        }

        #endregion

        #region SUPPOR FUNCTION

        /// <summary>
        /// ������� ���� ����� ������ ��� ���� ����� ����� (��������� ���������) ������� � ��� �����䳺���.
        /// ����� ������� ���� �� ������ �� ����� �����.
        /// </summary>
        /// <param name="image">����� ��� ������ ������</param>
        /// <returns>������ ���� ������ �� ���������� �� ����� ����� (��������� ���������)</returns>
        public List<string> GetImageIngradientyLink(Image image)
        {
            List<string> ImageIngradientyName = new List<string>();

            MySqlCommand myCommand = new MySqlCommand();
            myCommand.Connection = this.m_Connect;

            //������ � ������� ������ ������
            myCommand.CommandText = "SELECT `Image`.`Name` as `Image_name` " +
                                    "FROM `image_ingradienty` " +
                                    "      LEFT JOIN `image` ON  `Image`.`ID` = `image_ingradienty`.`Image`" +
                                    "WHERE `image_ingradienty`.`Ingradient` = @image";

            //³��� �� �� ������
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
                ErrorReporting("�������� ��� ������ ������ ������. ������� GetImageIngradientyLink()", e.Message);
            }

            return ImageIngradientyName;
        }

        #endregion

        #region SYSTEM FUNCTION

        /// <summary>
        /// ������� ������� ������ �������� ��������� ������� ������
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
                ErrorReporting("�������� ��� ������ ������ ��������� ������� ������", e.Message);
                return null;
            }

            return TableList;
        }

        /// <summary>
        /// ������� ������� ������ ���� �������
        /// </summary>
        /// <param name="TableName">����� �������</param>
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
                ErrorReporting("�������� ��� ������ ������ ���� ������� " + TableName, e.Message);
                return null;
            }

            return TableColumnList;
        }

        /// <summary>
        /// ������� ������� ������ ���� �������
        /// </summary>
        /// <param name="TableName">����� �������</param>
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
                ErrorReporting("�������� ��� ������ ������ ������� ������� " + TableName, e.Message);
                return null;
            }

            return TableIndexList;
        }

        /// <summary>
        /// ������� ������ ����� � ��� �����
        /// </summary>
        /// <param name="query">�����</param>
        /// <param name="query_param">��������� ������</param>
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
                ErrorReporting("�������� ��� �������� ������", e.Message);
            }

            return result;
        }

        /// <summary>
        /// ������� ������� ��� � �������� �������
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
                //!!!Console.WriteLine("���������: " + ExecuteNonSQLQuery(item));
            }
        }

        #endregion

        #region SEARCH

        /// <summary>
        /// ������ ����� � ����
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
                ErrorReporting("�������� ��� �����", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// ������ ����� � �����
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
                ErrorReporting("�������� ��� �����", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// ����� �������� � �����
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
                ErrorReporting("�������� ��� �����", e.Message);
                return null;
            }

            return list;
        }

        /// <summary>
        /// ������� ���� �������� �� ������� � �� ������� �
        /// </summary>
        /// <param name="Tracks">������ ����������</param>
        /// <param name="listShemaA">������ �</param>
        /// <param name="listShemaB">������ �</param>
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
                ErrorReporting("�������� ��� ������ �����", e.Message);
            }
        }

        #endregion
    }
}