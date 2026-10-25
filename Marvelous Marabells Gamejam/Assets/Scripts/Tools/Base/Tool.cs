using UnityEngine;
using System.Collections;
public class Tool : MonoBehaviour
{
    public ToolsObject thisTool;
    public Sprite toolImage;
    [SerializeField] Mesh _toolMesh;
    public float rotationChangeValue;

    public virtual void useToolPrimary()
    {
        StartCoroutine(useHoeAnimation());

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
        Debug.Log("Running");
        float rotationValue = 0;
        Vector3 tempValue = transform.eulerAngles;

        while (rotationValue <= 90)
        {
            Debug.Log("running " + rotationValue);
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
