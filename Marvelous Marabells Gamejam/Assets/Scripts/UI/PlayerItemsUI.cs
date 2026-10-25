using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Image[] _toolImages = new Image[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 0;
        foreach (Tool item in _playerInventory.playerTools)
        {
            if (item == null)
            {
                i++;
                continue;
            }
            _toolImages[i].sprite = item.toolImage;
            i++;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
