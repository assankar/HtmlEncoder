using System;
using System.Collections.Generic;

namespace Htmlencoder
{
    class HtmlEncoder
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Tuple<int, int> t1 = new Tuple<int, int>(0, 9);
            Tuple<int, int> t2 = new Tuple<int, int>(11, 13);
            Tuple<int, int> t3 = new Tuple<int, int>(0, 9);
            List<Tuple<int, int>> l1 = new List<Tuple<int, int>>();
            l1.Add(t1);
            l1.Add(t2);
            List<Tuple<int, int>> l2 = new List<Tuple<int, int>>();
            l2.Add(t3);
            Dictionary<string, List<Tuple<int, int>>> dict = new Dictionary<string, List<Tuple<int, int>>>();
            dict["<p>"] = l1;
            dict["<div>"] = l2;
            string text = "hellomynameishellomynameishellomynameisslimshady";
            HtmlEncoder p = new HtmlEncoder();
            for(int i = 0; i < 5; i++)
            {
                string result = p.MakeHtml(dict, text);
                Console.WriteLine(result);
            }

        }
        public HtmlEncoder()
        {

        }

        public string MakeHtml(Dictionary<string, List<Tuple<int, int>>> dict, string text)
        {
            List<Tuple<int, int, string>> list = new List<Tuple<int, int, string>> ();
            Stack<Tuple<int, string>> stack = new Stack<Tuple<int, string>> ();

            foreach (var key in dict.Keys)
            {
                foreach (var val in dict[key])
                {
                    list.Add(new Tuple<int, int, string>(val.Item1, val.Item2, key));
                }
            }
            list.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            string result = "";
            int marker = 0;
            int listIndex = 0;

            while (list.Count != 0 || stack.Count != 0)
            {
                if (list.Count != 0 && stack.Count == 0)
                {
                    result = result + text.Substring(marker, list[listIndex].Item1 - marker) + list[listIndex].Item3;
                    marker = list[listIndex].Item1;

                    string endingtag = @"</" + list[listIndex].Item3.Substring(1);
                    stack.Push(new Tuple<int, string>(list[listIndex].Item2, endingtag));

                    list.RemoveAt(0);
                }
                else if(list.Count == 0 && stack.Count != 0)
                {
                    result = result + text.Substring(marker, stack.Peek().Item1 - marker) + stack.Peek().Item2;
                    marker = stack.Peek().Item1;
                    stack.Pop();
                }
                else if (list[0].Item1 < stack.Peek().Item1)
                {
                    result = result + text.Substring(marker, list[listIndex].Item1 - marker) + list[listIndex].Item3;
                    marker = list[listIndex].Item1;

                    string endingtag = @"</" + list[listIndex].Item3.Substring(1);
                    stack.Push(new Tuple<int, string>(list[listIndex].Item2, endingtag));

                    list.RemoveAt(0);
                }
                else if (list[0].Item1 >= stack.Peek().Item1)
                {
                    result = result + text.Substring(marker, stack.Peek().Item1 - marker) + stack.Peek().Item2;
                    marker = stack.Peek().Item1;
                    stack.Pop();
                }
            }
            result = result + text.Substring(marker, text.Length-marker);
            return result;
        }

        public string HtmlThing(Dictionary<string, List<Tuple<int,int>>> dict, string text)
        {
            /*
            Tuple<int, int> t1 = new Tuple<int, int>(0, 9);
            Tuple<int, int> t2 = new Tuple<int, int>(11, 13);
            Tuple<int, int> t3 = new Tuple<int, int>(0, 9);
            List<Tuple<int, int>> l1 = new List<Tuple<int, int>>();
            l1.Add(t1);
            l1.Add(t2);
            List<Tuple<int, int>> l2 = new List<Tuple<int, int>>();
            l2.Add(t3);
            Dictionary<string, List<Tuple<int, int>>> dict = new Dictionary<string, List<Tuple<int, int>>>();
            dict["<p>"] = l1;
            dict["<div>"] = l2;
            */

            // "<p>", [0,9],[11,13]
            // "<div>", [11,13]

            List<Tuple<int, int, string>> list = new List<Tuple<int, int, string>>();
            Stack<Tuple<int, string>> stack = new Stack<Tuple<int, string>>();


            foreach (var key in dict.Keys)
            {
                foreach(var val in dict[key])
                {
                    list.Add(new Tuple<int, int, string> (val.Item1, val.Item2, key));
                }
            }
            list.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            string result = "";
            int marker = 0;
            int listIndex = 0;

            while (list.Count != 0 || stack.Count != 0)
            {
                if (list.Count != 0 && stack.Count == 0)
                {
                    result = result + text.Substring(marker, list[listIndex].Item1 - marker) + list[listIndex].Item3;
                    marker = list[listIndex].Item1;

                    string endingtag = @"</" + list[listIndex].Item3.Substring(1);
                    stack.Push(new Tuple<int, string>(list[listIndex].Item2, endingtag));

                    list.RemoveAt(0);
                }
                else if(list.Count == 0 && stack.Count != 0)
                {
                    result = result + text.Substring(marker, stack.Peek().Item1 - marker) + stack.Peek().Item2;
                    marker = stack.Peek().Item1;
                    stack.Pop();
                }
                else if(list[0].Item1 < stack.Peek().Item1)
                {
                    result = result + text.Substring(marker, list[listIndex].Item1 - marker) + list[listIndex].Item3;
                    marker = list[listIndex].Item1;

                    string endingtag = @"</" + list[listIndex].Item3.Substring(1);
                    stack.Push(new Tuple<int, string>(list[listIndex].Item2, endingtag));

                    list.RemoveAt(0);

                }
                else if(list[0].Item1 >= stack.Peek().Item1)
                {
                    result = result + text.Substring(marker, stack.Peek().Item1 - marker) + stack.Peek().Item2;
                    marker = stack.Peek().Item1;
                    stack.Pop();
                }
            }

            result = result + text.Substring(marker, text.Length - marker);
            return result;
        }
    }
}
