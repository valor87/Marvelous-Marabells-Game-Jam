using UnityEngine;

public class PickableCrop : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    public Plants thisPlantObejct; 
    [HideInInspector] public BasePlant basePlant;
    private Eventcore _eventCore;
    
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _growDuration;
    [SerializeField] private float _currentGrowTime;
    [SerializeField] private int _cropSellPrice;
    [SerializeField] private bool _reGrow;
    public GameObject cropPrefab;

    private Vector3 startScale = Vector3.zero;
    private Vector3 endScale = Vector3.one;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _growSpeed = thisPlantObejct.growSpeed;
        _growDuration = thisPlantObejct.growDuration;
        _cropSellPrice = thisPlantObejct.sellAmount;
        _reGrow = thisPlantObejct.reGrow;

        if (!_reGrow)
        {
            _currentGrowTime = _growDuration;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentGrowTime < _growDuration) {
            _currentGrowTime += _growSpeed * Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, _currentGrowTime / _growDuration);
        }
    }

    public void Execute(toolTypes args)
    {
        if (args != toolTypes.None || _currentGrowTime < _growDuration)
            return;
        _eventCore.UE_PickPlant.Invoke(this);
        if (_reGrow)
        {
            basePlant.reGrowPlant();
        }
        else
        {
            basePlant.destroyThisCrop(); ;
        }
        
            Destroy(gameObject);
    }

    public void fallowPlayer(Transform _playerTransform)
    {
        transform.position = _playerTransform.position;
        transform.eulerAngles = _playerTransform.eulerAngles;
    }

    public void deactivateCrop()
    {
        gameObject.SetActive(false);
    }

    public void activateCrop()
    {
        gameObject.SetActive(true);
    }
}
