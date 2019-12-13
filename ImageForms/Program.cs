using System;
using System.Windows.Forms;
using ImageLibrary;

namespace ImageForms
{
    static class Program
    {
        /// <summary>
        /// Головний канал роботи з бібліотекою
        /// </summary>
        public static Kernel GlobalKernel;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalKernel = new Kernel();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormFirst());
        }

        #region USER GLOBAL_FUNCTION

        /// <summary>
        /// Нормалізує рядки в DataGridViewRowCollection
        /// </summary>
        /// <param name="GetListCount">Кількість рядків у вибірці з бази даних</param>
        /// <param name="dataGridViewForNormalize">Колекція рядків які треба нормалізувати</param>
        public static void Global_NormalizeDataGridRows(int GetListCount, DataGridViewRowCollection dataGridViewForNormalizeRows)
        {
            //Скільки зараз рядків у таблиці
            int dataGridViewRowsCount = dataGridViewForNormalizeRows.Count;

            //Видаляєм лишні рядки
            //20 -> 10
            if (dataGridViewRowsCount > GetListCount)
                for (int i = dataGridViewRowsCount; i > GetListCount; i--)
                    dataGridViewForNormalizeRows.RemoveAt(i - 1);

            //Добавляєм якщо недостає
            //10 <- 20
            if (GetListCount > dataGridViewRowsCount)
                for (int i = dataGridViewRowsCount; i < GetListCount; i++)
                    dataGridViewForNormalizeRows.Add();
        }


        #endregion

    }
}
