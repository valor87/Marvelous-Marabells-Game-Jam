using UnityEngine;

public class PlayerActions : MonoBehaviour
{
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
        playerInput();
        _toolEquiped.fallowPlayer(_toolHeldPosition);
    }

    private void playerInput()
    {
        if (Input.GetKeyDown(_useToolPrimary))
        {
            _toolEquiped.useToolPrimary();
        }

        if (Input.GetKeyDown(_useToolSecondary))
        {
            _toolEquiped.useToolSecondary();
        }
    }
}
