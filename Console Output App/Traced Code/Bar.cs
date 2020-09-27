using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            for (int i = 0; i < 100000; i++)
            {
                int j = i*2;
            }

            _tracer.StopTrace();
        }

        internal void AnotherBarMethod()
        {
            _tracer.StartTrace();

            for (int i = 0; i < 1000000; i++)
            {
                int j = i * 2;
            }

            _tracer.StopTrace();
        }
    }
}
