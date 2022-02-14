using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Не указан путь до каталога.");
                return;
            }
            Console.WriteLine($"Размер папки: {GetFolderSize(args[0])} байт");
            Console.WriteLine("Завершено.");
        }

        static long GetFolderSize(string path)
        {
            if (!Directory.Exists(path)) 
                return 0;
            long size = 0;
            try
            {
                var root = new DirectoryInfo(path);
                var files = root.GetFiles();
                var dirs = root.GetDirectories();
                foreach (var file in files)
                {
                    if (file.Exists) 
                        size += file.Length;
                }
                foreach (var dir in dirs)
                {
                    size += GetFolderSize(dir.FullName);
                }
                return size;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
