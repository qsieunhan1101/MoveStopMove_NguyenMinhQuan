using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody rb;
    [SerializeField] private float speed = 5f;
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
        Destroy(this.gameObject,3f);
    }
}
