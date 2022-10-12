using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public int seconds;
    public int minutes;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minutes = 0;
        Refresh();
        StartCoroutine("Seconds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Seconds()
    {
        yield return new WaitForSeconds(1);
        seconds = seconds + 1;
        if(seconds == 60)
        {
            minutes = minutes + 1;
            seconds = 0;
        }
        Refresh();
        StartCoroutine("Seconds");
    }

    void Refresh()
    {
        string secondsT = null;
        string minutesT = null;
        if(seconds <= 9)
        {
            secondsT = "0" + seconds.ToString();
        }
        else
        {
            secondsT = seconds.ToString();
        }
        if(minutes <= 9)
        {
            minutesT = "0" + minutes.ToString();
        }
        else
        {
            minutesT = minutes.ToString();
        }
        GetComponent<Text>().text = minutesT + "：" + secondsT;
        DataBase.minutes = minutes;
        DataBase.seconds = seconds;
    }
}
