using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepingPoint : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    public toolTypes actionRequired = toolTypes.None;
    [SerializeField] Transform playersTransform;
    [SerializeField] float totalDuration = 2;
    [SerializeField] StartBoxFade _boxFade;
    Vector3 playerStartPosition;

    Quaternion startRotation;
    Quaternion endRotation;
    bool begin = false;
    float time = 0.0f;
    float fadeTimer = 0;

    public void Execute(toolTypes neededAction)
    {
        startRotation = playersTransform.rotation;
        endRotation = transform.rotation;

        playerStartPosition = playersTransform.position;

        playersTransform.GetComponent<PlayerActions>().enabled = false;
        playersTransform.GetComponent<PlayerMovement>().enabled = false;
        playersTransform.GetComponent<PlayerCamera>().setRotationSpeed(0);
        begin = true;
        fadeTimer = _boxFade.totalWaitTime;
    }

    void Update()
    {
        if (begin)
        {
            time += .5f * Time.deltaTime;

            float percentComplete = time / totalDuration;
            playersTransform.position = Vector3.Lerp(playerStartPosition, transform.position, percentComplete);
            playersTransform.rotation = Quaternion.Lerp(startRotation, endRotation, percentComplete);

            if (percentComplete >= 0.90f)
            {
                _boxFade.startFade();
                fadeTimer -= Time.deltaTime;
                if (fadeTimer < 0)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        
    }
}