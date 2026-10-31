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
    private Eventcore _eventcore;

    private bool _interactImageOff = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _eventcore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }

    private void Update()
    {
        
        if (Vector3.Distance(transform.position, _playerTransform.position) < _triggerDistence)
        {
            if (Input.GetKeyDown(_openShopKeyCode))
            {
                _shopObject.SetActive(true);
                _eventcore.UE_DeactivatePlayer.Invoke();
            }
            _openShopPrompt.SetActive(true);
            _interactImageOff = false;
        }
        else if(Vector3.Distance(transform.position, _playerTransform.position) > _triggerDistence && !_interactImageOff)
        {
            _interactImageOff = true;
            _openShopPrompt.SetActive(false);
        }
    }
    public void activatePlayer()
    {
        _eventcore.UE_ActivatePlayer.Invoke();
    }
}
