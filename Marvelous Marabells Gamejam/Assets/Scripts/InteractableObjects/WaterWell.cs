using UnityEngine;

public class WaterWell : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired {get; set;}

    private Eventcore _evenCore;
    void Start()
    {
        _evenCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }

    public void Execute(toolTypes arg)
    {
        if (arg == toolTypes.WateringCan)
        {
            _evenCore.UE_FillWateringCan.Invoke();
        }
    }
}
