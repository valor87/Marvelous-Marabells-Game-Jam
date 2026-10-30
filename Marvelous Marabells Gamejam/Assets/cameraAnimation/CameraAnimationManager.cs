using System.Collections;
using UnityEngine;

public class CameraAnimationManager : MonoBehaviour
{
    [SerializeField]
    CameraAnimation[] animations;
    private void Start()
    {
        StartCoroutine(lateStart());
    }
    [ContextMenu("Play animations")]
    void playAllAnimations()
    {
        StartCoroutine(manageAnimation());
    }
    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(.10f);
        playAllAnimations();
    }
    IEnumerator manageAnimation()
    {
        foreach (var animation in animations)
        {
            yield return animation.camMovement();
        }
    }
}
