using System.Diagnostics;
using System.Threading.Tasks;

namespace Week2_Tutorial1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();

            Console.WriteLine("Hello World!");
            Console.WriteLine("Main thread has started");
            Console.WriteLine("Main ThreadID: " + Environment.CurrentManagedThreadId);
            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine("Thread {0}from the thread pool.", Thread.CurrentThread.IsThreadPoolThread ? "" : "not ");

            Task t1 = new(method1);
            Task t2 = new(method2);
            Task t3 = new(method3); 

            t1.Start();
            t2.Start();
            t3.Start();

            Console.WriteLine("Main thread has completed");
            stopWatch.Stop();

            Console.WriteLine("RunTime in milliseconds: " + stopWatch.ElapsedMilliseconds);
        }

        static void method1()
        {
            Console.WriteLine("This is Method 1");
            Console.WriteLine("ThreadID: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("Thread {0}from the thread pool.", Thread.CurrentThread.IsThreadPoolThread ? "" : "not ");
            Thread.Sleep(200);

        }

        static void method2()
        {
            Console.WriteLine("This is Method 2");
            Console.WriteLine("ThreadID: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("Thread {0}from the thread pool.", Thread.CurrentThread.IsThreadPoolThread ? "" : "not ");
            Thread.Sleep(200);
        }

        static void method3()
        {
            Console.WriteLine("This is Method 3");
            Console.WriteLine("ThreadID: " + Environment.CurrentManagedThreadId);
            Console.WriteLine("Thread {0}from the thread pool.", Thread.CurrentThread.IsThreadPoolThread ? "" : "not ");
            Thread.Sleep(200);
        }
    }
}