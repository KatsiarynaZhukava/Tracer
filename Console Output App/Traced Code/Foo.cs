using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer_Library.Tracing;

namespace SPP_Tracer.Traced_Code
{
    class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void SomeFooMethod()
        {
            _tracer.StartTrace();

            for (int i = 0; i < 10000; i++)
            {
                int j = i;
            }

            _bar.SomeBarMethod();

            _tracer.StopTrace();
        }
    }
}
