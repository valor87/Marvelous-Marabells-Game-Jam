using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameRequirementsUI : MonoBehaviour
{
    [SerializeField] private int _woodQoutaNumber;
    [SerializeField] private int _stoneQoutaNumber;
    [SerializeField] private int _costQoutaNumber;

    [SerializeField] private TextMeshProUGUI _woodQouta;
    [SerializeField] private TextMeshProUGUI _stoneQouta;
    [SerializeField] private TextMeshProUGUI _costQouta;

    [SerializeField] private Button _endGameButton;

    [SerializeField] private Image _FadeBox;

    [SerializeField] private playerProgress _playerProgress;

    private FadeScript _fadeScript = new FadeScript();
    void displayInformation()
    {
        _woodQouta.text = $"Wood: {_playerProgress.playerWoodMaterials}/{_woodQoutaNumber}";
        _stoneQouta.text = $"Stone: {_playerProgress.playerStoneMaterials}/{_stoneQoutaNumber}";
        _costQouta.text = $"Cost: {_playerProgress.playerMoney}/{_costQoutaNumber}";
    }
    void activateButton()
    {
        if (_playerProgress.playerMoney >=_costQoutaNumber &&
            _playerProgress.playerWoodMaterials >= _woodQoutaNumber &&
            _playerProgress.playerStoneMaterials >= _stoneQoutaNumber)
        {
            _endGameButton.interactable = true;
        }
       
    }

    private void Update()
    {
        displayInformation();
        activateButton();

        if (_fadeScript.fadeComplete)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void endGame()
    {
        Debug.Log("button hit");
        StartCoroutine(_fadeScript.fadeFunctionUI(_FadeBox, Color.black));
    }
}
