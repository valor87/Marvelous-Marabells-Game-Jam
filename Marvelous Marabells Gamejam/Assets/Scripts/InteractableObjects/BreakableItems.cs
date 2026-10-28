using UnityEngine;

public class BreakableItems : MonoBehaviour, IInteractable
{
    [SerializeField] private toolTypes _toolNeededToDestroy;
    public toolTypes toolRequired { get; set; }
    private ParticleSystem _particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Execute(toolTypes toolNeeded)
    {
        if (toolNeeded == _toolNeededToDestroy)
        {
            _particleSystem.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
