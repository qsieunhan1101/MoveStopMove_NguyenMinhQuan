using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    public Rigidbody rb;

    [SerializeField] private float time;
    // Start is called before the first frame update
    void Start()
    {
        OnDespawm();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + Vector3.forward *speed * Time.deltaTime;
        //transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
    public void OnInit()
    {

    }
    public void OnDespawm()
    {
        Destroy(this.gameObject,time);
    }
}
