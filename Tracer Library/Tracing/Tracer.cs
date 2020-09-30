using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Tracer_Library.Tracing
{
    public class Tracer : ITracer
    {
        private TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult();
            _traceResult.Stacks = new ConcurrentDictionary<int, Stack<MethodTraceInfo>>();
        }

        public TraceResult GetTraceResult()
        {
            foreach (ThreadTraceInfo thread in _traceResult.Threads)
            {
                thread.GetTime();
            }
            return _traceResult;
        }

        public void StartTrace()
        {
            MethodTraceInfo method = new MethodTraceInfo();
            ThreadTraceInfo thread = new ThreadTraceInfo();

            int threadId = Thread.CurrentThread.ManagedThreadId;
            _traceResult.Stacks.TryAdd(threadId, new Stack<MethodTraceInfo>());
            
          
            if (_traceResult.Stacks[threadId].Count == 0)
            {
                thread = _traceResult.Threads.Find(item => item.Id == threadId);
                if (thread == null)
                {
                    thread = new ThreadTraceInfo();
                    _traceResult.AddNewThread(thread);
                }
                thread.AddMethod(method);
            }              
            else
            {
                _traceResult.Stacks[threadId].Peek().AddMethod(method);
            }
            _traceResult.Stacks[threadId].Push(method);

        
            method.StartTimeTracking();
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            var method = _traceResult.Stacks[threadId].Pop();
            method.StopTimeTracking();           
        }
    }
}