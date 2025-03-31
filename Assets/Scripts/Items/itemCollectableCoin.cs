using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollectableCoin : itemCollectableBase
{
    protected override void collect()
    {
        base.collect();
        ItemManager.Instance.addCoins();
    }
}
