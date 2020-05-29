using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    /*массив структур и массив классов
Ќужно измерить врем€ дл€ структур и классов всех размеров,
указанных в Constants.FieldCounts.
–езультаты измерени€ вернуть в виде объекта ChartData.
ƒальше в Program.cs эти результаты будут показаны на графиках.
первый график скорости работы от количества полей в классе/структуре.
Ќа нЄм должно быть видно, что массивы классов создаютс€ дольше, 
чем массивы структур.*/
    {/* вызывают метод, передава€ в качестве аргумента класс 
    или структуру с большим количеством полей.
    классы передаютс€ в метод быстрее, чем большие структуры.*/
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var i in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult
                    (i, benchmark.MeasureDurationInMs
                        (new ClassArrayCreationTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult
                    (i, benchmark.MeasureDurationInMs
                        (new StructArrayCreationTask(i), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var i in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult
                    (i, benchmark.MeasureDurationInMs
                        (new MethodCallWithClassArgumentTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult
                    (i, benchmark.MeasureDurationInMs
                        (new MethodCallWithStructArgumentTask(i), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}