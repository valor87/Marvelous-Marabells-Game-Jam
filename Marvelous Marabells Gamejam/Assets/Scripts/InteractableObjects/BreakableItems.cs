using UnityEngine;

public class BreakableItems : MonoBehaviour, IInteractable
{
    [SerializeField] private toolTypes _toolNeededToDestroy;

    [Header("Sound Effects")]
    [SerializeField] audioData[] breakingSoundEffect = new audioData[0];
    [SerializeField] AudioManager _audioManager;

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
            int randomNumber = Random.Range(0, breakingSoundEffect.Length - 1);
            _audioManager.playerSound(breakingSoundEffect[randomNumber].ac, breakingSoundEffect[randomNumber].voloume);
            _particleSystem.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
