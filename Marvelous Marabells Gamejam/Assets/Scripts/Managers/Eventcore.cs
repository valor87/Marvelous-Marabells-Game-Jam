using UnityEngine;
using UnityEngine.Events;

public class Eventcore : MonoBehaviour
{
    // for when the player sells an item the int is the amount it went for
    public UnityEvent<int> UE_SoldItem;
    // for when the player buys an item the int is the amount it went for
    public UnityEvent<int> UE_PurchasedItem;
    //for when the player changes out their item/tool
    public UnityEvent<int> UE_ToolChange;
    //for when the player changes out their item/crop
    public UnityEvent<int> UE_CropChange;
    //for when the player plants something
    public UnityEvent<ToolsObject, Transform> UE_PlantItem;
    // for when the player harvests plants
    public UnityEvent<Plants> UE_PickPlant;

    public UnityEvent<int> UE_TurnOfTheMinute;
    public UnityEvent<int> UE_TurnOfTheHour;
}
