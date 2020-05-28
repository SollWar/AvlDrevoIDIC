using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlDrevoIDIC
{
    class Program
    {
        static void Main(string[] args)
        {
            //  var dict = new SortedDictionary< string, int > ();//AVLTree

            // dict.Add("asdf", 53);
            // dict.Add("avd", 76);
            // dict["gsdfg"] = 100;
            // Console.WriteLine(dict["gsdfg"]);

            //string input_text = System.IO.File.ReadAllText(@"100.txt");

            // SortedDictionary Dictionary AVLTree
            var obj = new AVLTree<string, int>();

            obj.Add("Nikita", 1);
            obj.Add("Maxim", 1);



            Console.WriteLine(obj.ContainsKey("Nikita"));
            Console.ReadKey();

            /*
            string str = "";
            foreach (var i in input_text)
            {
                if (i >= 'a' && i <= 'z' || i >= 'A' && i <= 'Z' || i == '\'')
                {
                    str += i;
                }
                else if (str.Length > 0)
                {
                    if (obj.ContainsKey(str))
                        ++obj[str];
                    else
                        obj.Add(str, 1);
                    str = "";
                }
            }
            */
            // foreach(var i in obj)
            //  Console.WriteLine(i.Key + " " + i.Value);
            //Console.WriteLine("about " + obj["about"]);
        }
    }
}
