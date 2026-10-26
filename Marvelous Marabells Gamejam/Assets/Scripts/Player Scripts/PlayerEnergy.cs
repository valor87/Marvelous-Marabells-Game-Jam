using System;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float currentEnergy;
    public float maxEnergy;
    private PlayerActions _playerActions;
    private Eventcore _eventCore;

    void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_UsedEnergy.AddListener(changeEnergy);
    }
    private void Update()
    {
        if (outOfEnergyCheck())
        {
            _playerActions.enabled = false;
        }
        else
        {
            _playerActions.enabled = true;
        }
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
