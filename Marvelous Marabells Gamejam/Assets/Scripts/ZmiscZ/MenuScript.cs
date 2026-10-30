using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] int _sceneIndex;
    public void loadGameScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void endGameScene()
    {
        Application.Quit();
    }
}
