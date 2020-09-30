using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tracer_Library.Tracing
{
    [DataContract]
    public class TraceResult
    {
        [DataMember]
        private List<ThreadTraceInfo> threads;
        private ConcurrentDictionary<int, Stack<MethodTraceInfo>> _stacks;

        public TraceResult()
        {
            threads = new List<ThreadTraceInfo>();
        }

        public List<ThreadTraceInfo> Threads
        {
            get => threads;
            set => threads = value;
        }

        public ConcurrentDictionary<int, Stack<MethodTraceInfo>> Stacks
        {
            get => _stacks;
            set => _stacks = value;
        }

        internal void AddNewThread(ThreadTraceInfo thread)
        {
            threads.Add(thread);
        }

        public ThreadTraceInfo GetThread(int index)
        {
            return threads[index];
        }
    }
}