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

    [SerializeField]
    private Camera camera;
    void Update()
    {
        CheckStats();
        if (Input.GetMouseButtonUp(0))
            UpgradeTower();
    }

    //Обновление значений UI
    void CheckStats()
    {
        if (ResourceSystem.Money > 0)
        {
            Money.text = ResourceSystem.Money.ToString();
        }
        else
        {
            Money.text = "0";

        }
        if (ResourceSystem.HealthPlayer > 0)
        {
            Health.text = ResourceSystem.HealthPlayer.ToString();
        }
        else
        {
            Money.text = "0";
            LoseMenu();
        }
    }

    void LoseMenu()
    {
        if (ResourceSystem.CountKillEnemies > 0)
        {
            CountKillEnemies.text = "Убито: " + ResourceSystem.CountKillEnemies.ToString();
        }
        else
        {
            CountKillEnemies.text = "Убито: " + "0";
        }
        Time.timeScale = 0;
        ReloadMenu.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0); 
        ResourceSystem.HealthPlayer = 50;
        ResourceSystem.Money = 0;
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
}
