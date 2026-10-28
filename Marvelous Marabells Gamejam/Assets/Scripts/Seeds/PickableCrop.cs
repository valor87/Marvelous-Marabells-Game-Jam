using UnityEngine;

public class PickableCrop : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    [SerializeField] private int _cropCost;
    
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _growDuration;
    private float _currentGrowTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _currentGrowTime += _growSpeed * Time.deltaTime; 
    }

    public void Execute(toolTypes args)
    {
        if (args != toolTypes.None || _currentGrowTime < _growDuration)
            return;
    }
}
