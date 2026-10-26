using System.Collections;
using UnityEngine;

public class PlantableSoil : MonoBehaviour, IInteractable
{
    [SerializeField] Material grassMaterial;
    [SerializeField] Material tilledMaterial;
    [SerializeField] Material wateredMaterial;
    public toolTypes toolRequired { get; set; }

    //private 
    private Eventcore _eventCore;
    public ToolsObject seedPlanted;
    private bool _isTilled;
    private bool _isWatered;
    private bool _isPlanted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().material = grassMaterial;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }

    public void Execute(toolTypes usedTool)
    {
        switch (usedTool)
        {
            case toolTypes.Hoe:
                GetComponent<MeshRenderer>().material = tilledMaterial;
                Debug.Log("The soid has been tilled");
                _isTilled = true;
                break;
            case toolTypes.WateringCan:
                if (!_isTilled)
                {
                    Debug.Log("Till the land first");
                    return;
                }
                Debug.Log("The land has been watered");
                GetComponent<MeshRenderer>().material = wateredMaterial;
                _isWatered = true;
                break;
            case toolTypes.Seeds:
                if (!_isTilled)
                    return;
                _eventCore.UE_PlantItem.Invoke(seedPlanted, transform);
                break;
        }

    }

    public void plantSeed(ToolsObject seedTool)
    {
        seedPlanted = seedTool;
    }
}
