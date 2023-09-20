using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory _inventory;
    [SerializeField] GameObject[] ItemUI;
    [SerializeField] Sprite startSprite;
    
    public void Awake()
    {
        GameInput.Instance.OnInventoryOpenAction += GameInput_OnInventoryAction;
    }
    public void Start()
    {
        DrawInventoryUI();
    }

    private void GameInput_OnInventoryAction(object sender, System.EventArgs e)
    {
        //отрисовка по событию открыти€ дл€ избежание посто€нного вызова этого в Update
        DrawInventoryUI();
    }

    public void DrawInventoryUI()
    {
        //отрисовка интерфейса
        for (int i = 0; i < _inventory.GetCountInventory(); i++)
        {
            ItemUI[i].GetComponent<ItemDelete>().indexItem = i;
            if (_inventory.GetItemByIndex(i) != null)
            {
                ItemUI[i].transform.GetChild(0).GetComponent<Image>().sprite = _inventory.GetItemByIndex(i).iconItem;
                if (_inventory.GetItemByIndex(i).countItem==1)
                {
                    ItemUI[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;
                }
                else
                {
                    ItemUI[i].transform.GetChild(1).GetComponent<TMP_Text>().text = _inventory.GetItemByIndex(i).countItem.ToString();
                }
                
            }
            else
            {
                ItemUI[i].transform.GetChild(0).GetComponent<Image>().sprite = startSprite;
                ItemUI[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;
            }
            
        }
    }
}
