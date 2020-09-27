using System.IO;

namespace Tracer_Library.Serialization
{
    public interface ISerializer
    {
        Stream Serialize(Tracing.TraceResult traceResult);
    } 
}