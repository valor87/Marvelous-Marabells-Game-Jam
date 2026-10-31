using UnityEngine;

public class RestStump : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _energyFillAmount;
    private Eventcore _eventCore;
    private PlayerMovement _playerMovement;
    private float _currentFill;
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _playerMovement = _playerTransform.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerMovement.enabled)
        {
            _eventCore.UE_UsedEnergy.Invoke(_currentFill);

            if (!_playerMovement.enabled)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _playerMovement.enabled = true;
                    _currentFill = 0;
                }
            }
        }
    }

    public void Execute(toolTypes requiredTool)
    {
        if (requiredTool != toolTypes.None)
            return;
        Debug.Log("Sitting on log");
        _currentFill = _energyFillAmount;
        _playerMovement.enabled = false;
        _playerTransform.position = transform.position + new Vector3(0,transform.localScale.y*2,0);
    }
}
