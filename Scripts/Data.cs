using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using System.Linq;

public class Data 
{
    public BigDouble money;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public Data()
    {
        money = 0;
        clickUpgradeLevel = Methods.CreateList<int>(capacity: 3);
        productionUpgradeLevel = new int[4].ToList();
    }

}
