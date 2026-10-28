using UnityEngine;
using UnityEngine.UI;

public class DisplayWater : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerWater;
    [SerializeField] private Image waterImage;

    void Update()
    {
        waterImage.fillAmount = _playerWater.currentWater/ _playerWater.maxWater;
    }
}
