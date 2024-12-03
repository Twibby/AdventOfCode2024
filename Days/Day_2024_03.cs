using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Day_2024_03 : DayScript2024
{
    protected override string part_1()
    {
        if (IsTestInput)
            _input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        string pattern = "mul\\((\\d*),(\\d*)\\)";
        Regex reg = new Regex(pattern);
        MatchCollection matches = reg.Matches(_input);

        double result = 0;
        foreach (Match match in matches) 
        {
            Debug.Log(match.ToString() + " | " + System.String.Join(" -- ", match.Groups.Select(g => g.Value).ToList()));
            result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        if (IsTestInput)
            _input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        string pattern = "mul\\((\\d*),(\\d*)\\)|don't\\(\\)|do\\(\\)";
        Regex reg = new Regex(pattern);
        MatchCollection matches = reg.Matches(_input);

        double result = 0;
        bool compute = true;
        foreach (Match match in matches)
        {
            if (match.Value == "don't()")
            {
                compute = false;
                continue;
            }
            else if (match.Value == "do()")
            {
                compute = true;
                continue;
            }

            if (!compute)
                continue;

            result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return result.ToString();
    }
}
