using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public static class ResourceSystem
{
    //Значения золота, хп, убитых врагов, волны.
    public static int Money;

    public static int FactorHomeTower = 1;
    public static int FactorDamageTower = 1;

    public static int HealthPlayer = 50 * FactorHomeTower;
    public static int CountKillEnemies = 0;
    public static int Wave = 0;

    public static int CostUpgradeTower = 1500 * FactorDamageTower;
    public static int CostUpgradeHome = 100 * FactorHomeTower;

    //Конфигурация интервала волны.
    public static float IntervalWave = 10;
    //Нулевая позиция.
    public static Vector3 ZeroPosition = GameObject.Find("Zero Position").transform.position;
    //Позиция базы игрока.
    public static Vector3 BasePlayer = GameObject.Find("Base Player").transform.position;

    //Установка нулевого значения.
    public static void SetZeroPosition(GameObject clone)
    {
        clone.transform.position = ZeroPosition;
    }

    public static XElement SaveParameters()
    {
        XAttribute Interval = new XAttribute("WaveInterval", IntervalWave);

        XElement Element = new XElement("WaveParameters", "TimeBetweenWave", Interval); 

        return Element;
    }

    public static void SaveMoney(int MoneyNow)
    {
        PlayerPrefs.SetInt("Money", MoneyNow);
    }

    public static int LoadMoney()
    {
        Money = PlayerPrefs.GetInt("Money");
        return Money;
    }

    public static void SaveFactors()
    {
        PlayerPrefs.SetInt("FactorTower", FactorDamageTower);
        PlayerPrefs.SetInt("FactorHome", FactorHomeTower);
    }
    public static void LoadFactors()
    {
        FactorDamageTower = PlayerPrefs.GetInt("FactorTower");
        FactorHomeTower = PlayerPrefs.GetInt("FactorHome");
    }
}
