using UnityEngine;
using UnityEngine.Events;

public class Eventcore : MonoBehaviour
{
    // for when the player sells an item the int is the amount it went for
    public UnityEvent<int> UE_SoldItem;
    // for when the player buys an item the int is the amount it went for
    public UnityEvent<int> UE_PurchasedItem;
    //for when the player changes out their item
    public UnityEvent<Tool> UE_ToolChange;
    //for when the player plants something
    public UnityEvent<ToolsObject, PlantableSoil> UE_PlantItem;
}
