using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyEntity : Entity
{
    [Header("Параметры врага")]
    [SerializeField]
    private int Money;
    [SerializeField]
    private int Damage;
    public int Health;

    [Header("Активность")]
    public bool Activity;

    private NavMeshAgent Agent;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Враг умирает если его здоровье падает ниже 0 и он при этом активен.
        if (Health <= 0 && Activity == true)
        {
            Death();
        }

        //Если враг активен, то он следует к базе игрока, если же деактивирован,
        //то вынесен из зоны видимости игрока.
        if (Activity)
        {
            Agent.enabled = true;
            Agent.SetDestination(ResourceSystem.BasePlayer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Base Player")
        {
            DamagePlayer();
        }
    }
    //Установка значений.
    public void SetSettings(int Wave)
    {
        Health = 100 * Wave;
        Money = 2 * Wave;
        Damage = 10 * Wave;
    }

    //Смерть.
    private void Death()
    {
        ResourceSystem.Money += Money;
        ResourceSystem.CountKillEnemies++;
        Deactivation();
    }

    //Атака базы.
    private void DamagePlayer()
    {
        ResourceSystem.HealthPlayer -= Damage;
        Deactivation();
    }

    //Деактивация.
    private void Deactivation()
    {
        Activity = false;
        Agent.enabled = false;
        ResourceSystem.SetZeroPosition(gameObject);
    }
}
