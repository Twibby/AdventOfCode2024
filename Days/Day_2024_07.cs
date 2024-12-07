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
        double result = 0;
        foreach (string instruction in _input.Split('\n'))
        {
            double total = double.Parse(instruction.Split(':')[0]);
            //Debug.Log(instruction);
            Queue<double> values = new Queue<double>(instruction.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse));

            //Debug.LogWarning("Starting count for instruction " + instruction);
            double head = values.Dequeue();
            if (matchTotal(values, total, head, 0))
            {
                Debug.Log("match for instruction " + instruction);
                result += total;
            }
        }

        return result.ToString();
    }

    bool matchTotal(Queue<double> values, double total, double curSum, double logOffset )
    {
        //Debug.Log(Tools2024.WriteOffset(logOffset) + " curSum: " + curSum + " with values left : " + System.String.Join(" ", values.ToArray()));

        if (curSum > total)
            return false;
        
        if (values.Count == 0)
            return curSum == total;

        double head = values.Dequeue();
        return matchTotal(new Queue<double>(values), total, curSum + head, logOffset+1) || matchTotal(new Queue<double>(values), total, curSum * head, logOffset+1);
    }

    protected override string part_2()
    {
        double result = 0;
        //string log = "";
        foreach (string instruction in _input.Split('\n'))
        {
            double total = double.Parse(instruction.Split(':')[0]);
            Queue<double> values = new Queue<double>(instruction.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse));

            double head = values.Dequeue();
            var isMatch = matchTotalP2(values, total, head, 0, head.ToString());

            if (isMatch.Item1)
            {
                //if (result >= 685996237430442)
                //    log += isMatch.Item2 + "\t" + total.ToString("0") + "\n";
                
                double ev = eval(isMatch.Item2);
                if (ev != total)
                    Debug.LogWarning("match for instruction " + instruction + "\t--\t" + isMatch.Item2 + System.Environment.NewLine + ev.ToString("N1") + " - " + total.ToString("N1") + " | " + (ev == total).ToString());

                //Debug.Log("match for instruction " + instruction + "\t--\t" + isMatch.Item2 + System.Environment.NewLine + (result+total).ToString("N1") + "\t" + (result + total).ToString("0").Length);

                double oldres = result;
                result = result + total;
            }
        }

        //Debug.LogWarning(log);
        return result.ToString("N1");
    }

    (bool, string) matchTotalP2(Queue<double> values, double total, double curSum, double logOffset, string formula)
    {
        //Debug.Log(Tools2024.WriteOffset(logOffset) + " curSum: " + curSum + " with values left : " + System.String.Join(" ", values.ToArray()));

        if (curSum > total)
            return (false, "");

        if (values.Count == 0)
            return (curSum == total, formula);

        double head = values.Dequeue();
        (bool, string) tmp;

        tmp = matchTotalP2(new Queue<double>(values), total, double.Parse(curSum.ToString("0") + head.ToString("0")), logOffset + 1, formula + "|" + head);
        if (tmp.Item1)
            return tmp;
        
        tmp = matchTotalP2(new Queue<double>(values), total, curSum * head, logOffset + 1, formula + "*" + head);
        if (tmp.Item1)
            return tmp;

        tmp = matchTotalP2(new Queue<double>(values), total, curSum + head, logOffset + 1, formula + "+" + head);
        if (tmp.Item1)
            return tmp;

        return tmp;
        //return 
        //    || matchTotalP2(new Queue<double>(values), total, curSum * head, logOffset + 1, formula + "*" + head)
        //    || matchTotalP2(new Queue<double>(values), total, double.Parse(curSum.ToString() + head.ToString()), logOffset + 1, formula + "||" + head);
    }

    double eval(string formula)
    {
        double res = 0;
        string number = "";
        char operand = ' ';
        for (int i = 0; i < formula.Length; i++)
        {
            if (formula[i] is '*' or '+' or '|')
            {

                switch (operand)
                {
                    case '*': res *= double.Parse(number); break;
                    case '+': res += double.Parse(number); break;
                    case '|': res = double.Parse(res.ToString() + number); break;
                    default: res = double.Parse(number); break;
                }
                operand = formula[i];
                number = "";
            }
            else if (formula[i] >= '0' && formula[i] <= '9')
            {
                number += formula[i].ToString();
            }
        }

        if (number != "")
        {
            switch (operand)
            {
                case '*': res *= double.Parse(number); break;
                case '+': res += double.Parse(number); break;
                case '|': res = double.Parse(res.ToString() + number); break;
                default: res = double.Parse(number); break;
            }
        }

        return res;
    }
}
