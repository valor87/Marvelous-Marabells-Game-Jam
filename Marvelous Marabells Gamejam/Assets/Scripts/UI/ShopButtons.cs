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
    }
    public void deactivateButton()
    {
        if(_shopManager._playerMoney.currentMoney < _itemCost)
            return;
        _eventCore.UE_PurchasedItem.Invoke(_itemCost);
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
