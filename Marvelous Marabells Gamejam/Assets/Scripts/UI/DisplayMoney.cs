using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private string _text;
    [SerializeField] private float _amount;
    [SerializeField] private float _targetAmount;
    
    private Eventcore _eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
        _eventCore.UE_SoldItem.AddListener(changeMoney);
        _eventCore.UE_PurchasedItem.AddListener(changeMoney);
        StartCoroutine(moneyFunctions());
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = $"{_text} {_amount}";
    }

    void changeMoney(int changeAmount)
    {
        _targetAmount = changeAmount;
        StartCoroutine(moneyFunctions());
    }

    IEnumerator moneyFunctions()
    {
        float fadeAmount = 0;
        while(fadeAmount < 1)
        {
            _moneyText.color = new Color(1f, 1f, 1f, fadeAmount);
            fadeAmount += 0.005f;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        while (_amount < _targetAmount)
        {
            _amount++;
            yield return new WaitForSeconds(0.01f);
        }
        while (_amount > _targetAmount)
        {
            _amount--;
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
