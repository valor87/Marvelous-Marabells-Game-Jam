using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timePassing = 1;
    public int startingHour;
    public int startingMinute;
    private int currentHour;
    private int currentMinute;
    public float currentSecond;

    private Eventcore _eventCore;
    private timePeriod _timePeriod;

    private void Start()
    {
        currentMinute = startingMinute;
        currentHour = startingHour;
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

        

        if (currentHour < 11)
        {
            _timePeriod = timePeriod.AM;
        }
        if (currentHour > 11)
        {
            _timePeriod = timePeriod.PM;
        }
    }
}
