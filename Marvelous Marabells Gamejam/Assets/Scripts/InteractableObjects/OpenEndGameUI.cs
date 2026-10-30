using UnityEngine;

public class OpenEndGameUI : MonoBehaviour
{
    [Header("For the player opening any UI object")]
    [SerializeField] private float _triggerDistence;
    [SerializeField] private KeyCode _openShopKeyCode;
    [SerializeField] private GameObject _activateObject;
    [SerializeField] private GameObject _openPrompt;
    private Transform _playerTransform;
    private Eventcore _eventcore;
    private bool _interactImageOff = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _eventcore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) < _triggerDistence)
        {
            if (Input.GetKeyDown(_openShopKeyCode))
            {
                _activateObject.SetActive(true);
                _eventcore.UE_DeactivatePlayer.Invoke();
            }
            _interactImageOff = false;
            _openPrompt.SetActive(true);
        }
        else if (Vector3.Distance(transform.position, _playerTransform.position) > _triggerDistence && !_interactImageOff)
        {
            _interactImageOff = true;
            _openPrompt.SetActive(false);
        }
    }

}
