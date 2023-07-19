using BreakInfinity;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePreFab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradePreFab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;

    //public Sprite[] Images;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;
    public BigDouble[] clickUpgradesUnlock;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;
    public BigDouble[] productionUpgradesUnlock;


    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 3);

        clickUpgradeNames = new string[] { "Click Power +1", "Click Power +5", "Click Power +10" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };
        clickUpgradesUnlock = new BigDouble[] { 0, 25, 50 };

        productionUpgradeNames = new[]
        {
            "Apples +$1/s",
            "Carrots +$2/s",
            "Grapes +$10/s",
            "Pies +$100/s",
        };
        productionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        productionUpgradesBasePower = new BigDouble[] { 1, 2, 10, 100 };
        productionUpgradesUnlock = new BigDouble[] { 1, 2, 10, 100 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePreFab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradePreFab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
    }

    public void Update()
    {
        for(int i = 0; i < clickUpgrades.Count; i++)
        {
            if(!clickUpgrades[i].gameObject.activeSelf)
                clickUpgrades[i].gameObject.SetActive(Controller.instance.data.money >= clickUpgradesUnlock[i]);
        }
        for (int i = 0; i < productionUpgrades.Count; i++)
        {
            if (!productionUpgrades[i].gameObject.activeSelf)
                productionUpgrades[i].gameObject.SetActive(Controller.instance.data.money >= productionUpgradesUnlock[i]);
        }
    }

    public void UpdateUpgradeUI(string type, int UpgradeID = -1)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click":
                if (UpgradeID == -1)
                {
                    for (int i = 0; i < clickUpgrades.Count; i++)
                    {
                        UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                    }
                }
                else
                {
                    UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, UpgradeID);
                }
                break;
            case "production":
                if (UpgradeID == -1)
                {
                    for (int i = 0; i < productionUpgrades.Count; i++)
                    {
                        UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                    }
                }
                else
                {
                    UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, UpgradeID);
                }
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            upgrades[ID].Level.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost: $ {UpgradeCost(type, ID):F2}";
            upgrades[ID].NameText.text = upgradeNames[ID];
            //upgrades[ID].Produce.sprite = Images[ID];

        }
    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], (BigDouble)data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], (BigDouble)data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click":
                Buy(data.clickUpgradeLevel);
                break;
            case "production":
                Buy(data.productionUpgradeLevel);
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.money >= UpgradeCost(type, UpgradeID))
            {
                data.money -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
            }

            UpdateUpgradeUI(type, UpgradeID);
        }
    }
}
