using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCount : MonoBehaviour {
    Text text;
    float startTime;
    bool timerIsOn = false;
    float prevPause;
    float pause = 0;

    bool isPause = false;
    void Start() {
        text = GetComponent<Text>();
    }
	
	void Update () {
        if (timerIsOn) {
            float t = Time.time - pause - startTime;
            string min = ((int)t / 60).ToString();
            if (min.Length == 1) {
                min = "0" + min;
            }
            string sec = (t % 60).ToString("f0");
            if (sec.Length == 1) {
                sec = "0" + sec;
            }
            text.text = min + ":" + sec;
        }
	}

    public string getTime() {
        return text.text;
    }

    public void enabledTimer(bool value) {
        if(value == true) {
            startTime = Time.time;
        }
        timerIsOn = value;
    }

    public void pauseTimer() {
        prevPause = pause;
        timerIsOn = false;
        pause = Time.time;
    }

    public void startTimer() {
        pause = Time.time - pause + prevPause;
        timerIsOn = true;
    }
    
}
