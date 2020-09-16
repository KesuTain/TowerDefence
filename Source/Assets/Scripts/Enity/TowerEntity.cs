using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerEntity : Entity
{
    [Header("Параметры башни")]
    [SerializeField]
    private float SpeedOfShooting;
    [SerializeField]
    private int Damage;

    [Header("Боеприпас")]
    public GameObject CannonCore;
    [SerializeField]
    private GameObject ParentForPatrons;

    [Header("Улучшение башни")]
    public int CostUpgrade = 40;
    public Text TextCost;
    public GameObject AreaUpgrade;

    private bool CanShot;
    void Start()
    {
        ResourceSystem.Money = ResourceSystem.LoadMoney();
        ParentForPatrons = gameObject.transform.GetChild(1).gameObject;
        CanShot = true;
        Damage = 100 * ResourceSystem.FactorDamageTower;
        SpeedOfShooting = 2f;
    }

    void Update()
    {
        CheckCost();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            Shooting(other.gameObject);
        }
    }

    public void Shooting(GameObject Enemy)
    {
        if (CanShot)
            StartCoroutine(Shoot(Enemy));
    }

    IEnumerator Shoot(GameObject Target)
    {
        ParentForPatrons.transform.LookAt(Target.transform);
        Instantiate(CannonCore, ParentForPatrons.transform);
        CanShot = false;
        Target.GetComponent<EnemyEntity>().Health -= Damage;
        yield return new WaitForSeconds(SpeedOfShooting);
        CanShot = true;
    }

    //Проверка стоимости здания
    void CheckCost()
    {
        TextCost.text = CostUpgrade.ToString();
        if (Damage >= 500)
        {
            AreaUpgrade.SetActive(false);
        }
    }

    //Улучшение башни
    public void UpgradeTower()
    {
        Debug.Log("Try Upgrade Tower");
        if(ResourceSystem.Money >= CostUpgrade && Damage < 500)
        {
            ResourceSystem.Money -= CostUpgrade;
            Damage += 100 * ResourceSystem.FactorDamageTower;
            CostUpgrade = CostUpgrade * (Damage / 100);
            SpeedOfShooting /= 2f;
            gameObject.transform.localScale *= 1.1f;
        }
    }

}
