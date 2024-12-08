using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_08 : DayScript2024
{
    protected override string test_input()
    {
        return "............\n........0...\n.....0......\n.......0....\n....0.......\n......A.....\n............\n............\n........A...\n.........A..\n............\n............";
    }

    protected override string part_1()
    {
        Dictionary<char, List<Vector2Int>> antennas = new Dictionary<char, List<Vector2Int>>();

        List<string> instructions = _input.Split('\n').ToList();
        Vector2Int roomSize = new Vector2Int(instructions.Count, instructions[0].Length);
        for (int i = 0; i < instructions.Count; i++)
        {
            for (int j = 0; j < instructions[i].Length; j++)
            {
                char c = instructions[i][j];
                if (c != '.')
                {
                    if (!antennas.ContainsKey(c))
                        antennas.Add(c, new List<Vector2Int>());

                    antennas[c].Add(new Vector2Int(i, j));
                }
            }
        }

        List<Vector2Int> antinodes = new List<Vector2Int>();
        foreach (List<Vector2Int> positions in antennas.Values)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    if (i == j)
                        continue;

                    Vector2Int dir = positions[j] - positions[i];
                    Vector2Int an = positions[i] - dir;
                    if (isInRoom(an, roomSize) && !antinodes.Contains(an)) // && !isOnAntenna(antennas, an))
                    {
                        antinodes.Add(an);
                    }
                }
            }
        }

        return antinodes.Count.ToString();
    }

    bool isInRoom(Vector2Int pos, Vector2Int size)
    {
        return pos.x >= 0 && pos.x < size.x && pos.y >= 0 && pos.y < size.y;
    }

    bool isOnAntenna(Dictionary<char, List<Vector2Int>> antennas, Vector2Int anPos)
    {
        foreach (List<Vector2Int> positions in antennas.Values)
        {
            foreach (Vector2Int pos in positions)
            {
                if (pos == anPos)
                    return true;
            }
        }
        return false;
    }

    protected override string part_2()
    {
        Dictionary<char, List<Vector2Int>> antennas = new Dictionary<char, List<Vector2Int>>();

        List<string> instructions = _input.Split('\n').ToList();
        Vector2Int roomSize = new Vector2Int(instructions.Count, instructions[0].Length);
        for (int i = 0; i < instructions.Count; i++)
        {
            for (int j = 0; j < instructions[i].Length; j++)
            {
                char c = instructions[i][j];
                if (c != '.')
                {
                    if (!antennas.ContainsKey(c))
                        antennas.Add(c, new List<Vector2Int>());

                    antennas[c].Add(new Vector2Int(i, j));
                }
            }
        }

        List<Vector2Int> antinodes = new List<Vector2Int>();
        foreach (List<Vector2Int> positions in antennas.Values)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = 0; j < positions.Count; j++)
                {
                    if (i == j)
                        continue;

                    Vector2Int dir = positions[j] - positions[i];
                    Vector2Int an = positions[i] - dir;
                    int safetyCount = 1000;
                    while (isInRoom(an, roomSize) && safetyCount > 0)
                    {
                        if (!antinodes.Contains(an)) // && !isOnAntenna(antennas, an))
                        {
                            antinodes.Add(an);
                        }
                        an = an - dir;
                        safetyCount--;
                    }
                    if (safetyCount < 0)
                    {
                        Debug.LogError("splaf");
                    }
                }
                
                if (!antinodes.Contains(positions[i]))
                    antinodes.Add(positions[i]);
            }
        }


        return antinodes.Count.ToString();
    }
}
