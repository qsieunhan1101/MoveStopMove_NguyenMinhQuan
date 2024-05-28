using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();
    //khoi tao pool moi
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY!!");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.PoolType) || poolInstance[prefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, amount, parent);
            poolInstance[prefab.PoolType] = p;
        }
    }
    //lay phan tu ra
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }
    //tra phan tu vao
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType + "IS NOT PRELOAD!!");
        }
        poolInstance[unit.PoolType].Despawn(unit);
    }
    //thu thap phan tu
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!");
        }
        poolInstance[poolType].Collect();
    }
    //thu thap tat ca
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }
    //destroy 1 pool
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD!!");
        }
        poolInstance[poolType].Release();
    }
    //destroy tat ca
    public static void ReleaseAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Release();
        }
    }

}

public class Pool
{
    Transform parent;
    GameUnit prefab;
    //list chua cac unit dang o trong pool
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    //list chua cac unit dang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();


    public void Preload(GameUnit prefab, int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < amount; i++)
        {
            Despawn(Spawn(Vector3.zero, Quaternion.identity));
        }
    }

    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.Tf.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }

    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}
