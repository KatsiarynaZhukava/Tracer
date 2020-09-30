using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Tracer_Library.Tracing
{
    [DataContract]
    public class MethodTraceInfo
    {
        public Stopwatch _stopwatch;

        [DataMember(Order = 0)]
        private string name;
        [DataMember(Order = 1)]
        private string className;
        [DataMember(Order = 2)]
        private double time;
        [DataMember(Order = 3)]
        private List<MethodTraceInfo> methods;

        public MethodTraceInfo(string name, string className)
        {
            this.name = name;
            this.className = className;
            methods = new List<MethodTraceInfo>();
        }

        public MethodTraceInfo()
        {
            StackFrame _stackFrame = new StackTrace().GetFrame(2);
            name = _stackFrame.GetMethod().Name;
            className = _stackFrame.GetMethod().DeclaringType.Name;

            _stopwatch = new Stopwatch();
            methods = new List<MethodTraceInfo>();
        }

        public double Time
        {
            get => time;
            set => time = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string ClassName
        {
            get => className;
            set => className = value;
        }

        public List<MethodTraceInfo> Methods
        {
            get => methods;
            set => methods = value;
        }

        internal void AddMethod(MethodTraceInfo method)
        {
            methods.Add(method);
        }

        public MethodTraceInfo GetMethod(int index)
        {
            return methods[index];
        }

        internal void StartTimeTracking()
        {
            _stopwatch.Start();
        }

        internal void StopTimeTracking()
        {
            _stopwatch.Stop();
            time = _stopwatch.ElapsedMilliseconds;
        }

        public void GetTime()
        {
            foreach (MethodTraceInfo method in methods)
            {
                time += method.Time;
            }
        }
    }
}