using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_06 : DayScript2024
{
    protected override string test_input()
    {
        return "....#.....\n.........#\n..........\n..#.......\n.......#..\n..........\n.#..^.....\n........#.\n#.........\n......#...";
    }

    private enum CellState { Empty, Block, Passed, PassedTop, PassedRight, PassedBot, PassedLeft  };
    private enum FacingDirection { Top, Right, Bot, Left };


    Vector2Int roomSize = Vector2Int.zero;
    
    protected override string part_1()
    {
        Dictionary<Vector2Int, CellState> cells = new Dictionary<Vector2Int, CellState>();
        roomSize = new Vector2Int(_input.Split('\n').Length, _input.Split('\n')[0].Length);

        int rowIndex = 0, colIndex= 0;
        Vector2Int guardPos = Vector2Int.zero;
        foreach(char c in _input)
        {
            if (c == '\n')
            {
                rowIndex++;
                colIndex = 0;
            }
            else
            {
                cells.Add(new Vector2Int(rowIndex, colIndex), c == '.' ? CellState.Empty : c == '#' ? CellState.Block : CellState.Passed);
                if (c == '^')
                    guardPos = new Vector2Int(rowIndex, colIndex);
                
                colIndex++;
            }
        }

        FacingDirection guardDir = FacingDirection.Top;

        int safetyCount = 10000;
        while (!isGuardOnEdge(guardPos) && safetyCount > 0)
        {
            safetyCount--;
            Vector2Int nextPos = guardPos;
            switch (guardDir)
            {
                case FacingDirection.Top: nextPos.x -= 1; break;
                case FacingDirection.Bot: nextPos.x += 1; break;
                case FacingDirection.Left: nextPos.y -= 1; break;
                case FacingDirection.Right: nextPos.y += 1; break;
            }

            if (!cells.ContainsKey(nextPos))
            {
                Debug.LogError("wtf");
                break;
            }

            if (cells[nextPos] == CellState.Block)
            {
                guardDir = getNextDirection(guardDir);
            }
            else
            {
                guardPos = nextPos;
                cells[nextPos] = CellState.Passed;
            }
        }

        return cells.Values.Where(c => c == CellState.Passed).Count().ToString();

        //return base.part_1();
    }

    bool isGuardOnEdge(Vector2Int guardPos)
    {
        return guardPos.x == 0 || guardPos.x == roomSize.x - 1 || guardPos.y == 0 || guardPos.y == roomSize.y - 1; 
    }

    FacingDirection getNextDirection(FacingDirection currentDir)
    {
        switch (currentDir)
        {
            case FacingDirection.Top: return FacingDirection.Right;
            case FacingDirection.Right: return FacingDirection.Bot;
            case FacingDirection.Bot: return FacingDirection.Left;
            case FacingDirection.Left: return FacingDirection.Top;
        }
        return FacingDirection.Top;
    }

    protected override string part_2()
    {
        Dictionary<Vector2Int, List<CellState>> cellsInit = new Dictionary<Vector2Int, List<CellState>>();
        roomSize = new Vector2Int(_input.Split('\n').Length, _input.Split('\n')[0].Length);

        int rowIndex = 0, colIndex = 0;
        Vector2Int initGuardPos = Vector2Int.zero;
        foreach (char c in _input)
        {
            if (c == '\n')
            {
                rowIndex++;
                colIndex = 0;
            }
            else
            {
                List<CellState> tmp = new List<CellState>();
                tmp.Add(c == '.' ? CellState.Empty : c == '#' ? CellState.Block : CellState.PassedTop);
                cellsInit.Add(new Vector2Int(rowIndex, colIndex), tmp);
                if (c == '^')
                    initGuardPos = new Vector2Int(rowIndex, colIndex);

                colIndex++;
            }
        }


        double result = 0;
        for (int i=0; i < roomSize.x; i++)
        {
            for (int j = 0; j < roomSize.y; j++)
            {
                Vector2Int obstaclePos = new Vector2Int(i, j);
                if (!cellsInit[obstaclePos].Contains(CellState.Empty))
                    continue;

                Dictionary<Vector2Int, List<CellState>> cells = new Dictionary<Vector2Int, List<CellState>>();
                foreach (var val in cellsInit) { cells.Add(val.Key, new List<CellState>( val.Value)); }
                cells[obstaclePos] = new List<CellState>() { CellState.Block };

                Vector2Int curGuardPos = initGuardPos;
                FacingDirection guardDir = FacingDirection.Top;

                int safetyCount = 10000;
                while (safetyCount > 0)
                {
                    safetyCount--;
                    Vector2Int nextPos = curGuardPos;
                    switch (guardDir)
                    {
                        case FacingDirection.Top: nextPos.x -= 1; break;
                        case FacingDirection.Bot: nextPos.x += 1; break;
                        case FacingDirection.Left: nextPos.y -= 1; break;
                        case FacingDirection.Right: nextPos.y += 1; break;
                    }

                    if (!cells.ContainsKey(nextPos))
                    {
                        Debug.LogError("wtf");
                        break;
                    }

                    if (cells[nextPos].Contains(CellState.Block))
                    {
                        guardDir = getNextDirection(guardDir);
                        CellState dirState = (CellState)((int)guardDir + 3);
                        if (cells[curGuardPos].Contains(dirState))
                        {
                            result++;
                            //Debug.Log("Worked with obstacle at " + obstaclePos  + " | cur: " + curGuardPos);
                            break;
                        }
                    }
                    else
                    {
                        if (isGuardOnEdge(nextPos))
                        {
                            //Debug.Log("Guard on edge with obstacle at " + obstaclePos + " | next: " + nextPos);
                            break;
                        }

                        CellState dirState = (CellState)((int)guardDir + 3);
                        if (cells[nextPos].Contains(dirState))
                        {
                            result++;
                            //Debug.Log("Worked with obstacle at " + obstaclePos + " | next: " + nextPos + " | steps:" + (10000-safetyCount).ToString());
                            break;
                        }

                        curGuardPos = nextPos;
                        cells[nextPos].Add(dirState);
                    }
                }
                if (safetyCount <= 0)
                {
                    Debug.LogError("woopsie on " + obstaclePos);
                    break;
                }
            }
        }

        return result.ToString();
    }
}
