using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static string time;

    public Text timerText;
    private float startTime;
    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer Text").GetComponent<Text>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            time = minutes + ":" + seconds;

            timerText.text = time;
        }
    }

    public void Finnish()
    {
        timerText.color = Color.yellow;
        finished = true;
    }
}
