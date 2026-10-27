using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerMoney _playerMoney = new PlayerMoney();
    public int _currentMoney;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            _playerMoney.currentMoney = _currentMoney;
    }

}
