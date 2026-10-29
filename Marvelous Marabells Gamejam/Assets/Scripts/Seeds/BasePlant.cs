using UnityEngine;

public class BasePlant : MonoBehaviour, IInteractable
{
    public float growSpeed;
    public toolTypes toolRequired { get; set; }
    public toolTypes actionRequired = toolTypes.None;
    public Plants plants;
    [SerializeField] private float _growTime;
    [SerializeField] private float _currentGrowTime;
    [SerializeField] private bool _isFertalized;
    [SerializeField] private GameObject _cropToGrow;
    [HideInInspector] public PlantableSoil soilPlantedOn;
    private bool _fullyGrown;
    private Eventcore _eventCore;
    void Start()
    {
        transform.localScale = Vector3.zero;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();

        _growTime = plants.growDuration;

    }

    void Update()
    {
        if (_growTime > _currentGrowTime)
        {
            transform.localScale = (Vector3.one / 2) * (_currentGrowTime / _growTime);
            _currentGrowTime += growSpeed * Time.deltaTime;
        }

        if (_growTime < _currentGrowTime)
        {
            _fullyGrown = true;
        }

        if (_fullyGrown)
        {
            growCrop();
            _fullyGrown = false;
            _growTime = _currentGrowTime;
        }
    }

    void growCrop()
    {
        GameObject _tempCrop = Instantiate(_cropToGrow, transform);
        _tempCrop.GetComponent<PickableCrop>().basePlant = this;
        _tempCrop.GetComponent<PickableCrop>().thisPlantObejct = plants;
        _tempCrop.transform.position = transform.position + new Vector3(transform.localScale.x, transform.localScale.y, 0);
    }

    public void Execute(toolTypes arg)
    {
        //if (!_fullyGrown)
        //    return;

        //Destroy(gameObject);
    }
    // i get it these functions are pee and poo
    public void destroyThisCrop()
    {
        soilPlantedOn.clearSoil();
        Destroy(gameObject);
    }
    // i dont feel like making it better 
    public void destroyOnlyCrop()
    {
        Destroy(gameObject);
    }
    public void reGrowPlant()
    {
        growCrop();
        _eventCore.UE_CropInventoryChange.Invoke();
    }
    public bool checkIfGrown()
    {
        return _fullyGrown;
    }
    public void deactivateCrop()
    {
        gameObject.SetActive(false);
    }
    public void activateCrop()
    {
        gameObject.SetActive(true);
    }
    public void setCurrentGrowTime(float arg)
    {
        _currentGrowTime = arg;
    }
    public float getGrowTime()
    {
        return _growTime;
    }
    public void fallowPlayer(Transform _playerTransform)
    {
        transform.position = _playerTransform.position;
        transform.eulerAngles = _playerTransform.eulerAngles;
    }
}
