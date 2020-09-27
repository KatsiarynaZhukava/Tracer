using System;
using System.IO;
using Tracer_Library.Serialization;
using Tracer_Library.Tracing;
using SPP_Tracer.Traced_Code;

namespace Console_Output_App
{
    internal class Program
    {
        private static ITracer _tracer = new Tracer();
        private static TraceResult _traceResult;
        
        public static void Main(string[] args)
        {
            Foo foo = new Foo(_tracer);
            foo.SomeFooMethod();

            _traceResult = _tracer.GetTraceResult();


            
            Console.Write("Tracing started. Results will be written to:\n" +
                          "1. 'Results/XMLResult.xml' - the result of xml serialization\n" +
                          "2. 'Results/JSONResult.json - the result of json serialization\n" +
                          "3. console\n");

            //Serialization to XML
            FileStream fileStream = new FileStream("Results/XMLResult.xml", FileMode.OpenOrCreate);
            ISerializer serializer = new XmlSerializer();
            MemoryStream memoryStream = (MemoryStream)serializer.Serialize(_traceResult);
            memoryStream.CopyTo(fileStream);
            memoryStream.CopyTo(Console.OpenStandardOutput());

            //Serialization to JSON
            fileStream = new FileStream("Results/JSONResult.json", FileMode.OpenOrCreate);
            serializer = new JsonSerializer();
            memoryStream = (MemoryStream)serializer.Serialize(_traceResult);
            memoryStream.CopyTo(fileStream);
            memoryStream.CopyTo(Console.OpenStandardOutput());
        }
    }
}
