using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIConroller : MonoBehaviour
{
    [Header("Золото")]
    public Text Money;
    [Header("Здоровье")]
    public Text Health;
    [Header("Количество убитых врагов")]
    public Text CountKillEnemies;
    [Header("Меню перезапуска")]
    public GameObject ReloadMenu;

    public Text CostUpAllTower;
    public Text UpAllTowerNow;

    public Text CostUpHome;
    public Text UpHomeNow;
    [SerializeField]
    private Camera camera;
    private void Start()
    {
        if(PlayerPrefs.GetInt("FactorTower") != 0)
        {
            ResourceSystem.LoadFactors();
        }
    }
    void Update()
    {
        DebugT();
        CheckStats();
        if (Input.GetMouseButtonUp(0))
            UpgradeTower();
    }
    void DebugT()
    {
        Debug.Log(ResourceSystem.FactorDamageTower);
        //Debug.Log();
    }
    //Обновление значений UI
    void CheckStats()
    {
        Money.text = ResourceSystem.Money.ToString();
        Health.text = ResourceSystem.HealthPlayer.ToString();
        if (ResourceSystem.HealthPlayer <= 0)
        {
            LoseMenu();
        }
    }

    void LoseMenu()
    {
        CheckAllTowerUpdate();
        CheckHomeUpdate();
        ResourceSystem.SaveMoney(ResourceSystem.Money);
        CountKillEnemies.text = "Убито: " + ResourceSystem.CountKillEnemies.ToString();
        Time.timeScale = 0;
        ReloadMenu.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0); 
        ResourceSystem.HealthPlayer = 50;
        ResourceSystem.Money = ResourceSystem.LoadMoney();
        Time.timeScale = 1;
        ReloadMenu.SetActive(false);
    }

    void UpgradeTower()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tower"))
            {
                hit.collider.transform.GetComponentInParent<TowerEntity>().UpgradeTower();
            }
        }
    }

    public void UpgradeAllTower()
    {
        if (ResourceSystem.Money >= ResourceSystem.CostUpgradeTower)
        {
            ResourceSystem.Money -= ResourceSystem.CostUpgradeTower;
            ResourceSystem.FactorDamageTower += 1;
            ResourceSystem.SaveFactors();
        }
    }

    public void CheckAllTowerUpdate()
    {
        CostUpAllTower.text = "Стоимость " + ResourceSystem.CostUpgradeTower.ToString();
        UpAllTowerNow.text = "Башни усилены в " + ResourceSystem.FactorDamageTower.ToString() + " раз";
    }

    public void UpgradeHome()
    {
        if (ResourceSystem.Money >= ResourceSystem.CostUpgradeHome)
        {
            ResourceSystem.Money -= ResourceSystem.CostUpgradeHome;
            ResourceSystem.FactorHomeTower += 1;
            ResourceSystem.SaveFactors();
        }
    }

    public void CheckHomeUpdate()
    {
        CostUpHome.text = "Стоимость " + ResourceSystem.CostUpgradeHome.ToString();
        UpHomeNow.text = "Дом укреплён в " + ResourceSystem.FactorHomeTower.ToString() + " раз";
    }
}
