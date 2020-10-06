using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C_Sharp_Reflection
{
    class Program
    {
        private static Dictionary<string, string> _methodDictionary;
        static void Main(string[] args)
        {
            _methodDictionary = new Dictionary<string, string>();
            _methodDictionary = GetMethodsDictionary();

            var type = typeof(StudentFunction);
            var studentFunctionInstance = Activator.CreateInstance(type);

            var testString = "Hello [GetName], your university name is [GetUniversity] and roll is [GetRoll]";
            var match = Regex.Matches(testString, @"\[([A-Za-z0-9\-]+)]", RegexOptions.IgnoreCase);
            foreach (var v in match)
            {
                var originalString = v.ToString();
                var x = v.ToString();
                x = x.Replace("[", "");
                x = x.Replace("]", "");
                x = _methodDictionary[x];

                var toInvoke = type.GetMethod(x);
                var result = toInvoke.Invoke(studentFunctionInstance, null);
                testString = testString.Replace(originalString, result.ToString());
            }

            Console.WriteLine(testString);

            Console.ReadKey();
        }

        private static Dictionary<string, string> GetMethodsDictionary()
        {
            var dictionary = new Dictionary<string, string>
        {
            {"GetName", "GetName"},
            {"GetUniversity", "GetUniversity"},
            {"GetRoll","GetRoll"}
        };
            return dictionary;
        }
    }
}
