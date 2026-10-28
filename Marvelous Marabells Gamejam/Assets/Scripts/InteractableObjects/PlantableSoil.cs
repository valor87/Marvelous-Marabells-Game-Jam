using System.Collections;
using UnityEngine;

public class PlantableSoil : MonoBehaviour, IInteractable
{
    [SerializeField] Material grassMaterial;
    [SerializeField] Material tilledMaterial;
    [SerializeField] Material wateredMaterial;

    [Header("For debugging")]
    [SerializeField] private BreakableItems _blockingItem;

    [HideInInspector] public seedTypes seedPlanted;
    [HideInInspector] public BasePlant growingPlant;
    public toolTypes toolRequired { get; set; }

    private ParticleSystem _particalSystem;
    private Eventcore _eventCore;
    private bool _isTilled;
    private bool _isWatered;
    private bool _isPlanted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<MeshRenderer>().material = grassMaterial;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _particalSystem = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (_isPlanted && _isWatered)
        {
            growingPlant.growSpeed = 1;
        }
        checkForBlockingItem();
    }

    void checkForBlockingItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 1))
        {
            if (hit.collider.GetComponent<BreakableItems>() != null)
            {
                _blockingItem = hit.collider.GetComponent<BreakableItems>();
            }
        }
        else
        {
            _blockingItem = null;
        }
    }

    public void Execute(toolTypes usedTool)
    {
        if (_blockingItem != null)
            return;
        switch (usedTool)
        {
            case toolTypes.Hoe:
                GetComponent<MeshRenderer>().material = tilledMaterial;
                _particalSystem.Play();
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
                _isPlanted = true;
                break;
        }

    }

    public void plantSeed(seedTypes seedTool)
    {
        seedPlanted = seedTool;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
