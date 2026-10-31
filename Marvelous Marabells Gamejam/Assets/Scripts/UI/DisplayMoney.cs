using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private string _text;
    [SerializeField] private int _targetAmount;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private PlayerData _playerData;
    private int moneyDisplay = 0;
    private Eventcore _eventCore;
    private Coroutine _coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_SoldItem.AddListener(gainMoney);
        _eventCore.UE_PurchasedItem.AddListener(spendMoney);
        _eventCore.UE_SavePlayerData.AddListener(saveMoneyData);
        _shopManager._playerMoney.currentMoney = 0;
        _targetAmount = _playerData.varPlayerProgress.playerMoney;
        StartCoroutine(gainMoneyFunctions());
        _shopManager._playerMoney.currentMoney = _targetAmount;

    }

    // Update is called once per frame
    void Update()
    {
        _playerData.varPlayerProgress.playerMoney = _shopManager._playerMoney.currentMoney;
        _moneyText.text = $"{_text} {moneyDisplay}";
    }

    void saveMoneyData()
    {
        _playerData.varPlayerProgress.playerMoney = _shopManager._playerMoney.currentMoney;
        _playerData.varPlayerProgress.day++;
    }
    void spendMoney(int changeAmount)
    {
        _targetAmount -= changeAmount;

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(spendMoneyFunctions());
        }
        _shopManager._playerMoney.currentMoney -= changeAmount;

    }
    void gainMoney(int changeAmount)
    {
        _targetAmount += changeAmount;
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(gainMoneyFunctions());
        }
        _shopManager._playerMoney.currentMoney += changeAmount;

    }
    IEnumerator gainMoneyFunctions()
    {
        moneyDisplay = _shopManager._playerMoney.currentMoney;
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
            moneyDisplay++;
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
        _coroutine = null;
    }
    IEnumerator spendMoneyFunctions()
    {
        moneyDisplay = _shopManager._playerMoney.currentMoney;

        float fadeAmount = 0;

        while (fadeAmount < 1)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount += 0.005f;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (_targetAmount < 0)
        {
            moneyDisplay--;
            _targetAmount++;
            yield return new WaitForSeconds(0.01f);
        }
       
        yield return new WaitForSeconds(0.5f);

        while (fadeAmount > 0)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount -= 0.005f;
            yield return null;
        }
        _coroutine = null;
    }
}
