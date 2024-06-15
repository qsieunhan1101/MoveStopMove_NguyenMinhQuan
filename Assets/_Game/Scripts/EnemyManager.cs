using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private List<Vector3> listSpawnPos;
    [SerializeField] private int totalEnemy = 30;


    private int maxEnemy = 10;
    public int TotalEnemy => totalEnemy;
    public List<Enemy> Enemies => enemies;
    public static Action totalEnemyUpdateEvent;



    void Update()
    {
        CheckAndSpawnEnemy();
    }

    public void CheckAndSpawnEnemy()
    {
        enemies.RemoveAll(enemy => enemy == null || !enemy.gameObject.activeSelf);

        int activeEnemies = enemies.Count;

        if (activeEnemies < maxEnemy && totalEnemy > 10)
        {
            SpawnEnemy();

        }

    }

    public void SpawnEnemy()
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, listSpawnPos[UnityEngine.Random.Range(0, listSpawnPos.Count)], Quaternion.identity);
        enemies.Add(enemy);
    }

    public void SpawnFristTime()
    {
        for (int i = 0; i < listSpawnPos.Count - 1; i++)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, listSpawnPos[i], Quaternion.identity);
            enemies.Add(enemy);

        }
    }

    public void UpdateTotalEnemy()
    {
        totalEnemy--;
        totalEnemyUpdateEvent?.Invoke();
    }

}
