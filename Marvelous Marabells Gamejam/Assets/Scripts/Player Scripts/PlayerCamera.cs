using NUnit.Framework;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Properties")]
    [SerializeField] private float _rotationSpeed;
    [Header("The camera that the player uses")]
    [SerializeField] private Transform _cameraTransform;

    private Eventcore _eventCore;
    private void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_ActivatePlayer.AddListener(activatePlayerCamera);
        _eventCore.UE_DeactivatePlayer.AddListener(deactivatePlayerCamera);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        _cameraTransform.position = transform.position;
        _cameraTransform.eulerAngles += cameraRotationHandeler();
        transform.eulerAngles += cameraRotationHandeler();

        if (_rotationSpeed == 0)
        {
            _cameraTransform.eulerAngles = transform.eulerAngles;
        }
    }

    private Vector3 cameraRotationHandeler()
    {
        return new( - Input.GetAxis("Mouse Y") * _rotationSpeed, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }
    public float setRotationSpeed(float newSpeed)
    {
        return _rotationSpeed = newSpeed;
    }
    void activatePlayerCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.enabled = true;
    }
    void deactivatePlayerCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.enabled = false;
    }
}
