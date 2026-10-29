using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
public class Tool : MonoBehaviour
{
    public ToolsObject thisTool;
    public Sprite toolImage;
    public seedTypes thisSeedType;
    [SerializeField] Mesh _toolMesh;
    public float rotationChangeValue;

    [Header("Sound Effects")]
    [SerializeField] bool _useThisSounds = false;
    [SerializeField] audioData[] toolSoundEffect = new audioData[0];
    [SerializeField] AudioManager _audioManager;
    [HideInInspector] public int _thisDurability = 0;
    Eventcore _eventCore;
    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _thisDurability = thisTool.durability;
        _eventCore = GameObject.Find("EventCore").GetComponent<Eventcore>();
    }
    public virtual void useToolPrimary()
    {   

        StartCoroutine(useHoeAnimation());
        if (_useThisSounds)
        {
            int randomNumber = Random.Range(0, toolSoundEffect.Length - 1);
            _audioManager.playerSound(toolSoundEffect[randomNumber].ac, toolSoundEffect[randomNumber].voloume);
        }
        Debug.Log($"Tools {gameObject.name} primary has been used");
    }

    public virtual void useToolSecondary()
    {
        Debug.Log($"Tools {gameObject.name} secondary has been used");
    }

    public virtual void equipTool()
    {
        Debug.Log($"Tools {gameObject.name} has been equipped");
    }

    public virtual void deactivateTool()
    {
        gameObject.SetActive(false);
    }

    public virtual void activateTool()
    {
        gameObject.SetActive(true);
    }
    public virtual void breakTool()
    {
        Debug.Log($"Tools {gameObject.name} has been broken");
    }

    public void fallowPlayer(Transform _playerTransform)
    {
        transform.position = _playerTransform.position;
        transform.eulerAngles = _playerTransform.eulerAngles;
    }

    IEnumerator useHoeAnimation()
    {
        float rotationValue = 0;
        Vector3 tempValue = transform.eulerAngles;

        while (rotationValue <= 90)
        {
            rotationValue += rotationChangeValue * Time.deltaTime;
            tempValue.x += rotationChangeValue * Time.deltaTime;
            transform.eulerAngles = tempValue;
            yield return null;
        }
        while (rotationValue >= 0)
        {
            rotationValue -= rotationChangeValue * Time.deltaTime;
            tempValue.x -= rotationChangeValue * Time.deltaTime;
            transform.eulerAngles = tempValue;
            yield return null;
        }
    }
}
