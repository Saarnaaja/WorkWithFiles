using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { set; get; }
        public DateTime DateOfBirth { set; get; }

        public override string ToString()
        {
            return $"Имя: {Name}, Группа: {Group}, Дата рождения: {DateOfBirth}";
        }
    }
}
