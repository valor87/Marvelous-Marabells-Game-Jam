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
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerEnergy);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerEnergy);
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
        currentEnergy = Mathf.Clamp(currentEnergy, 0, 100);
        if (outOfEnergyCheck())
            Debug.Log("the player is out of energy");
    }

    private bool outOfEnergyCheck()
    {
        if(currentEnergy <= 0) 
            return true;
        return false;
    }

    void deactivatePlayerEnergy()
    {
        this.enabled = false;
    }

    void activatePlayerEnergy()
    {
        this.enabled = true;
    }
}
