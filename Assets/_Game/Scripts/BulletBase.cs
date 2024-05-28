using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : GameUnit
{

    public Rigidbody rb;

    [SerializeField] private float time;

    public float forceAttack;
    public Vector3 dir;


    float t;


    public float speed =5;

    // Start is called before the first frame update
    void Start()
    {
        //OnInit();
        
    }
    private void OnEnable()
    {
        OnInit();
        Invoke(nameof(OnDespawm), 2);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + Vector3.forward *speed * Time.deltaTime;
        //transform.Translate(Vector3.forward*speed*Time.deltaTime);
        
        transform.position = transform.position + dir*speed * Time.deltaTime;
        
    }
    public void OnInit()
    {
       // rb.AddForce(dir * forceAttack);
    }
    public void OnDespawm()
    {
        //Destroy(this.gameObject,time);
        SimplePool.Despawn(this);
    }
}
