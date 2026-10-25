using UnityEngine;

public class BasePlant : MonoBehaviour
{
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _growTime;
    [SerializeField] private float _currentGrowTime;

    [SerializeField] private bool _isFertalized;

    [SerializeField] private Plants _growingPlant;
    private GameObject _plantObject;
    void Start()
    {
        _plantObject = Instantiate(_growingPlant.plantObject);
        _plantObject.transform.position = transform.position + Vector3.up;
        _plantObject.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if (_growTime >= _currentGrowTime) {
            _plantObject.transform.localScale = (Vector3.one/2) * (_currentGrowTime/_growTime);
            _currentGrowTime += _growSpeed * Time.deltaTime;
        }
    }
}
