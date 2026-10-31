using UnityEngine;

public class SellBox : MonoBehaviour, IInteractable
{
    [Header("Sound Effects")]
    [SerializeField] audioData[] sellingSoundEffect = new audioData[0];
    [SerializeField] AudioManager _audioManager;

    public toolTypes toolRequired { get; set; }
    private Eventcore _eventCore;
    [HideInInspector] public PickableCrop _sellingCrop; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }
    public void Execute(toolTypes arg)
    {
        if(_sellingCrop == null)
            return;
        int randomNumber = Random.Range(0, sellingSoundEffect.Length - 1);
        _audioManager.playerSound(sellingSoundEffect[randomNumber].ac, sellingSoundEffect[randomNumber].voloume);
        _eventCore.UE_SoldItem.Invoke(_sellingCrop.thisPlantObejct.sellAmount);
        _sellingCrop = null;
    }
}
