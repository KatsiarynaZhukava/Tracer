using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public void FirstFooMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(42);
            _bar.SomeBarMethod();

            _tracer.StopTrace();
        }

        public void SecondFooMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(100);

            _tracer.StopTrace();
        }    
        
        public void ThirdFooMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(100);

            _tracer.StopTrace();
        }

        public void Recursion(int i)
        {
            _tracer.StartTrace();

            i--;
            if (i > 0)
            {
                Thread.Sleep(100);
                Recursion(i);
            }

            _tracer.StopTrace();
        }
    }
}
