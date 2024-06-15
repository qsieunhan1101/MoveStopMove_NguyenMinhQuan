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



    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int levelIndex)
    {
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

    }
    public void GetLevel()
    {

    }
    public void DestroyLevel()
    {
        if (this.mapLevelParent != null)
        {
            Destroy(this.mapLevelParent);
            Debug.Log("destroy level");
        }
    }

    private void SpawnEnemy(int count, float range , Transform map)
    {
        
        for (int i = 0; i< count;i++)
        {
            Vector3 randomPoint = Random.insideUnitSphere * range;
            randomPoint += map.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                /*GameObject enmy = Instantiate(enemyPrefab, this.mapLevelParent);
                enmy.transform.position = hit.position;*/

                Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, hit.position, Quaternion.identity);
                
            }
            else
            {
                i--;
            }
        }
    }

}
