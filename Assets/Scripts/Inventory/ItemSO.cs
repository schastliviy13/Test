using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    public int IDItem;
    public int countItem;
    public Sprite iconItem;
}
