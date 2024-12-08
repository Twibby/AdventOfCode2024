using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_07 : DayScript2024
{
    protected override string test_input()
    {
        return "190: 10 19\n3267: 81 40 27\n83: 17 5\n156: 15 6\n7290: 6 8 6 15\n161011: 16 10 13\n192: 17 8 14\n21037: 9 7 18 13\n292: 11 6 16 20";
    }

    protected override string part_1()
    {
        long result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            long total = long.Parse(instruction.Split(':')[0]);
            Queue<long> values = new Queue<long>(instruction.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse));

            long head = values.Dequeue();
            if (matchTotal(values, total, head, 0))
                result += total;
        }

        return result.ToString();
    }

    bool matchTotal(Queue<long> values, long total, long curSum, long logOffset )
    {
        if (curSum > total)
            return false;
        
        if (values.Count == 0)
            return curSum == total;

        long head = values.Dequeue();
        return matchTotal(new Queue<long>(values), total, curSum + head, logOffset+1) || matchTotal(new Queue<long>(values), total, curSum * head, logOffset+1);
    }


    protected override string part_2()
    {
        long result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            long total = long.Parse(instruction.Split(':')[0]);
            Queue<long> values = new Queue<long>(instruction.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse));

            long head = values.Dequeue();
            if ( matchTotalP2(values, total, head) )
                result += total;
        }

        return result.ToString();
    }

    bool matchTotalP2(Queue<long> values, long total, long curSum)
    {
          
        if (values.Count == 0)
            return curSum == total;

        if (curSum > total)
            return false;

        long head = values.Dequeue();

        return matchTotalP2(new Queue<long>(values), total, long.Parse(curSum.ToString() + head.ToString()))
            || matchTotalP2(new Queue<long>(values), total, curSum * head)
            || matchTotalP2(new Queue<long>(values), total, curSum + head);
    }

    //long eval(string formula)
    //{
    //    long res = 0;
    //    string number = "";
    //    char operand = ' ';
    //    for (int i = 0; i < formula.Length; i++)
    //    {
    //        if (formula[i] is '*' or '+' or '|')
    //        {

    //            switch (operand)
    //            {
    //                case '*': res *= long.Parse(number); break;
    //                case '+': res += long.Parse(number); break;
    //                case '|': res = long.Parse(res.ToString() + number); break;
    //                default: res = long.Parse(number); break;
    //            }
    //            operand = formula[i];
    //            number = "";
    //        }
    //        else if (formula[i] >= '0' && formula[i] <= '9')
    //        {
    //            number += formula[i].ToString();
    //        }
    //    }

    //    if (number != "")
    //    {
    //        switch (operand)
    //        {
    //            case '*': res *= long.Parse(number); break;
    //            case '+': res += long.Parse(number); break;
    //            case '|': res = long.Parse(res.ToString() + number); break;
    //            default: res = long.Parse(number); break;
    //        }
    //    }

    //    return res;
    //}

    //string matchInput(string formula)
    //{
    //    long total = eval(formula);

    //    string res = formula.Replace("|", " ").Replace("+", " ").Replace("*", " ");
    //    return total + ": " + res;
    //}

}
