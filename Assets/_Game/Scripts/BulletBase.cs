using UnityEngine;

public class BulletBase : GameUnit
{

    public enum BulletType
    {
        Turning = 0,
        NoTurning = 1,
    }

    [SerializeField] private BulletType bulletType = BulletType.Turning;
    [SerializeField] private Character characterOwner;
    [SerializeField] private Transform bodyAnim;
    public Vector3 dir;
    public float speed = 5;

    private void OnEnable()
    {
        Invoke(nameof(OnDespawm), 2);
    }

    // Update is called once per frame
    void Update()
    {


        transform.position = transform.position + dir * speed * Time.deltaTime;

        if (bulletType == BulletType.Turning)
        {
            bodyAnim.Rotate(new Vector3(0, 1, 0), 180 * speed * Time.deltaTime, Space.Self);

        }

    }
    public void OnInit()
    {

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
        if (other.gameObject.layer == LayerMask.NameToLayer(Constant.Layer_Character))
        {

            if (other.transform != characterOwner.transform)
            {
                if (other.CompareTag(Constant.Tag_Player))
                {
                    Player player = other.GetComponent<Player>();
                    player.Death();
                    characterOwner.SetScore();
                }
                if (other.CompareTag(Constant.Tag_Enemy))
                {
                    Enemy enemy = other.GetComponent<Enemy>();
                    enemy.Death();
                    characterOwner.SetScore();
                }

            }
        }
    }
}
