using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var students = Deserialize<Student[]>("Students.dat");
                var desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var studentDir = Path.Combine(desktopDir, "Students");
                if (!Directory.Exists(studentDir))
                    Directory.CreateDirectory(studentDir);
                foreach (var student in students)
                {
                    var file = Path.Combine(studentDir, student.Group + ".txt");
                    using (StreamWriter writer = File.AppendText(file))
                        writer.WriteLine($"{student.Name}, {student.DateOfBirth}");
                }
                Console.WriteLine("Завершено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static T Deserialize<T>(string fileName)
        {
            if (!File.Exists(fileName)) 
                throw new FileNotFoundException($"Файл {fileName} не найден.");
            try
            {
                var result = default(T);
                var formatter = new BinaryFormatter();
                using (var reader = new FileStream(fileName, FileMode.Open))
                {
                    result = (T)formatter.Deserialize(reader);
                }
                return result;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
