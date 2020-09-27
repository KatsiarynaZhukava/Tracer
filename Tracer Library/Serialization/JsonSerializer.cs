using System.IO;
using Tracer_Library.Tracing;
using System.Runtime.Serialization.Json;

namespace Tracer_Library.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(TraceResult));
        
        public Stream Serialize(TraceResult traceResult)
        {
            MemoryStream stream = new MemoryStream();
            _serializer.WriteObject(stream, traceResult);
            return stream;
        }
    }
}