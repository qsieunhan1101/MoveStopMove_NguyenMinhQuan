using UnityEngine;

public enum PoolType
{
    Hammer = 0,
    Lolipop = 1,
    Knife = 2,
    CandyCane = 3,
    Bomerang = 4,
    SwirlyPop = 5,
    Axe = 6,
    IceScream = 7,
    BattleAxe = 8,
    Arrow = 9,
    Uzi = 10,


    Enemy = 11,
    None = 12,  
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
