using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private string _text;
    [SerializeField] private float _targetAmount;
    [SerializeField] private ShopManager _shopManager;
    private Eventcore _eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_SoldItem.AddListener(gainMoney);
        _eventCore.UE_PurchasedItem.AddListener(spendMoney);
        StartCoroutine(gainMoneyFunctions());
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = $"{_text} {_shopManager._playerMoney.currentMoney}";
    }
    void spendMoney(int changeAmount)
    {
        _targetAmount = changeAmount;
        StartCoroutine(spendMoneyFunctions());
    }
    void gainMoney(int changeAmount)
    {
        _targetAmount = changeAmount;
        StartCoroutine(gainMoneyFunctions());
    }
    IEnumerator gainMoneyFunctions()
    {
        float fadeAmount = 0;
        while (fadeAmount < 1)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount += 0.005f;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (0 < _targetAmount)
        {
            _shopManager._playerMoney.currentMoney++;
            _targetAmount--;
            yield return new WaitForSeconds(0.01f);
        }
       
        yield return new WaitForSeconds(0.5f);

        while (fadeAmount > 0)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount -= 0.005f;
            yield return null;
        }
    }
    IEnumerator spendMoneyFunctions()
    {
        float fadeAmount = 0;
        while (fadeAmount < 1)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount += 0.005f;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (_targetAmount > 0)
        {
            _shopManager._playerMoney.currentMoney--;
            _targetAmount--;
            yield return new WaitForSeconds(0.01f);
        }
       
        yield return new WaitForSeconds(0.5f);

        while (fadeAmount > 0)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount -= 0.005f;
            yield return null;
        }
    }
}
