using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("Точки спавна")]
    [SerializeField]
    private List<PointEntity> Points;

    [Header("Типы врагов")]
    public List<EnemyEntity> TypeEnemy;

    [Header("Нулевая позиция врагов")]
    [SerializeField]
    private Vector3 ZeroPositionEnemies;

    [Header("Количество врагов")]
    [SerializeField]
    private int CountEnemies;

    [Header("Враги")]
    [SerializeField]
    private List<GameObject> Enemies;

    [Header("Интервал между волнами")]
    public float WaveInterval;

    [Header("Интервал между врагами")]
    public float EnemyInterval;

    [Header("Номер волны")]
    public int NumberOfWave;

    public Transform PointForEnemies;
    private bool WaveActivity;

    void Start()
    {
        WaveActivity = true;
        FindSpawnPoints();
        WaveInterval = ResourceSystem.IntervalWave;
        StartCoroutine(Wave(NumberOfWave + Random.Range(CountEnemies, CountEnemies + 2)));
    }
    void Update()
    {
        if (WaveActivity == true && CheckWave() == true)
            StartCoroutine(Wave(NumberOfWave + Random.Range(CountEnemies, CountEnemies + 2)));
    }

    bool CheckWave()
    {
        int Now = 0;
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].GetComponent<EnemyEntity>().Activity == true)
            {
                Now++;
            }
        }

        if (Now == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Поиск точек для спавна врагов.
    private void FindSpawnPoints()
    {
        Points = new List<PointEntity>(FindObjectsOfType<PointEntity>());
    }

    //Создание врагов и установка им нулевого значения координат (пул объектов).

    //Функцию можно вызывать с указанием требуемого количества врагов. 
    //В итоге если для первой волны нужно 5 врагов, а для второй 10,
    //то к имеющимся 5 добавится ещё 5, а не создатся 10 новых.
    public void CreateEnemies(int CountEnemies)
    {
        if (Enemies.Count > CountEnemies)
        {
            for (int i = Enemies.Count; i > CountEnemies; i--)
            {
                Enemies.Remove(Enemies[i]);
            }
        }
        for (int i = Enemies.Count; i < CountEnemies; i++)
        {
            ChoosingTypeOfEnemy();

            //Установка нулевой позиции и деактивация.
            ResourceSystem.SetZeroPosition(Enemies[i]);
            Enemies[i].GetComponent<EnemyEntity>().Activity = false;
        }
    }
    private void ChoosingTypeOfEnemy()
    {
        //Выбор любого из представленных типов врагов и его создание.
        int j = Random.Range(0, TypeEnemy.Count);
        
        //Добавление врагов в список.
        Enemies.Add(Instantiate(TypeEnemy[j].gameObject, PointForEnemies));
    }

    //Спавн врагов волнами.
    IEnumerator Wave(int CountEnemies)
    {
        WaveActivity = false;
        NumberOfWave++;
        CreateEnemies(CountEnemies);
        StartCoroutine(SpawnEnemies());
        Debug.Log("Wait " + WaveInterval + " seconds");
        Debug.Log(Enemies.Count);
        yield return new WaitForSeconds(WaveInterval);
        WaveActivity = true;
    }
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].transform.position = FindPositionForSpawn();
            Enemies[i].GetComponent<EnemyEntity>().SetSettings(NumberOfWave);
            Enemies[i].GetComponent<EnemyEntity>().Activity = true;
            yield return new WaitForSeconds(EnemyInterval);
        }
    }
    
    //Поиск точки для спавна.
    Vector3 FindPositionForSpawn()
    {
        int i = Random.Range(0, Points.Count);
        return Points[i].transform.position;
    }
}
