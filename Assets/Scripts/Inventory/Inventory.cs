using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int MAXCOUNTITEM = 10;
    [SerializeField] private ItemSO[] items = new ItemSO[MAXCOUNTITEM];
    private int FreeItemIndex = 0;

    public ItemSO GetItemByIndex(int index)
    {
        return items[index];
    }
    public ItemSO GetItemById(int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if (items[i].IDItem == index)
                {
                    return items[i];
                }
            }
        }
        return null;
    }
    public bool HandlerNewItem(ItemSO newItem)
    {
        //обработчик нового предмета
        //если не нашлось к какому предмету добавить
        //создаем новый
        bool IsAddItem=false;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                if (newItem.IDItem == items[i].IDItem)
                {
                    AddItem(newItem, i);
                    IsAddItem = true;
                    return true;
                    
                }
            }
        }
        if (IsAddItem==false)
        {
            if (NewItem(newItem))
            {
                return true;
            }
            
        }
        return false;
    }
    public void AddItem(ItemSO newItem,int index)
    {
        items[index].countItem += newItem.countItem;    
    }
    public bool NewItem(ItemSO newItem)
    {
        if (FreeItemIndex < MAXCOUNTITEM)
        {
            var clone = Instantiate(newItem);
            items[FreeItemIndex] = clone;
            FreeItemIndex++;
            return true;
        }
        else return false;
    }
    public void DeleteItem(int index)
    {
        items[index] = null;
    }
    public int GetCountInventory()
    {
        return MAXCOUNTITEM;
    }
}
