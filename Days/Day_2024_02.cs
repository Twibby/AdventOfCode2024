using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Day_2024_02 : DayScript2024
{
    protected override string part_1()
    {
        double result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            List<int> levels = instruction.Split(' ').Select(int.Parse).ToList();
            //Debug.Log(instruction);

            if (levels.Count < 2)
            {
                Debug.LogWarning("SHORT");

                result++;
                continue;
            }

            if (IsReportSafe(levels).Item1)
                result++;
        }

        return result.ToString();
    }

    public (bool,int) IsReportSafe(List<int> levels)
    {
        bool isIncr = levels[0] < levels[1];
        int lastValue = levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            if (levels[i - 1] == levels[i]
                || (isIncr != (levels[i - 1] < levels[i]))
                || Math.Abs(levels[i] - lastValue) > 3)
            {
                return (false, i);
            }
            lastValue = levels[i];
        }
        return (true, -1);
    }

    protected override string part_2()
    {
        //_input = "50 49 50 51 52 54 55";

        double result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            List<int> levels = instruction.Split(' ').Select(int.Parse).ToList();
            //Debug.Log(instruction);

            if (levels.Count < 2)
            {
                Debug.LogWarning("SHORT");

                result++;
                continue;
            }

            (bool,int) reportSafety = IsReportSafe(levels);
            if (reportSafety.Item1)
                result++;
            else
            {
                List<int> subLevels = new List<int>(levels);
                subLevels.RemoveAt(reportSafety.Item2 -1);
                if (IsReportSafe(subLevels).Item1)
                {
                    result++;
                    continue;
                }

                subLevels = new List<int>(levels);
                subLevels.RemoveAt(reportSafety.Item2);
                if (IsReportSafe(subLevels).Item1)
                {
                    result++;
                    continue;
                }

                // Special case when 3 first numbers are v or ^ (increase then decrease or the opposite, eg : 50 49 50)
                // In that case, the IsReportSafe unction returns false because 49 50 is incr, but deleting the first digit may create a safe report
                if (reportSafety.Item2 == 2)
                {
                    subLevels = new List<int>(levels);
                    subLevels.RemoveAt(reportSafety.Item2 - 2);
                    if (IsReportSafe(subLevels).Item1)
                    {
                        result++;
                        continue;
                    }
                }
            }
        }

        return result.ToString();
    }
}
