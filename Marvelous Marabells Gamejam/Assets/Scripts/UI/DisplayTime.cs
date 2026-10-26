using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    private Eventcore _eventCore;
    private int dayNumber = 1;
    private int minutes;
    private int hours;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_TurnOfTheMinute.AddListener(setMinutes);
        _eventCore.UE_TurnOfTheHour.AddListener(setHours);
    }
    
    // Update is called once per frame
    void Update()
    {
        time.text = $"{hours}:{minutes}";
    }

    void setMinutes(int newMinute)
    {
        minutes = newMinute;
    }
    void setHours(int newHours)
    {
        hours = newHours;
    }
}
