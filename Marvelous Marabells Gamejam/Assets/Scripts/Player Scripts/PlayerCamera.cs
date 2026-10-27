using NUnit.Framework;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Properties")]
    [SerializeField] private float _rotationSpeed;
    [Header("The camera that the player uses")]
    [SerializeField] private Transform _cameraTransform;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        _cameraTransform.position = transform.position;
        _cameraTransform.eulerAngles += cameraRotationHandeler();
        transform.eulerAngles += cameraRotationHandeler();
    }

    private Vector3 cameraRotationHandeler()
    {
        return new( - Input.GetAxis("Mouse Y") * _rotationSpeed, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }
}
