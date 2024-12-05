using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Item item, Transform spawnPoint)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, spawnPoint);
        ItemWorld itemWorld  = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item item;
    public void SetItem(Item item)
    {
        this.item = item;
    }
}
