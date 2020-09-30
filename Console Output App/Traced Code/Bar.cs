using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer_Library.Tracing;

namespace SPP_Tracer.Traced_Code
{
    class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void SomeBarMethod()
        {
            _tracer.StartTrace();

            AnotherBarMethod();
            Thread.Sleep(100);

            _tracer.StopTrace();
        }

        public void AnotherBarMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(10);
            _tracer.StopTrace();
        }
    }
}
