using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGraund : MonoBehaviour
{
    public ItemSO _itemSO;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (player._inventory.HandlerNewItem(_itemSO))
            {
                Destroy(gameObject);
            }
        }
    }
}
