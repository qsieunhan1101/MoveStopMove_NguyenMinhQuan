using UnityEngine;

public class BulletBase : GameUnit
{

    [SerializeField] private Character characterOwner;

    public Rigidbody rb;

    [SerializeField] private Transform bodyAnim;

    [SerializeField] private float time;

    public float forceAttack;
    public Vector3 dir;


    float t;


    public float speed = 5;




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

        transform.position = transform.position + dir * speed * Time.deltaTime;

        bodyAnim.Rotate(new Vector3(0, 1, 0), 180 * speed * Time.deltaTime, Space.Self);
        
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


    public void SetCharacterOwner(Character owner)
    {
        characterOwner = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Cache.Layer_Character))
        {

            if (other.transform != characterOwner.transform)
            {
                if (other.CompareTag("Player"))
                {
                    Player player = other.GetComponent<Player>();
                    player.Death();
                    characterOwner.SetScore();
                }
                if (other.CompareTag("Enemy"))
                {
                    Enemy enemy = other.GetComponent<Enemy>();
                    enemy.Death();
                    characterOwner.SetScore();
                }

            }
        }
    }
}
