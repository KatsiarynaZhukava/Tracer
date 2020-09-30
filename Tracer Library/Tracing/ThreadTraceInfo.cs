using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace Tracer_Library.Tracing
{
    [DataContract]
    public class ThreadTraceInfo
    {
        [DataMember(Order = 0)]
        private int id;
        [DataMember(Order = 1)]
        private double time;
        [DataMember(Order = 2)]
        private List<MethodTraceInfo> methods;

        public ThreadTraceInfo()
        {
            id = Thread.CurrentThread.ManagedThreadId;
            methods = new List<MethodTraceInfo>();
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public double Time
        {
            get => time;
            set => time = value;
        }


        public List<MethodTraceInfo> Methods
        {
            get => methods;
            set => methods = value;
        }

        public void AddMethod(MethodTraceInfo method)
        {
            bool isAlreadyinList = false;
            foreach(MethodTraceInfo _method in methods)
            {
                if (_method == method)
                {
                    isAlreadyinList = true;
                }
            }
            if (!isAlreadyinList)
            {
                methods.Add(method);
            }
        }

        public MethodTraceInfo GetMethod(int index)
        {
            return methods[index];
        }

        public void GetTime()
        {
            time = 0;
            foreach (MethodTraceInfo method in methods)
            {
                time += method.Time; 
            }
        }
    }
}