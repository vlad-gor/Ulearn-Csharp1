using System.Collections.Generic;
using System.Diagnostics;

namespace Profiling
{
	public class ProfilerTask : IProfiler
	{
		public List<ExperimentResult> Measure(IRunner runner, int repetitionsCount)
		{			
		    var results = new List<ExperimentResult>();
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var cTime = GetCallTime(true, runner, repetitionsCount, fieldCount);
                var sTime = GetCallTime(false,runner, repetitionsCount, fieldCount);
                results.Add(new ExperimentResult(fieldCount,
                    (double)cTime/repetitionsCount,
                    (double)sTime/repetitionsCount));
            }
            return results;
        }

        private static long GetCallTime(bool isClass,IRunner runner, int repetitionsCount, int fieldCount)
        {
            runner.Call(isClass, fieldCount, 1);
            var sStopwatch = Stopwatch.StartNew();
            runner.Call(isClass, fieldCount, repetitionsCount);
            return sStopwatch.ElapsedMilliseconds;
        }
    }
}