using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class SellBox : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    private Eventcore _eventCore;
    [HideInInspector] public PickableCrop _sellingCrop; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }
    public void Execute(toolTypes arg)
    {
        if(_sellingCrop == null)
            return;
        _eventCore.UE_SoldItem.Invoke(_sellingCrop.thisPlantObejct.sellAmount);
        _sellingCrop = null;
    }
}
