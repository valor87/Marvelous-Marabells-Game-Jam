using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerMoney _playerMoney = new PlayerMoney();
    public int _currentMoney;

    [Header("For the player opening the shop")]
    [SerializeField] private float _triggerDistence;
    [SerializeField] private KeyCode _openShopKeyCode;
    [SerializeField] private GameObject _shopObject;
    [SerializeField] private GameObject _openShopPrompt;
    private Transform _playerTransform;
    
    [Header("Stop player from moving")]
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerActions _playerActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerMoney.currentMoney = _currentMoney;
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) < _triggerDistence)
        {
            if (Input.GetKeyDown(_openShopKeyCode))
            {
                _shopObject.SetActive(true);
                deactivatePLayer();
            }
            _openShopPrompt.SetActive(true);
        }
        else
        {
            _openShopPrompt.SetActive(false);
        }
    }

    void deactivatePLayer()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerActions.enabled = false;
        _playerCamera.enabled = false;
        _playerMovement.enabled = false;
    }

    public void activatePlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerActions.enabled = true;
        _playerCamera.enabled = true;
        _playerMovement.enabled = true;
    }
}
