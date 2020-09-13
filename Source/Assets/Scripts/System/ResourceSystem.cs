using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceSystem
{
    //Значения золота, хп, убитых врагов.
    public static int Money;
    public static int HealthPlayer = 50;
    public static int CountKillEnemies = 0;
    //Нулевая позиция.
    public static Vector3 ZeroPosition = GameObject.Find("Zero Position").transform.position;
    //Позиция базы игрока.
    public static Vector3 BasePlayer = GameObject.Find("Base Player").transform.position;

    //Установка нулевого значения.
    public static void SetZeroPosition(GameObject clone)
    {
        clone.transform.position = ZeroPosition;
    }
}
