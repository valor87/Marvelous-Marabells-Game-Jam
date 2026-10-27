using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Tool _toolEquiped;
    [SerializeField] private BasePlant _cropEquiped;


    [SerializeField] private KeyCode _useToolPrimary;
    [SerializeField] private KeyCode _useToolSecondary;
    [SerializeField] private KeyCode _pickPlant;

    [SerializeField] float toolUsedDistance;
    [SerializeField] private Transform _toolHeldPosition;

    private Eventcore _eventCore;
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerActions);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerActions);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * toolUsedDistance, Color.purple);
        if (_toolEquiped == null)
            return;
        playerInput();
        followPlayer();
    }
    void followPlayer()
    {
        if(_toolEquiped != null)
            _toolEquiped.fallowPlayer(_toolHeldPosition);
        if(_cropEquiped != null)
            _cropEquiped.fallowPlayer(_toolHeldPosition);
    }
    private void playerInput()
    {
        if (Input.GetKeyDown(_pickPlant))
        {
            pickPlant();
        }
        if (_toolEquiped == null)
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

    void pickPlant()
    {
        IInteractable interaction = null;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
        {
            if (hit.collider.GetComponent<BasePlant>() != null)
            {
                Debug.Log($"Crops is {hit.collider.name}");
                interaction = hit.collider.GetComponent<IInteractable>();
                interaction.Execute(toolTypes.None);
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

    public void changeCrop(BasePlant newCrop)
    {
        _cropEquiped = newCrop;
    }

    public Tool currentlyEquipTool()
    {
        return _toolEquiped;
    }
    public BasePlant currentlyEquipCrop()
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
