using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Tracer_Library.Tracing;

namespace Unit_Tests
{

    [TestFixture]
    public class Tests
    {
        private static string className = "Tests";
        private static Tracer _tracer;

        [SetUp]
        public void SetUp()
        {
            _tracer = new Tracer();
        }

        public static void SingleCall()
        {
            _tracer.StartTrace();

            Thread.Sleep(100);

            _tracer.StopTrace();
        }


        static public void Recursion(int i)
        {
            _tracer.StartTrace();

            while (i > 0)
            {
                i--;
                Recursion(i);
            }

            _tracer.StopTrace();
        }

        static public void OuterMethod()
        {
            _tracer.StartTrace();

            InnerMethod();

            _tracer.StopTrace();
        }

        static private void InnerMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(100);

            _tracer.StopTrace();
        }

        static public void ThreadOneMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(1000);

            _tracer.StopTrace();
        }

        static public void ThreadTwoMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(10);

            _tracer.StopTrace();
        }

        static public void ThreadCreation()
        {
            Thread firstThread = new Thread(ThreadOneMethod);
            Thread secondThread = new Thread(ThreadTwoMethod);

            _tracer.StartTrace();
    
            firstThread.Start();
            secondThread.Start();

            _tracer.StopTrace();
        }

        static void Asserts(MethodTraceInfo expectedResult, MethodTraceInfo realResult)
        {
            Assert.AreEqual(expectedResult.Name, realResult.Name);
            Assert.AreEqual(expectedResult.ClassName, realResult.ClassName);
            Assert.AreEqual(expectedResult.Methods.Count, realResult.Methods.Count);
        }

        static void MultithreadingAsserts(List<ThreadTraceInfo> realResult, List<MethodTraceInfo> expectedResult)
        {
            Assert.AreEqual(realResult.Count, 3);
            Asserts(realResult[0].GetMethod(0), expectedResult[0]);
            Asserts(realResult[1].GetMethod(0), expectedResult[1]);
            Asserts(realResult[2].GetMethod(0), expectedResult[2]);
        }


        [Test]
        public void TestSingleCall()
        {
            SingleCall();

            var realResult = _tracer.GetTraceResult().GetThread(0).GetMethod(0);
            var expectedResult = new MethodTraceInfo("SingleCall", className);
            Asserts(expectedResult, realResult);
        }

        [Test]
        public void TestRecursionCall()
        {
            string methodName = "Recursion";
            Recursion(1);

            var realResult = _tracer.GetTraceResult().GetThread(0).GetMethod(0);
            var expectedResult = new MethodTraceInfo(methodName, className);
            expectedResult.Methods.Add(new MethodTraceInfo(methodName, className));
            expectedResult.GetMethod(0).Methods.Add(new MethodTraceInfo(methodName, className));

            Asserts(expectedResult, realResult);
        }

        [Test]
        public void TestNestedCall()
        {
            OuterMethod();

            var realResult = _tracer.GetTraceResult().GetThread(0).GetMethod(0);
            var expectedResult = new MethodTraceInfo("OuterMethod", className);
            expectedResult.Methods.Add(new MethodTraceInfo("InnerMethod", className));

            Asserts(expectedResult, realResult);
        }

        [Test]
        public void TestMultithreading()
        {
            ThreadCreation();
            List<ThreadTraceInfo> realResult = _tracer.GetTraceResult().Threads;

            List<MethodTraceInfo> expectedResult = new List<MethodTraceInfo>();
            expectedResult.Add(new MethodTraceInfo("ThreadCreation", className));
            expectedResult.Add(new MethodTraceInfo("ThreadOneMethod", className));
            expectedResult.Add(new MethodTraceInfo("ThreadTwoMethod", className));

            MultithreadingAsserts(realResult, expectedResult);
        }
    }
}