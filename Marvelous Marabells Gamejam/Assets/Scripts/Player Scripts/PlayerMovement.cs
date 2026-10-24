using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("For player moving around the map")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _gravitySpeed;

    private Rigidbody _playerRB;
    private PlayerCamera _playerCamera;
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();    
        _playerCamera = GetComponent<PlayerCamera>();
    }

    private void FixedUpdate()
    {
        _playerRB.AddForce(gravityHandeler(), ForceMode.Acceleration);
        _playerRB.linearVelocity = movementHandeler();
        Debug.Log(transform.eulerAngles);
        if (transform.eulerAngles.x >= 270 && transform.eulerAngles.x <= 340)
        {
            _playerRB.linearVelocity = Vector3.zero;
        }
    }

    private Vector3 gravityHandeler()
    {
        return Physics.gravity * _gravitySpeed;
    }
    private Vector3 movementHandeler()
    {
        return (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * _movementSpeed;
    }
}
