using UnityEngine;
using UnityEngine.UI;

public class StartBoxFade : MonoBehaviour
{
    [Header("Fade box properties")]
    [SerializeField] private float waitTime = 3;
    [SerializeField] private float fadeAmount = 0.005f;
    [SerializeField] private Image _fadeBox;
    [HideInInspector] public float totalWaitTime;
    FadeScript _fade = new FadeScript();
    private void Start()
    {
        totalWaitTime = waitTime + (fadeAmount / 1) * 2;
    }
    [ContextMenu("Start fade")]
    public void startFade()
    {
        StartCoroutine(_fade.fadeFunctionUI(_fadeBox, Color.black, fadeAmount, waitTime));
    }
}
