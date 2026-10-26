using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy;

    private Eventcore _eventCore;

    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();    
    }
}
