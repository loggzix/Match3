using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public enum eNormalType
    {
        TYPE_ONE = 0,
        TYPE_TWO = 1,
        TYPE_THREE = 2,
        TYPE_FOUR = 3,
        TYPE_FIVE = 4,
        TYPE_SIX = 5,
        TYPE_SEVEN = 6
    }

    public eNormalType ItemType;
    public GameManager.eTheme Theme;

    public NormalItem(GameManager.eTheme theme)
    {
        this.Theme = theme;
    }

    public void SetType(eNormalType type)
    {
        ItemType = type;
    }

    public override void SetView()
    {
        var dataItems = GameManager.Instance.DataItems;
        int index = (int)ItemType;
        var prefab = dataItems.GetPrefabItem(Theme, index);
        
        if (prefab)
        {
            View = GameObject.Instantiate(prefab).transform;
        }
    }

    // protected override string GetPrefabName()
    // {
    //     string prefabname = string.Empty;
    //     switch (ItemType)
    //     {
    //         case eNormalType.TYPE_ONE:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_ONE;
    //             break;
    //         case eNormalType.TYPE_TWO:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_TWO;
    //             break;
    //         case eNormalType.TYPE_THREE:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_THREE;
    //             break;
    //         case eNormalType.TYPE_FOUR:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_FOUR;
    //             break;
    //         case eNormalType.TYPE_FIVE:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_FIVE;
    //             break;
    //         case eNormalType.TYPE_SIX:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_SIX;
    //             break;
    //         case eNormalType.TYPE_SEVEN:
    //             prefabname = Constants.PREFAB_NORMAL_TYPE_SEVEN;
    //             break;
    //     }

        // return prefabname;
    // }

    internal override bool IsSameType(Item other)
    {
        NormalItem it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }
}
