using System;

namespace ImageLibrary
{
    public delegate void DataBaseStateHandler(object sender, DataBaseEventArgs e);

    public class DataBaseEventArgs
    {
        // Сообщение
        public string Message { get; private set; }

        //
        public string DescriptionMessage { get; private set; }

        public DataBaseEventArgs(string message, string description_message = "")
        {
            Message = message;
            DescriptionMessage = description_message;
        }
    }
}
