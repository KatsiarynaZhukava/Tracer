using System.IO;
using Tracer_Library.Tracing;
using System.Runtime.Serialization;
using System.Xml;

namespace Tracer_Library.Serialization
{
    public class XmlSerializer : ISerializer
    {
        private readonly DataContractSerializer _serializer;
        private XmlWriterSettings _xmlWriterSettings;
        private MemoryStream _stream;

        public XmlSerializer()
        {
            _serializer = new DataContractSerializer(typeof(TraceResult));
            _xmlWriterSettings = new XmlWriterSettings { Indent = true };
            _stream = new MemoryStream();
        }


        public Stream Serialize(TraceResult traceResult)
        {
            var xmlWriter = XmlWriter.Create(_stream, _xmlWriterSettings);
                _serializer.WriteObject(xmlWriter, traceResult);
            xmlWriter.Flush();

            return _stream;
        }       
    }
}