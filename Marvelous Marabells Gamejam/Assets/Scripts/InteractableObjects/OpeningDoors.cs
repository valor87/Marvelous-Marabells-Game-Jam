using UnityEngine;

public class OpeningDoors : MonoBehaviour, IInteractable
{
    [Header("opening settings")]
    [SerializeField] float _timeToOpen;
    [SerializeField] float _stayOpenBuffer;
    [SerializeField] Transform _endTransform;

    private float _stayOpenCurrentTime;
    private float _currentTime;
    private float _closeCurrentTime;

    Quaternion _endRotation;
    Quaternion _startRotation;

    bool _activate;

    public toolTypes toolRequired { get; set;}
    
    void Start()
    {
        _startRotation = transform.rotation;
        _endRotation = _endTransform.rotation;
    }

    void Update()
    {
        if (_activate) {
           
            if (_currentTime / _timeToOpen >= 0.99f)
            {
                closeTimer();
            }
            else
            {
                _currentTime += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, _currentTime / _timeToOpen);
            }
        }
    }

    void closeTimer()
    {
        _stayOpenCurrentTime += Time.deltaTime;
        if (_stayOpenCurrentTime >= _stayOpenBuffer)
        {
            _closeCurrentTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(_endRotation, _startRotation, _closeCurrentTime / _timeToOpen);
           
            if (_closeCurrentTime / _timeToOpen >= 0.98f)
            {
                resetAllValues();
            }
        }
        else
        {
            transform.rotation = _endRotation;
        }
    }

    void resetAllValues()
    {
        _stayOpenCurrentTime = 0;
        _currentTime = 0;
        _closeCurrentTime = 0;
        _activate = false;
    }

    public void Execute(toolTypes arg)
    {
        if (arg == toolTypes.None)
        {
            _activate = true;
            _currentTime = 0;
        }
    }
}
