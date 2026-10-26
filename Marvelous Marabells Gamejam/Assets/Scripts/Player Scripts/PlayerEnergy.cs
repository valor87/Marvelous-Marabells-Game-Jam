using System;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float currentEnergy;
    public float maxEnergy;

    private Eventcore _eventCore;

    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_UsedEnergy.AddListener(changeEnergy);
    }

    private void changeEnergy(float amountOfEnergy)
    {
        currentEnergy += amountOfEnergy;
        if (outOfEnergyCheck())
            Debug.Log("the player is out of energy");
    }

    private bool outOfEnergyCheck()
    {
        if(currentEnergy <= 0) 
            return true;
        return false;
    }
}
