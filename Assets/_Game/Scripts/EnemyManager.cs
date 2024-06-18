using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private List<Vector3> listSpawnPos;
    [SerializeField] private int totalEnemyDefault;
    [SerializeField] private int maxEnemy = 5;

    private int totalEnemy;
    public int TotalEnemy => totalEnemy;
    public List<Enemy> Enemies => enemies;
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
        for (int i = 0; i < maxEnemy; i++)
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
