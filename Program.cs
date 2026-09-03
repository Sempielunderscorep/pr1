using System;
using System.Threading;
using System.Collections.Concurrent;

class Program{
    static BlockingCollection<string> collection = new BlockingCollection<string>();
    public static void Main(string[] args){
        Thread first = new Thread(Fill);
        Thread second = new Thread(MatchFizz);
        Thread third = new Thread(MatchBuzz);
        first.Start();
        //second.Start();
        //third.Start();

        while(first.IsAlive || (collection.Count != 0)){
            if(!second.IsAlive){
                second = new Thread(MatchFizz);
                second.Start();
            }
            if(!third.IsAlive){
                third = new Thread(MatchBuzz);
                third.Start();
            }
        }
    }
    public static void Fill(){
        string fizzbuzz = "";
        for (int i = 0;i < 20; i++)
        {
            fizzbuzz = "";
            if (i % 2 == 0){
                fizzbuzz += "fizz";
            }
            if (i % 3 == 0){
                fizzbuzz += "buzz";
            }
            collection.Add(fizzbuzz);
            Console.WriteLine("FILL: Added item!");
        }
    }
    public static void MatchFizz(){
        string item = collection.Take();
        if (item.Contains("fi"))
        {
            Console.WriteLine("FIZZ: Found fizz!");
        }
    }
    public static void MatchBuzz(){
        string item = collection.Take();
        if (item.Contains("bu"))
        {
            Console.WriteLine("BUZZ: Found buzz!");
        }
    }
}
