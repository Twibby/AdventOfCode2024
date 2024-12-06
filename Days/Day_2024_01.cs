using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_01 : DayScript2024
{
    protected override string part_1()
    {
        List<int> left = new List<int>();
        List<int> right = new List<int>();
        
        foreach (string instruction in _input.Split('\n'))
        {
            string[] values = instruction.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(values[0]));
            right.Add(int.Parse(values[1]));
        }

        left.Sort();
        right.Sort();

        double result = 0;
        for (int i=0; i < left.Count; i++)
        {
            result += Mathf.Abs(left[i] - right[i]);
        }

        return result.ToString() ;
    }

    protected override string part_2()
    {
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        foreach (string instruction in _input.Split('\n'))
        {
            string[] values = instruction.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(values[0]));
            right.Add(int.Parse(values[1]));
        }

        double result = 0;
        foreach (int val in left)
        {
            result += val * right.FindAll(x => x == val).Count;
        }

        return result.ToString();
    }
}
