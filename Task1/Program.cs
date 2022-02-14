using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
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
            ClearDirectory(args[0]);
            Console.WriteLine("Завершено.");
        }

        static void ClearDirectory(string path, bool removeRoot = false)
        {
            if (!Directory.Exists(path))
                return;
            try
            {
                var root = new DirectoryInfo(path);
                var dirs = root.GetDirectories();
                var files = root.GetFiles();
                foreach (var fileInfo in files)
                {
                    if (DateTime.Now - fileInfo.LastWriteTime >= TimeSpan.FromMinutes(30))
                    {
                        try
                        {
                            fileInfo.Delete();
                        }
                        catch (Exception ex) //Ловим ошибку, если файл занят или нет прав на удаление
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                foreach (var dirInfo in dirs)
                {
                    ClearDirectory(dirInfo.FullName, true);
                }
                if (removeRoot && root.GetDirectories().Length == 0 && root.GetFiles().Length == 0)
                    root.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
