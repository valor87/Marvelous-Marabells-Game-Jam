using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

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
    [SerializeField] private Vector3 _finalGrowSize = Vector3.one;
    [HideInInspector] public PlantableSoil soilPlantedOn;

    public bool oneTimeGrow = false;
    [SerializeField] private PickableCrop _PickableCrop;
    private bool _fullyGrown = false;
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
            transform.localScale = (_finalGrowSize) * (_currentGrowTime / _growTime);
            _currentGrowTime += growSpeed * Time.deltaTime;
        }

        if (_growTime < _currentGrowTime)
        {
            _fullyGrown = true;
        }

        if (_fullyGrown && !oneTimeGrow)
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
        _tempCrop.transform.position = transform.position;
    }

    public void Execute(toolTypes args)
    {
        if (args != toolTypes.None || !_fullyGrown)
            return;
        Debug.Log($"picked {plants.name}");
        GameObject copyCrop = Instantiate(_cropToGrow, transform);
        copyCrop.SetActive(false);
        copyCrop.GetComponent<PickableCrop>().basePlant = this;
        copyCrop.GetComponent<PickableCrop>().thisPlantObejct = plants;
        _eventCore.UE_PickPlant.Invoke(copyCrop.GetComponent<PickableCrop>());

        destroyThisCrop();
        Destroy(copyCrop);
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
