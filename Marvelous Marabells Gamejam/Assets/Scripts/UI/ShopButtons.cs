using UnityEngine;
using UnityEngine.UI;
public class ShopButtons : MonoBehaviour
{
    private Button _thisButton;
    
    [SerializeField] ShopManager _shopManager;
    [SerializeField] private GameObject _soldOutText;
    [SerializeField] private int _itemCost;
    [SerializeField] private GameObject objectToBuy;
    [SerializeField] private Plants buyingPlant;
    [SerializeField] private PlayerInventory playerInventroy;

    [Header("Sound Effects")]
    [SerializeField] audioData[] buyingSoundEffect = new audioData[0];
    [SerializeField] AudioManager _audioManager;

    Eventcore _eventCore;

    void Start()
    {
        _thisButton = GetComponent<Button>();
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }
    public void processBuying()
    {
        Debug.Log(isArrayFull(playerInventroy.playerTools));
        if (_shopManager._playerMoney.currentMoney < _itemCost || isArrayFull(playerInventroy.playerTools))
            return;
        playerInventroy.addSeedBagToInventory(buyingPlant);
        _eventCore.UE_PurchasedItem.Invoke(_itemCost);
        _eventCore.UE_ToolInventoryChange.Invoke();
        int randomNumber = Random.Range(0, buyingSoundEffect.Length - 1);
        _audioManager.playerSound(buyingSoundEffect[randomNumber].ac, buyingSoundEffect[randomNumber].voloume);
    }
    public void deactivateButton()
    {
        if(_shopManager._playerMoney.currentMoney < _itemCost)
            return;
        _eventCore.UE_PurchasedItem.Invoke(_itemCost);
        int randomNumber = Random.Range(0, buyingSoundEffect.Length - 1);
        _audioManager.playerSound(buyingSoundEffect[randomNumber].ac, buyingSoundEffect[randomNumber].voloume);
        _soldOutText.SetActive(true);
        if(objectToBuy != null)
            objectToBuy.SetActive(true);
        _thisButton.enabled = false;
    }
    private bool isArrayFull<T>(T[] checkingArray)
    {
        for (int i = 0; i < checkingArray.Length; i++)
        {
            var item = checkingArray[i];
            if (item == null)
            {
                return false;
            }
        }
        return true;
    }
}
