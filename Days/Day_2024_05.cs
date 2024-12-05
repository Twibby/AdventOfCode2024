using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day_2024_05 : DayScript2024
{
    protected override string test_input()
    {
        return "47|53\n97|13\n97|61\n97|47\n75|29\n61|13\n75|53\n29|13\n97|29\n53|29\n61|53\n97|53\n61|29\n47|13\n75|47\n97|75\n47|61\n75|61\n47|29\n75|13\n53|13\n\n75,47,61,53,29\n97,61,53,29,13\n75,29,13\n75,97,47,61,53\n61,13,29\n97,13,75,29,47";
    }

    protected override string part_1()
    {
        string[] splits = _input.Split("\n\n");

        // Create prerequisite rules dictionnary
        Dictionary<int, List<int>> prerequisites = new Dictionary<int, List<int>>();
        foreach (string instruction in splits[0].Split('\n'))
        {
            int[] pages = instruction.Split('|').Select(int.Parse).ToArray();
            
            if (!prerequisites.ContainsKey(pages[1]) )
                prerequisites.Add(pages[1], new List<int>());

            prerequisites[pages[1]].Add(pages[0]);
        }

        double result = 0;
        foreach (string rule in splits[1].Split('\n'))
        {
            List<int> pagesToPrint = rule.Split(',').Select(int.Parse).ToList();
            bool isOrdered = true;
            for (int i = 0;  i < pagesToPrint.Count; i++)
            {
                // Foreach prerequisite of the page, check if it's later in the pagesToPrint list. If so, pages are not ordered
                List<int> pre = prerequisites.ContainsKey(pagesToPrint[i]) ? prerequisites[pagesToPrint[i]] : new List<int>();
                foreach(int page in pre) 
                {
                    if (!pagesToPrint.Contains(page))
                        continue;
                    
                    if (!pagesToPrint.GetRange(0, i).Contains(page))
                    {
                        isOrdered = false;
                        break;
                    }    
                }

                if (!isOrdered)
                    break;
            }

            if (isOrdered)
            {
                result += pagesToPrint[pagesToPrint.Count / 2];
                Debug.Log("rule '" + rule + "' is ok, adding " + pagesToPrint[pagesToPrint.Count / 2]);
            }
        }

        return result.ToString();
    }

    protected override string part_2()
    {
        string[] splits = _input.Split("\n\n");

        // Create prerequisite rules dictionnary
        Dictionary<int, List<int>> prerequisites = new Dictionary<int, List<int>>(); 
        foreach (string instruction in splits[0].Split('\n'))
        {
            int[] pages = instruction.Split('|').Select(int.Parse).ToArray();

            if (!prerequisites.ContainsKey(pages[1]))
                prerequisites.Add(pages[1], new List<int>());

            prerequisites[pages[1]].Add(pages[0]);
        }

        double result = 0;
        foreach (string rule in splits[1].Split('\n'))
        {
            List<int> pagesToPrint = rule.Split(',').Select(int.Parse).ToList();
            bool isOrdered = true;
            for (int i = 0; i < pagesToPrint.Count; i++)
            {
                // Foreach prerequisite of the page, check if it's later in the pagesToPrint list. If so, pages are not ordered
                List<int> pre = prerequisites.ContainsKey(pagesToPrint[i]) ? prerequisites[pagesToPrint[i]] : new List<int>();
                foreach (int page in pre)
                {
                    if (!pagesToPrint.Contains(page))
                        continue;

                    if (!pagesToPrint.GetRange(0, i).Contains(page))
                    {
                        isOrdered = false;
                        break;
                    }
                }

                if (!isOrdered)
                    break;
            }

            // If pages list is ordered ignore this update
            if (isOrdered)
                continue;

            List<int> printedPages = new List<int>();
            int safetyCount = 1000;
            while(pagesToPrint.Count > 0 && safetyCount > 0 )
            {
                safetyCount--;

                List<int> temp = new List<int>();
                // For each page to print, check if all prequisite pages are not to print or already in "printedPages" list
                // If so, page is also ready to be printed so is added to printedPages and removed from pagesToPrint
                foreach (int page in pagesToPrint)
                {
                    List<int> pre = prerequisites.ContainsKey(page) ? prerequisites[page] : new List<int>();
                    bool isReady = true;
                    foreach (int prepage in pre)
                    {
                        if (!pagesToPrint.Contains(prepage))
                            continue;

                        if (!printedPages.Contains(prepage))
                        {
                            isReady = false;
                            break;
                        }
                    }

                    if (isReady)
                    {
                        printedPages.Add(page);
                        temp.Add(page);
                    }
                }

                foreach (int toRemove in temp)
                    pagesToPrint.Remove(toRemove);
            }

            if (safetyCount <= 0)
                Debug.LogError("welp");

            result += printedPages[printedPages.Count / 2];
        }


        return result.ToString();
    }
}
