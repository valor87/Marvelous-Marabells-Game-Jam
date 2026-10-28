using UnityEngine;

public class RestStump : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _energyFillAmount;
    private Eventcore _eventCore;
    private PlayerMovement _playerMovement;
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
            _eventCore.UE_UsedEnergy.Invoke(_energyFillAmount);

            if (!_playerMovement.enabled)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _playerMovement.enabled = true;
                }
            }
        }
    }

    public void Execute(toolTypes requiredTool)
    {
        if (requiredTool != toolTypes.None)
            return;
        Debug.Log("Sitting on log");
        _playerMovement.enabled = false;
        _playerTransform.position = transform.position + new Vector3(0,transform.localScale.y*2,0);
    }
}
