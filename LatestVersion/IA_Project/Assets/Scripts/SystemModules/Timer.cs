using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer> {

    [SerializeField] Text timerText;

    float time;
    float sec;
    float msec;

    public void StartTimer(float t) {
        time = t;
        StartCoroutine(TimeCount(t));
    }

    public void StopTimer() {
    }

    IEnumerator TimeCount(float t) {
        while (time != 0) {

            time -= Time.deltaTime;
            time = Mathf.Clamp(time, 0, t);

            sec = (int)(time % 60);
            msec = (int)((time - (int)time) * 100);

            timerText.text = string.Format("{0:00}:{1:00}", sec, msec);

            yield return null;
        }
    }
}