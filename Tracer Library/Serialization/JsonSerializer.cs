using System.IO;
using Tracer_Library.Tracing;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Tracer_Library.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private readonly DataContractJsonSerializer _serializer;
        MemoryStream _stream;

        public JsonSerializer()
        {
            _serializer = new DataContractJsonSerializer(typeof(TraceResult));
            _stream = new MemoryStream();
        }

        public Stream Serialize(TraceResult traceResult)
        {
            var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(_stream, Encoding.UTF8, false, true);
            _serializer.WriteObject(jsonWriter, traceResult);
            jsonWriter.Flush();
            return _stream;
        }
    }
}