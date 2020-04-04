using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ObservableList
{
    class Program
    {
        static void Main(string[] args)
        {

            ObservableList<int> ints = new ObservableList<int>();

            ints.ListChanged += Ints_ListChanged;

            ints.AddRange(new[] { 1, 2, 3, 4, 5, 7, 8, 9 });

            ints.Add(175);

            ints.Remove(175);

            ints.Insert(2, 56);

            ints.RemoveRange(1, 2);

            Console.ReadLine();
        }

        private static void Ints_ListChanged(object sender, NotifyListChangedEventArgs<int> e)
        {
            if (e.Action == NotifyListChangedType.Add)
            {
                Console.WriteLine($"Added item {e.Item}");
            }
            else if (e.Action == NotifyListChangedType.AddRange)
            {
                e.ForEachItems(x =>
                {
                    Console.WriteLine($"Added {x}");
                });
            }
            else if (e.Action == NotifyListChangedType.RemoveRange)
            {
                e.ForEachItems(x =>
                {
                    Console.WriteLine($"Removed {x}");
                });
            }
            else if (e.Action == NotifyListChangedType.Remove)
            {
                Console.WriteLine($"Removed item {e.Item}");
            }
            else if (e.Action == NotifyListChangedType.Insert)
            {
                Console.WriteLine($"Insert item {e.Item}");
            }
        }
    }
}
