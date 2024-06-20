using UnityEngine;

public enum PoolType
{
    //Weapon
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

    //Character
    Enemy = 11,
    None = 12, 
    
    //Sound
    DeadSound_1 = 13,
    DeadSound_2 = 14,
    WeaponThrowSound = 15,
    BackgroundSound = 16,
    ButtonSound = 17,

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



}


[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
