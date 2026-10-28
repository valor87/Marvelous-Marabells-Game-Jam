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
    //for whent the player adds or removes a tool form inventory
    public UnityEvent UE_ToolInventoryChange;
    //for whent the player adds or removes a crop form inventory
    public UnityEvent UE_CropInventoryChange;
    //for when the player changes out their item/crop
    public UnityEvent<int> UE_CropChange;
    // for when the player plants something
    public UnityEvent<ToolsObject, Transform> UE_PlantItem;
    // for when the player harvests plants
    public UnityEvent<PickableCrop> UE_PickPlant;
    // for when the player makes an action that takes energy
    public UnityEvent<float> UE_UsedEnergy;
    // stoping the players control over the character
    public UnityEvent UE_DeactivatePlayer;
    // returning the players control
    public UnityEvent UE_ActivatePlayer;

    //for in game time
    public UnityEvent<int> UE_TurnOfTheMinute;
    public UnityEvent<int> UE_TurnOfTheHour;
}
