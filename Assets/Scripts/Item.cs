using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        StrongBall,
        NoWindBall,
        WindBall,
        SpikeBall,
        PlasticBall
    }
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.StrongBall: return ItemAssets.Instance.StrongBall;
            case ItemType.SpikeBall: return ItemAssets.Instance.SpikeBall;
            case ItemType.NoWindBall: return ItemAssets.Instance.NoWindBall;
            case ItemType.PlasticBall: return ItemAssets.Instance.PlasticBall;

        }
    }

    public ItemType itemType;
    public int amount;

}
