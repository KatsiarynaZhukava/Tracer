using System;
using System.IO;
using Tracer_Library.Serialization;
using Tracer_Library.Tracing;
using SPP_Tracer.Traced_Code;
using System.Threading;

namespace Console_Output_App
{
    internal class Program
    {
        private static ITracer _tracer = new Tracer();
        private static Foo foo = new Foo(_tracer);

        public static void Main(string[] args)
        {            

            Thread newThread = new Thread(new ThreadStart(FooNestedCall));
            newThread.Start();

            Thread anotherThread = new Thread(new ThreadStart(FooRecursionCall));
            anotherThread.Start();

            foo.SecondFooMethod();
            foo.ThirdFooMethod();

           
            Console.Write("Tracing started. Results will be written to:\n" +
                          "1. 'Results/XMLResult.xml' - the result of xml serialization\n" +
                          "2. 'Results/JSONResult.json - the result of json serialization\n" +
                          "3. console\n\n");

            Console.WriteLine("Serialization to XML:\n");


            FileStream fileStream = new FileStream("Results/XMLResult.xml", FileMode.OpenOrCreate);
            ISerializer serializer = new XmlSerializer();
            MemoryStream memoryStream = (MemoryStream)serializer.Serialize(_tracer.GetTraceResult());
            memoryStream.Position = 0;
            memoryStream.CopyTo(fileStream);

            memoryStream.Position = 0;
            memoryStream.CopyTo(Console.OpenStandardOutput());

            Console.WriteLine("\n\nSerialization to JSON:\n");

            fileStream = new FileStream("Results/JSONResult.json", FileMode.OpenOrCreate);
            serializer = new JsonSerializer();
            memoryStream = (MemoryStream)serializer.Serialize(_tracer.GetTraceResult());
            memoryStream.Position = 0;
            memoryStream.CopyTo(fileStream);

            memoryStream.Position = 0;
            memoryStream.CopyTo(Console.OpenStandardOutput());

            fileStream.Close();
            memoryStream.Close();

            Console.WriteLine("\n\nPress any key to exit...");
            Console.Read();
        }

        public static void FooNestedCall()
        {
            _tracer.StartTrace();

            foo.FirstFooMethod();

            _tracer.StopTrace();

        }

        public static void FooRecursionCall()
        {
            _tracer.StartTrace();

            foo.Recursion(5);

            _tracer.StopTrace();
        }
    }
}
