using System.IO;
using Tracer_Library.Tracing;
using System.Runtime.Serialization;

namespace Tracer_Library.Serialization
{
    public class XmlSerializer : ISerializer
    {
        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(TraceResult));
        
        public Stream Serialize(TraceResult traceResult)
        {
            MemoryStream stream = new MemoryStream();
            _serializer.WriteObject(stream, traceResult);
            return stream;
        }
        
    }
}