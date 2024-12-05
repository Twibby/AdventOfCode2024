using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayScript2024 : MonoBehaviour
{
    public bool IsDebug = false;
    public bool IsTestInput = true;
    public bool Part1 = true;
    public bool Part2 = true;

    public bool AnimPart1 = false;
    public bool AnimPart2 = false;
    public DayAnimationScript animationScript;

    protected string _input;
    protected int _day
    {
        get { return int.Parse(this.GetType().ToString().Substring(this.GetType().ToString().Length-2)); }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (animationScript != null)
        {
            animationScript.gameObject.SetActive(false);
        }
        StartCoroutine(coDay());
    }

    protected IEnumerator coDay()
    {
        Debug.LogWarning("Day is : " + _day);

        if (!IsTestInput)
        {
            yield return StartCoroutine(Tools2024.Instance.GetInput(_day));
            _input = Tools2024.Instance.Input;
        }
        else
        {
            yield return StartCoroutine(Tools2024.Instance.GetTestInput("https://ollivier.iiens.net/AoC/2024/" + _day.ToString("00") + ".txt"));
            _input = Tools2024.Instance.Input;

            _input = test_input();
        }

        float t0;
        string log = "", result = "";

        if (Part1)
        {
            t0 = Time.realtimeSinceStartup;
            log = "Started at " + t0;

            result = part_1();

            log += " | Ended at " + Time.realtimeSinceStartup;
            log += " | Part 1 duration is : " + (Time.realtimeSinceStartup - t0).ToString();
            if (IsDebug || (Time.realtimeSinceStartup - t0) > 5f)
                Debug.Log(log);

            Debug.LogWarning("[Day " + _day.ToString() + "] Part 1 result is : " + result);
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (Part2)
        {
            t0 = Time.realtimeSinceStartup;
            log = "Started at " + t0;

            result = part_2();

            log += " | Ended at " + Time.realtimeSinceStartup;
            log += " | Part 2 duration is : " + (Time.realtimeSinceStartup - t0).ToString();
            if (IsDebug)
                Debug.Log(log);

            Debug.LogWarning("[Day " + _day.ToString() + "] Part 2 result is : " + result);
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (animationScript != null)
        {
            animationScript.input = _input;
            if (AnimPart1)
            {
                animationScript.gameObject.SetActive(true);
               
                Debug.Log("[Day " + _day.ToString() + "] Starting Anim Part 1");

                yield return animationScript.part_1();

                yield return new WaitForSeconds(1f);
            }

            if (AnimPart2)
            {
                animationScript.gameObject.SetActive(true);

                Debug.Log("[Day " + _day.ToString() + "] Starting Anim Part 2");

                yield return animationScript.part_2();
            }
        }

        //yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(5f);
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    protected virtual string test_input() { Debug.Log("No manual override of input test"); return _input; }
    protected virtual string part_1() { Debug.LogError("[" + this.GetType().ToString() + "] part_1 is not defined"); return "N/A"; }
    protected virtual string part_2() { Debug.LogError("[" + this.GetType().ToString() + "] part_2 is not defined"); return "N/A"; }
}
