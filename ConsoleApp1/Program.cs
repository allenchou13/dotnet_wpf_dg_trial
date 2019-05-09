using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Item
    {
        public string BatchName { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"{BatchName}--{Number}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Item>();
            list.Add(new Item { BatchName = "BOne", Number = 10 });
            list.Add(new Item { BatchName = "BTwo", Number = 3 });
            list.Add(new Item { BatchName = "BOne", Number = 3 });
            list.Add(new Item { BatchName = "BOne", Number = 4 });
            list.Add(new Item { BatchName = "BTwo", Number = 5 });
            list.Add(new Item { BatchName = "BOne", Number = 5 });
            list.Add(new Item { BatchName = "BOne", Number = 7 });
            list.Add(new Item { BatchName = "BTwo", Number = 8 });
            list.Add(new Item { BatchName = "B", Number = 8 });

            var groups = from i in list
                         group i by i.BatchName into g
                         orderby g.Key
                         select g;
            var batches = new List<List<Item>>();
            foreach(var g in groups)
            {
                var g_ordered = from i in g orderby i.Number select i;
                batches.Add(g_ordered.ToList());
            }

            batches.SelectMany(i => i)
                .ToList()
                .ForEach(i => Console.WriteLine(i));

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
