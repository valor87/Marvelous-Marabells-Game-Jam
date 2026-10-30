using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Tool _toolEquiped;
    [SerializeField] private PickableCrop _cropEquiped;
    [SerializeField] private PlayerInventory _playerInventory;

    [SerializeField] private KeyCode _useToolPrimary;
    [SerializeField] private KeyCode _useToolSecondary;
    [SerializeField] private KeyCode _pickPlant;

    [SerializeField] float toolUsedDistance;
    [SerializeField] private Transform _toolHeldPosition;

    [HideInInspector] public bool canUseTool;
    [HideInInspector] public float maxWater = 100;
    [HideInInspector] public float currentWater = 100;


    private Eventcore _eventCore;
    private PlayerMovement _playerMovement;
    private PlayerCamera _playerCamera;

    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _playerCamera = GetComponent<PlayerCamera>();
        _playerMovement = GetComponent<PlayerMovement>();
        
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerActions);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerActions);
        _eventCore.UE_FillWateringCan.AddListener(fillWateringCan);
    }

    void Update()
    {
        Debug.DrawRay(transform.position + _playerCamera._cameraPosOffest, transform.forward * toolUsedDistance, Color.purple);

        playerInput();
        followPlayer();
    }

    void followPlayer()
    {
        if (_toolEquiped != null)
            _toolEquiped.fallowPlayer(_toolHeldPosition);
        if (_cropEquiped != null)
            _cropEquiped.fallowPlayer(_toolHeldPosition);
    }

    private void playerInput()
    {
        if (Input.GetKeyDown(_pickPlant))
        {
            takeDownTools();
            hangUpTool();
            preformEmptyHandAction();
        }
        if (_toolEquiped == null || !canUseTool)
            return;

        /////// if the player has a tool in hand//////
        if (Input.GetKeyDown(_useToolPrimary))
        {
            preformToolAction();
            _toolEquiped.useToolPrimary();
            useEnergy(-_toolEquiped.thisTool.energyNeeded);

            if(_toolEquiped.thisTool.toolType == toolTypes.WateringCan)
            {
                currentWater -= 5;
            }
        }

        if (Input.GetKeyDown(_useToolSecondary))
        {
            _toolEquiped.useToolSecondary();

        }

    }

    void preformToolAction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + _playerCamera._cameraPosOffest, transform.forward, out hit, toolUsedDistance))
        {
            IInteractable interaction = null;

            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                interaction = hit.collider.GetComponent<IInteractable>();
            }
            else
            {
                return;
            }
            if (hit.collider.GetComponent<PlantableSoil>() != null && _toolEquiped.thisTool.seedTypes != seedTypes.None
                && hit.collider.GetComponent<PlantableSoil>()._isTilled)
            {
                hit.collider.GetComponent<PlantableSoil>().plantSeed(_toolEquiped.thisSeedType);
                if (_toolEquiped.thisTool.consumable)
                {
                    _toolEquiped._thisDurability--;
                    if (_toolEquiped._thisDurability == 0)
                    {
                        _playerInventory.playerTools[Array.IndexOf(_playerInventory.playerTools, _toolEquiped)] = null;
                        _eventCore.UE_ToolInventoryChange.Invoke();
                        Destroy(_toolEquiped.gameObject);
                    }
                }
            }

            interaction.Execute(_toolEquiped.thisTool.toolType);

        }
    }

    void preformEmptyHandAction()
    {
        IInteractable interaction = null;
        RaycastHit hit;


        if (Physics.Raycast(transform.position + _playerCamera._cameraPosOffest, transform.forward, out hit, toolUsedDistance))
        {
            if (hit.collider.GetComponent<IInteractable>() == null)
                return;

            interaction = hit.collider.GetComponent<IInteractable>();

            if (isArrayFull(_playerInventory.playerCrops) && hit.collider.GetComponent<PickableCrop>() != null)
                return;

            if (hit.collider.GetComponent<SellBox>() != null)
            {
                hit.collider.GetComponent<SellBox>()._sellingCrop = _cropEquiped;
                interaction.Execute(toolTypes.None);
                if (_cropEquiped != null)
                {
                    Destroy(_cropEquiped.gameObject);
                    _playerInventory.playerCrops[Array.IndexOf(_playerInventory.playerCrops, _cropEquiped)] = null;
                    _eventCore.UE_CropInventoryChange.Invoke();
                }
            }
            interaction.Execute(toolTypes.None);

        }
    }

    void hangUpTool()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + _playerCamera._cameraPosOffest, transform.forward, out hit, toolUsedDistance))
        {
            if (_toolEquiped == null)
            {
                return;
            }

            if (hit.collider.GetComponent<ToolWallManager>() != null)
            {
                ToolWallManager _toolWall = hit.collider.GetComponent<ToolWallManager>();
                _toolWall.setToolActive(_toolEquiped.thisTool);
                GameObject toolToDestroy = _toolEquiped.gameObject;
                _playerInventory.playerTools[Array.IndexOf(_playerInventory.playerTools, _toolEquiped)] = null;
                Destroy(_toolEquiped.gameObject);
                _eventCore.UE_ToolInventoryChange.Invoke();
            }
        }
    }
    void takeDownTools()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + _playerCamera._cameraPosOffest, transform.forward, out hit, toolUsedDistance))
        {
            if (hit.collider.GetComponent<ToolBoardTool>() != null)
            {
                foreach (Tool var in _playerInventory.playerTools)
                {
                    if (var != null)
                    {
                        continue;
                    }

                    GameObject grabbedTool = Instantiate(hit.collider.GetComponent<ToolBoardTool>().thisToolPrefab, gameObject.transform);
                    grabbedTool.SetActive(false);
                    _playerInventory.playerTools[Array.IndexOf(_playerInventory.playerTools, var)] = grabbedTool.GetComponent<Tool>();
                    _eventCore.UE_ToolInventoryChange.Invoke();
                    hit.collider.gameObject.SetActive(false);
                    break;
                }

            }
        }
    }

    public void useEnergy(float amountOfEnergy)
    {
        _eventCore.UE_UsedEnergy.Invoke(amountOfEnergy);
    }

    public void changeTool(Tool newTool)
    {
        _toolEquiped = newTool;
    }

    public void changeCrop(PickableCrop newCrop)
    {
        _cropEquiped = newCrop;
    }

    public Tool currentlyEquipTool()
    {
        return _toolEquiped;
    }
    public PickableCrop currentlyEquipCrop()
    {
        return _cropEquiped;
    }
    private bool isArrayFull<T>(T[] checkingArray)
    {
        for (int i = 0; i < checkingArray.Length; i++)
        {
            var item = checkingArray[i];
            if (item == null)
            {
                return false;
            }
        }
        return true;
    }

    private void activatePlayerActions()
    {
        this.enabled = true;
    }
    private void deactivatePlayerActions()
    {
        this.enabled = false;
    }

    private void fillWateringCan()
    {
        currentWater = maxWater;
    }
}
