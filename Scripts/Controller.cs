using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;using System;
public class Controller : MonoBehaviour
{
    public static Controller instance;

    private void Awake()
    {
        instance = this; 
    }

    public Data data;

    [SerializeField] private TMP_Text dollarText;
    [SerializeField] private TMP_Text moneyPerSecondText;
    [SerializeField] private TMP_Text clickPowerText;

    public BigDouble clickPower()
    {
        BigDouble total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradeManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }

    public BigDouble MoneyPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradeManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
        }
        return total;
    }

    private void Start()
    {
        data = new Data();
        data.money = 0;

        UpgradeManager.instance.StartUpgradeManager();
    }

    private void Update()
    {
        dollarText.text = "$" + data.money.ToString("F2");
        moneyPerSecondText.text = $"{MoneyPerSecond():F2}/s";
        clickPowerText.text = "+" + clickPower() + " Products Sold";

        data.money += MoneyPerSecond() * Time.deltaTime;
    }

    public void sellToCustomer()
    {
        data.money+= clickPower();
    }
    public BigDouble linearBigDoubleEquation(BigDouble number)
    {
        return 5 * number;
    }
    public BigDouble squareRootBigDoubleEquation(BigDouble number) 
    { 
        return BigDouble.Sqrt(number); 
    }
    public BigDouble squareRootBigDoubleEquation2(BigDouble number)
    {
        return BigDouble.Pow(number, 0.5);
    }
    public BigDouble polynomialBigDoubleEquation(BigDouble number)
    {
        return BigDouble.Pow(number, 2);
    }
    public BigDouble exponentialBigDoubleEquation(BigDouble number)
    {
        return BigDouble.Pow(2, number);
    }
    public BigDouble logBigDoubleEquation(BigDouble number)
    {
        return BigDouble.Log(number, 2);
    }
    public BigDouble exponentialBigDoubleEquation2(BigDouble number)
    {
        return BigDouble.Pow(10, number);
    }
    public BigDouble logBigDoubleEquation2(BigDouble number)
    {
        return BigDouble.Log10(number);
    }
}
