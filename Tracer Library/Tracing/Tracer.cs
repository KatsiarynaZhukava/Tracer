namespace Tracer_Library.Tracing
{
    public class Tracer : ITracer
    {
        private TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult();
        }

        public TraceResult TraceResult
        {
            get => _traceResult;
            set => _traceResult = value;
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            
        }

        public void StopTrace()
        {
            
        }
        

    }
}