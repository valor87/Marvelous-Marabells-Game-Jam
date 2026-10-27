using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("For player moving around the map")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _gravitySpeed;

    [SerializeField] private Transform _orientation;
    private Rigidbody _playerRB;
    private PlayerCamera _playerCamera;
    private Eventcore _eventCore;
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerMovement);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerMovement);

        _playerRB = GetComponent<Rigidbody>();    
        _playerCamera = GetComponent<PlayerCamera>();
    }

    private void FixedUpdate()
    {
        _playerRB.AddForce(gravityHandeler(), ForceMode.Acceleration);
        _playerRB.linearVelocity = movementHandeler();
      
    }

    private Vector3 gravityHandeler()
    {
        return Physics.gravity * _gravitySpeed;
    }
    private Vector3 movementHandeler()
    {
        return (_orientation.right * Input.GetAxis("Horizontal") + _orientation.forward * Input.GetAxis("Vertical")) * _movementSpeed;
    }
    void activatePlayerMovement()
    {
        this.enabled = true;
    }
    void deactivatePlayerMovement()
    {
        this.enabled = false;
    }
}
