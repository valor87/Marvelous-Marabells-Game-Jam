using UnityEngine;

public class BreakableItems : MonoBehaviour, IInteractable
{
    [SerializeField] private toolTypes _toolNeededToDestroy;
    public toolTypes toolRequired { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Execute(toolTypes toolNeeded)
    {
        if (toolNeeded == _toolNeededToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
}
