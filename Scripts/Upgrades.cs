using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public Image Upgradebutton;
    public TMP_Text Level;
    public TMP_Text CostText;
    public TMP_Text NameText;
    //public Image Produce;

    public void BuyClickUpgrade()
    {
        UpgradeManager.instance.BuyUpgrade("click",UpgradeID);
    }
    public void BuyProductionUpgrade()
    {
        UpgradeManager.instance.BuyUpgrade("production", UpgradeID);
    }
}
