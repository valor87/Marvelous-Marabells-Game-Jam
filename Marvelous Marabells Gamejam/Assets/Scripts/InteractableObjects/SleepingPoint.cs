using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepingPoint : MonoBehaviour, IInteractable
{
    public toolTypes toolRequired { get; set; }
    public toolTypes actionRequired = toolTypes.None;
    [SerializeField] Transform playersTransform;
    [SerializeField] float totalDuration = 2;
    Vector3 playerStartPosition;

    Quaternion startRotation;
    Quaternion endRotation;
    bool begin = false;
    float time = 0.0f;
    public void Execute(toolTypes neededAction)
    {
        startRotation = playersTransform.rotation;
        endRotation = transform.rotation;

        playerStartPosition = playersTransform.position;

        playersTransform.GetComponent<PlayerActions>().enabled = false;
        playersTransform.GetComponent<PlayerMovement>().enabled = false;
        playersTransform.GetComponent<PlayerCamera>().setRotationSpeed(0);

        begin = true;
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
                SceneManager.LoadScene(0);
            }
        }

        
    }
}