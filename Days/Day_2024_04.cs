using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_04 : DayScript2024
{
    List<List<char>> rows = new List<List<char>>();
    protected override string part_1()
    {
        if (IsTestInput)
            _input = "MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX";

        double result = 0;
        rows = _input.Split('\n').Select(r => r.ToCharArray().ToList()).ToList();
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i].Count; j++)
            {
                if (rows[i][j] == 'X')
                    result += countXmasFromPos(i, j);
            }
        }     

        return result.ToString();
    }

    double countXmasFromPos(int i, int j)
    {
        double result = 0;
        // top
        if (i >= 3 && rows[i - 1][j] == 'M' && rows[i - 2][j] == 'A' && rows[i - 3][j] == 'S')
            result++;

        // bot
        if (i < rows.Count -3 && rows[i + 1][j] == 'M' && rows[i + 2][j] == 'A' && rows[i + 3][j] == 'S')
            result++;

        // left
        if (j >= 3 && rows[i][j - 1] == 'M' && rows[i][j - 2] == 'A' && rows[i][j - 3] == 'S')
            result++;

        // right
        if (j < rows[i].Count - 3 && rows[i][j + 1] == 'M' && rows[i][j + 2] == 'A' && rows[i][j + 3] == 'S')
            result++;


        // top left
        if (i >= 3 && j >= 3 && rows[i - 1][j - 1] == 'M' && rows[i - 2][j - 2] == 'A' && rows[i - 3][j - 3] == 'S')
            result++;

        // bot left
        if (i < rows.Count - 3 && j >= 3 && rows[i + 1][j - 1] == 'M' && rows[i + 2][j - 2] == 'A' && rows[i + 3][j - 3] == 'S')
            result++;

        // top right
        if (i >= 3 && j < rows[i].Count - 3 && rows[i - 1][j + 1] == 'M' && rows[i - 2][j + 2] == 'A' && rows[i - 3][j + 3] == 'S')
            result++;

        // bot right
        if (i < rows.Count - 3 && j < rows[i].Count - 3 && rows[i + 1][j + 1] == 'M' && rows[i + 2][j + 2] == 'A' && rows[i + 3][j + 3] == 'S')
            result++;



        return result;
    }

    protected override string part_2()
    {
        if (IsTestInput)
            _input = "MMMSXXMASM\nMSAMXMSMSA\nAMXSXMAAMM\nMSAMASMSMX\nXMASAMXAMM\nXXAMMXXAMA\nSMSMSASXSS\nSAXAMASAAA\nMAMMMXMMMM\nMXMXAXMASX";

        double result = 0;
        rows = _input.Split('\n').Select(r => r.ToCharArray().ToList()).ToList();
        for (int i = 1; i < rows.Count -1; i++)
        {
            for (int j = 1; j < rows[i].Count -1; j++)
            {
                if (rows[i][j] == 'A')
                    result += countX_masFromPos(i, j);
            }
        }

        return result.ToString();
    }

    double countX_masFromPos(int i, int j)
    {
        double result = 0;

        // both M on top
        if (rows[i - 1][j -1] == 'M' && rows[i - 1][j + 1] == 'M' && rows[i + 1][j - 1] == 'S' && rows[i + 1][j + 1] == 'S')
            result++;

        // Ms on bot
        if (rows[i - 1][j - 1] == 'S' && rows[i - 1][j + 1] == 'S' && rows[i + 1][j - 1] == 'M' && rows[i + 1][j + 1] == 'M')
            result++;

        // Ms on left
        if (rows[i - 1][j - 1] == 'M' && rows[i - 1][j + 1] == 'S' && rows[i + 1][j - 1] == 'M' && rows[i + 1][j + 1] == 'S')
            result++;

        // Ms on right
        if (rows[i - 1][j - 1] == 'S' && rows[i - 1][j + 1] == 'M' && rows[i + 1][j - 1] == 'S' && rows[i + 1][j + 1] == 'M')
            result++;

        return result;
    }
}
