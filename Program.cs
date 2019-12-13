using System;
using ImageLibrary;

namespace Image
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string argOne = args[0];

                //Трансформація даних
                if (argOne == "transformation")
                {
                    Kernel kern = new Kernel();

                    kern.Transformation(System.IO.Path.GetFullPath("..\\..\\XML\\"));

                    Console.WriteLine("ok");
                    Console.ReadLine();
                }
            }
            else
            {

                SplitFullPath("/root/image[@id=5]/image[@id=4]/image[@id=7]/");

                SplitFullPath("/root/image[@id=5]/");

                SplitFullPath("/root/image[@id=32]/image[@id = 300001  ]/image[@id=7]/image[@id=8]/image[@id=6]/");

                Console.WriteLine("ok");
                Console.ReadLine();
            }
        }

        private static void SplitFullPath(string fullPath)
        {
            string[] itemsFullPath = fullPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            int pStart = 0;
            int pEnd = 0;
            int length = 0;

            foreach (string itemFullPath in itemsFullPath)
            {
                Console.WriteLine(itemFullPath);

                pStart = itemFullPath.IndexOf("@id", 0);
                if (pStart > 0)
                {
                    pStart += 3;

                    pStart = itemFullPath.IndexOf("=", pStart);
                    if (pStart > 0)
                    {
                        pStart += 1;

                        pEnd = itemFullPath.IndexOf("]", pStart);

                        length = pEnd - pStart;

                        string shemaID = itemFullPath.Substring(pStart, length);

                        Console.WriteLine(shemaID);
                    }
                }
            }
        }
    }
}
