using UnityEngine;

public enum PoolType
{
    Bullet_1 = 0,
    Bullet_2 = 1,
    Bullet_3 = 2,
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
