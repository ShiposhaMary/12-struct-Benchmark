using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
        /*Ётот интерфейс содержит единственный метод, 
         * принимающий task Ч действие, скорость работы которого 
         * нужно измерить. » возвращает длительность в миллисекундах.
         */
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            Stopwatch toMeasure = new Stopwatch();
            toMeasure.Start();
             task.Run();
            toMeasure.Reset();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            for (int i=0;i<repetitionCount; i++)
            {
                toMeasure.Start();
                task.Run();
                toMeasure.Stop();
            }
            
            return toMeasure.Elapsed.TotalMilliseconds/repetitionCount;
		}
	}

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        public class StBuilder : ITask

        {
            public void Run()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 1000; i++)
                {
                    sb.Append('a');
                }
                sb.ToString();
            }
        }
        public class StConstructor : ITask
        {
            public void Run()
            {
                var st = new String('a', 1000);
            }
        }
        [Test]
        /* сравнить два способа создани€ строки, состо€щей из 10000 букв 'а':
      —оздать StringBuilder, много раз вызвать Append, а в конце вызвать у
      него ToString().
      ¬ызвать специализированный конструктор строки new string('a', 10000).*/

        public void StringConstructorFasterThanStringBuilder()
        {
            var runner1 = new StBuilder();
            var runner2 = new StConstructor();
            var benchmark1 = new Benchmark();
            var benchmark2 = new Benchmark();
            Assert.Less(benchmark2.MeasureDurationInMs(runner2, 100000), benchmark1.MeasureDurationInMs(runner1, 100000));
        }
    }
}