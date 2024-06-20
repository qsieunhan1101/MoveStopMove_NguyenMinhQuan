using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Character")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header ("MapLevel")]
    [SerializeField] private List<GameObject> mapLevelPrefabs;
    [SerializeField] private Transform mapLevelParent;

    private GameObject currentMapLevel;
    private GameObject currentPlayer;
    private GameObject currentEnemy;

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            if (value < 0)
            {
                level = value;
            }
            else
            {
                level = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerDataManager.Instance.playerEquipmentData.level;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int levelIndex)
    {
        EnemyManager.Instance.ResetTotalEnemy();
        EnemyManager.Instance.DespawnAllEnemy();
        EnemyManager.Instance.ResetListEnableEnemy();


        if (levelIndex > mapLevelPrefabs.Count-1)
        {
            levelIndex = 0;
        }
        DestroyLevel();
        GameObject lvParent = new GameObject("Level_Parent");
        this.mapLevelParent = lvParent.transform;

        currentMapLevel = Instantiate(mapLevelPrefabs[levelIndex], this.mapLevelParent);
        GameObject player = Instantiate(playerPrefab, this.mapLevelParent);

        CameraFollow mainCam = CameraFollow.Instance;
        mainCam.target = player;
        mainCam.SetUpCamera(10,5);
        EnemyManager.Instance.SpawnFristTime();
   
    }
    public void SaveLevel()
    {
        PlayerDataManager.Instance.playerEquipmentData.level = this.level;
        PlayerDataManager.Instance.UpdatePlayerEquipmentData();
    }
    public int GetLevel()
    {
        int a;
        PlayerEquipmentData p = PlayerDataManager.Instance.LoadPlayerEquipmentData() ;
        a = p.level;
        return a ;
    }

    public void ReLoadLevel()
    {
        LoadLevel(GetLevel());
    }
    
    public void DestroyLevel()
    {
        if (this.mapLevelParent != null)
        {
            Destroy(this.mapLevelParent.gameObject);
            Debug.Log("destroy level");
        }
    }

}
