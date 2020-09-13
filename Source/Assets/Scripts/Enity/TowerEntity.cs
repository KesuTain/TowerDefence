using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool CanShot;
    void Start()
    {
        ParentForPatrons = gameObject.transform.GetChild(1).gameObject;
        CanShot = true;
        Damage = 100;
        SpeedOfShooting = 2f;
    }

    void Update()
    {
        
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
        Instantiate(CannonCore, ParentForPatrons.transform);
        CanShot = false;
        Target.GetComponent<EnemyEntity>().Health -= Damage;
        yield return new WaitForSeconds(SpeedOfShooting);
        CanShot = true;
    }

    public void UpgradeTower()
    {
        Damage += 100;
        SpeedOfShooting /= 2f;
        gameObject.transform.localScale *= 1.1f;
    }

}
