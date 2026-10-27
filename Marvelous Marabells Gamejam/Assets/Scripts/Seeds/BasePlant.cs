using UnityEngine;

public class BasePlant : MonoBehaviour, IInteractable
{
    public float growSpeed;
    public toolTypes toolRequired { get; set; }

    [SerializeField] private float _growTime;
    [SerializeField] private float _currentGrowTime;
    [SerializeField] private bool _isFertalized;
    public Plants thisPlantObject;
    [HideInInspector] public PlantableSoil soilPlantedOn;
    private bool _fullyGrown;
    private Eventcore _eventCore;
    void Start()
    {
        transform.localScale = Vector3.zero;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }

    void Update()
    {
        if (_growTime >= _currentGrowTime) {
            transform.localScale = (Vector3.one / 2) * (_currentGrowTime / _growTime);
            _currentGrowTime += growSpeed * Time.deltaTime;
        }

        if (_growTime <= _currentGrowTime)
        {
            _fullyGrown = true;
        }
    }

    public void Execute(toolTypes arg)
    {
        if (!_fullyGrown)
            return;
        Debug.Log($"Destroying {gameObject.name} {thisPlantObject.name}");
        _eventCore.UE_PickPlant.Invoke(thisPlantObject);
        Destroy(gameObject);
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
