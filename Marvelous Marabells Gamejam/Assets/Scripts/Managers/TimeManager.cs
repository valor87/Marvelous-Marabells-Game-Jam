using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float timePassing = 1;
    public int startingHour;
    public int startingMinute;
    public int currentHour;
    private int currentMinute;
    public float currentSecond;
    public float slowDownTime = 100;

    public int endGameHour = 12;
    public timePeriod endGameTimePeriod = timePeriod.AM;
    private Eventcore _eventCore;
    public timePeriod _timePeriod;
    public StartBoxFade _boxFade;
    public playerProgress _playerProgress;
    float t = 0;
    private void Start()
    {
        currentMinute = startingMinute;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        StartCoroutine(MyLateStart());

    }
    IEnumerator MyLateStart()
    {
        yield return new WaitForSeconds(.25f);
        _eventCore.UE_TurnOfTheMinute.Invoke(currentMinute);
        _eventCore.UE_TurnOfTheHour.Invoke(currentHour);
    }
    // Update is called once per frame
    void Update()
    {
        passTime();

        if (currentHour == endGameHour && _timePeriod == endGameTimePeriod)
        {
            _boxFade.startFade();
            t += Time.deltaTime;
            if (t > _boxFade.totalWaitTime)
            {
                loadNextScene();
            }
        }
    }

    void passTime()
    {
        currentSecond += timePassing * Time.deltaTime;

        if (currentSecond >= 59)
        {
            currentMinute++;
            currentSecond = 0;

            _eventCore.UE_TurnOfTheMinute.Invoke(currentMinute);
        }

        if (currentMinute == 59)
        {
            currentHour++;
            if (currentHour > 12)
            {
                currentHour = 1;
            }

            currentMinute = 0;

            _eventCore.UE_TurnOfTheHour.Invoke(currentHour);
        }



        if (currentHour > 11)
        {
            _timePeriod = timePeriod.PM;
        }
    }
    public void slowTime()
    {
        timePassing = slowDownTime;
    }
    bool save = false;
    void loadNextScene()
    {
        if (!save)
        {
            _eventCore.UE_SavePlayerData.Invoke();
            _playerProgress.startDayLate = true;
            save = true;
        }
        SceneManager.LoadScene(1);
    }
}
