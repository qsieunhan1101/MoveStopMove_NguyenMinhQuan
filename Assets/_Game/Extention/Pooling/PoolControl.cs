using UnityEngine;

public enum PoolType
{
    None = 0,
    Enemy = 1,
    Bullet_1 = 2,
    Bullet_2 = 3,
    Bullet_3 = 4,
}
public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}


[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
