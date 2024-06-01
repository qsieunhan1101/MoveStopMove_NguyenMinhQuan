using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Enemy ee = SimplePool.Spawn<Enemy>(PoolType.Enemy, transform.position, transform.rotation);
            ee.transform.position = new Vector3(0, 0.5f, 0);
        }
    }
}
