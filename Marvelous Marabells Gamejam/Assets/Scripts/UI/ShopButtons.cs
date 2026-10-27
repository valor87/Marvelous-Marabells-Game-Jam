using UnityEngine;
using UnityEngine.UI;
public class ShopButtons : MonoBehaviour
{
    private Button _thisButton;
    
    [SerializeField] ShopManager _shopManager;
    [SerializeField] private GameObject _soldOutText;
    [SerializeField] private int _itemCost;
    [SerializeField] private GameObject objectToBuy;
    Eventcore _eventCore;

    void Start()
    {
        _thisButton = GetComponent<Button>();
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
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

    
}
