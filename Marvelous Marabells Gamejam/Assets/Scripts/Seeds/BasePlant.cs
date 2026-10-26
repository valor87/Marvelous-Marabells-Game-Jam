using UnityEngine;

public class BasePlant : MonoBehaviour
{
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _growTime;
    [SerializeField] private float _currentGrowTime;

    [SerializeField] private bool _isFertalized;

    void Start()
    {
       transform.localScale = Vector3.one;
    }

    void Update()
    {
        if (_growTime >= _currentGrowTime) {
            transform.localScale = (Vector3.one/2) * (_currentGrowTime/_growTime);
            _currentGrowTime += _growSpeed * Time.deltaTime;
        }
    }
}
