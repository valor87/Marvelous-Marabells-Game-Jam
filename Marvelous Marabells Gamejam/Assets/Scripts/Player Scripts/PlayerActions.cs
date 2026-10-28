using System;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Tool _toolEquiped;
    [SerializeField] private PickableCrop _cropEquiped;
    [SerializeField] private PlayerInventory _playerInventory;
    private PlayerMovement _playerMovement;

    [SerializeField] private KeyCode _useToolPrimary;
    [SerializeField] private KeyCode _useToolSecondary;
    [SerializeField] private KeyCode _pickPlant;

    [SerializeField] float toolUsedDistance;
    [SerializeField] private Transform _toolHeldPosition;
    [HideInInspector] public bool canUseTool;
    private Eventcore _eventCore;
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerActions);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerActions);
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * toolUsedDistance, Color.purple);

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
        }

        if (Input.GetKeyDown(_useToolSecondary))
        {
            _toolEquiped.useToolSecondary();

        }

    }
    void preformToolAction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
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
            if (hit.collider.GetComponent<PlantableSoil>() != null && _toolEquiped.thisTool.seedTypes != seedTypes.None)
            {
                hit.collider.GetComponent<PlantableSoil>().plantSeed(_toolEquiped.thisTool);
            }

            interaction.Execute(_toolEquiped.thisTool.toolType);
            Debug.Log("Tool being used correctly");

        }
    }

    void preformEmptyHandAction()
    {
        IInteractable interaction = null;
        RaycastHit hit;


        if (Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
        {
            Debug.Log("trying to swap tools");
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

        if (Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
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
                    _playerInventory.playerTools[Array.IndexOf(_playerInventory.playerTools, var)] = grabbedTool.GetComponent<Tool>();
                    _eventCore.UE_ToolInventoryChange.Invoke();
                    hit.collider.gameObject.SetActive(false);
                    break;
                }

            }
        }
    }

    bool isArrayFull<T>(T[] checkingArray)
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

    void activatePlayerActions()
    {
        this.enabled = true;
    }
    void deactivatePlayerActions()
    {
        this.enabled = false;
    }
}
