using UnityEngine;
using UnityEngine.UI;

public class DisplayEnergy : MonoBehaviour
{
    [SerializeField] private PlayerEnergy _playerEnergy;
    [SerializeField] private Image energyImage;

    void Update()
    {
        energyImage.fillAmount = _playerEnergy.currentEnergy/_playerEnergy.maxEnergy;
    }
}
