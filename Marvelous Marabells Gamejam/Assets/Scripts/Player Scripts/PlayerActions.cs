using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] float toolUsedDistance;

    [SerializeField] private KeyCode _useToolPrimary;
    [SerializeField] private KeyCode _useToolSecondary;

    [SerializeField] private Tool _toolEquiped;
    [SerializeField] private Transform _toolHeldPosition;
    void Start()
    {
        if(_toolEquiped == null)
            this.enabled = false;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * toolUsedDistance, Color.purple);
        playerInput();
        _toolEquiped.fallowPlayer(_toolHeldPosition);
    }

    private void playerInput()
    {
        if (Input.GetKeyDown(_useToolPrimary))
        {
            preformToolAction();
            _toolEquiped.useToolPrimary();
        }

        if (Input.GetKeyDown(_useToolSecondary))
        {
            _toolEquiped.useToolSecondary();
        }
    }

    void preformToolAction()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, toolUsedDistance))
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

            if (interaction.toolRequired == _toolEquiped.thisTool.toolType)
            {
                interaction.Execute(_toolEquiped.thisTool.toolType);
                Debug.Log("Tool being used correctly");
            }
        }
    }
}
