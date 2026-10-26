using UnityEngine;

public class BasePlant : MonoBehaviour
{
    public float growSpeed;
    [SerializeField] private float _growTime;
    [SerializeField] private float _currentGrowTime;
    [SerializeField] private bool _isFertalized;
    
    [HideInInspector]public PlantableSoil soilPlantedOn;
    void Start()
    {
       transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (_growTime >= _currentGrowTime) {
            transform.localScale = (Vector3.one/2) * (_currentGrowTime/_growTime);
            _currentGrowTime += growSpeed * Time.deltaTime;
        }
    }
}
