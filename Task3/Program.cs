using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
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
            Console.WriteLine($"Исходный размер папки: {GetFolderSize(args[0])} байт");
            var removed = ClearDirectory(args[0]);
            Console.WriteLine($"Удалено файлов: {removed.count}");
            Console.WriteLine($"Освобождено: {removed.size} байт");
            Console.WriteLine($"Текущий размер папки: {GetFolderSize(args[0])} байт");
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

        static (int count, long size) ClearDirectory(string path, bool removeRoot = false)
        {
            if (!Directory.Exists(path))
                return (0, 0);
            (int count, long size) value = (0, 0);
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
                            value.count++;
                            value.size += fileInfo.Length;
                            fileInfo.Delete();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                foreach (var dirInfo in dirs)
                {
                    (int count, long size) = ClearDirectory(dirInfo.FullName, true);
                    value.count += count;
                    value.size += size;
                }
                if (removeRoot && root.GetDirectories().Length == 0 && root.GetFiles().Length == 0) 
                    root.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return value;
        }
    }
}
