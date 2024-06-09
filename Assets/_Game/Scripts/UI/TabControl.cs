using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabControl : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefabs;
    [SerializeField] private Transform buttonParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateItem()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject btn = Instantiate(buttonPrefabs, buttonParent);
            
        }
    }
}
