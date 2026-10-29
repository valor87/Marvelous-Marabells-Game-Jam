using UnityEngine;

[System.Serializable]
public struct audioData
{
    public AudioClip ac;
    public float voloume;
}


public class AudioManager : MonoBehaviour
{
    public void playerSound(AudioClip _AC, float volume = 1)
    {
        GetComponent<AudioSource>().pitch = Random.Range(1.0f, 1.5f);
        GetComponent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().PlayOneShot(_AC);
    }
}
