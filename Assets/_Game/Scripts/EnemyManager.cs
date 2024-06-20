using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<Enemy> listEnableEnemies = new List<Enemy>();
    [SerializeField] private List<Enemy> listAllEnemies;
    [SerializeField] private List<Vector3> listSpawnPos;
    [SerializeField] private int totalEnemyDefault;
    [SerializeField] private int maxEnemy = 5;

    private int totalEnemy;
    public int TotalEnemy => totalEnemy;
    public List<Enemy> ListEnableEnemies => listEnableEnemies;
    public static Action totalEnemyUpdateEvent;

    private void Start()
    {
        ResetTotalEnemy();
    }

    void Update()
    {

        if (GameManager.Instance.CurrentState == GameState.Gameplay)
        {
            CheckTotalEnemy();
            CheckAndSpawnEnemy();

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            DespawnAllEnemy();

        }

    }

    public void CheckAndSpawnEnemy()
    {
        listEnableEnemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf);

        int activeEnemies = listEnableEnemies.Count;

        if (activeEnemies < maxEnemy && totalEnemy > 10)
        {
            SpawnEnemy();

        }

    }
    public void SpawnFristTime()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, listSpawnPos[UnityEngine.Random.Range(0, listSpawnPos.Count)], Quaternion.identity);
        listEnableEnemies.Add(enemy);

        AddEnemyToListAllEnemy(enemy);


    }

    public void AddEnemyToListAllEnemy(Enemy enemy)
    {
        bool enemyInList = false;

        foreach (Enemy e in listAllEnemies)
        {
            if (enemy.transform == e.transform)
            {
                enemyInList = true;
                break;
            }
        }

        if (enemyInList == false)
        {
            listAllEnemies.Add(enemy);
        }
        else
        {
            Debug.Log("Co trong list roi");
        }
    }
    public void ResetListEnableEnemy()
    {
        listEnableEnemies = new List<Enemy>();
    }

    public void DespawnAllEnemy()
    {
        foreach (Enemy enemy in listAllEnemies)
        {
            SimplePool.Despawn(enemy);
        }
    }

    public void UpdateTotalEnemy()
    {
        totalEnemy--;
        totalEnemyUpdateEvent?.Invoke();
    }

    private void CheckTotalEnemy()
    {
        if (totalEnemy < 1 && GameManager.Instance.CurrentState != GameState.Victory)
        {
            GameManager.Instance.Victory();
        }
    }
    public void ResetTotalEnemy()
    {
        totalEnemy = totalEnemyDefault;
    }

}
