using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dob = new DateTime();
            DateTime.TryParseExact( "2020" + "-"+  "12" + "-" + "31", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dob);

            var datetime = Convert.ToDateTime("01/01/01");
            Console.ReadLine();
        }
    }
}
