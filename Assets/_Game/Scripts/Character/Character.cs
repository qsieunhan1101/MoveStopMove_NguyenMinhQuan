using System.Collections;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected CharacterState charState;

    public float rangeAttack;
    public LayerMask characterLayerMask;

    [SerializeField] protected Transform targetAttack;
    public Transform TargetAttack => targetAttack;
    protected Vector3 bulletDirection;



    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform bulletPoint;
    [SerializeField] protected Transform bulletPointDir;
    protected float angle;


    [SerializeField] protected Collider[] enemyColliders;


    [SerializeField] float forceAttack;

    [SerializeField] public Transform bodyTransform;

    public float frameRate = 1;
    public float time = 1f;

    

    //Anim
    [SerializeField] protected Animator anim;
    [SerializeField] protected string currentAnimName;


    //pool
    [SerializeField] private BulletBase bulletBasePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void OnInit()/////////////////
    {
        enemyColliders = new Collider[10];

    }

    protected virtual void OnDespawn()//////////////////
    {

    }
    public virtual void ChangeAnim(string animName)///////////////
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }

    }
    public virtual void SpawnWeapon()/////////////////
    {

        //GameObject b = Instantiate(bulletPrefab);
        //b.transform.position = bulletPoint.position;

        BulletBase bb = SimplePool.Spawn<BulletBase>(PoolType.Bullet_1, bulletPoint.position, bulletPoint.rotation);
            
        Vector3 dir = (bulletPointDir.position - this.transform.position).normalized;

        
        //b.GetComponent<BulletBase>().rb.AddForce(dir * forceAttack);
        //BulletBase bs = b.GetComponent<BulletBase>();
        bb.forceAttack = forceAttack;
        bb.dir = dir;
        int sign = 1;
        if (bulletPointDir.position.x < this.transform.position.x)
        {
            sign = -1;
        }
        angle = Vector3.Angle(bulletPointDir.forward, Vector3.forward) * sign;
        bb.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Attack()
    {
        //Vector3 dir = (targetAttack.position - this.transform.position).normalized;
        //GetRotation(dir);

        
        ChangeAnim(Cache.Anim_Attack);
        Invoke(nameof(SpawnWeapon),0.5f);
        
    }

    void Wait()
    {

    }

    protected virtual void Death()/////////////////
    {
                                                                
    }
    protected virtual void GetTargetOtherCharacter()
    {

        int numberHitEnemyCatch = Physics.OverlapSphereNonAlloc(this.transform.position, rangeAttack, enemyColliders, characterLayerMask);
        float closestDistance = Mathf.Infinity;

        Transform lastTarget;

        for (int i = 0; i < numberHitEnemyCatch; i++)
        {

            float distance = Vector3.Distance(enemyColliders[i].transform.position, this.transform.position);
            if (distance < closestDistance && enemyColliders[i].transform != this.transform)
            {
                if (targetAttack != null && this is Player)
                {
                    lastTarget = targetAttack;
                    lastTarget.GetComponent<Enemy>().spriteRenderer.enabled = false;

                }
                targetAttack = enemyColliders[i].transform;
                closestDistance = distance;
                Debug.Log(enemyColliders[i].name);
                
            }
            if (numberHitEnemyCatch == 1)
            {
                targetAttack = null;
            }
        }

    }

    protected virtual Quaternion GetRotation(Vector3 rotation)
    {
        return bodyTransform.rotation = Quaternion.LookRotation(rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, rangeAttack);
    }

    public virtual bool IsHaveTargetAttack()
    {
        if (targetAttack != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
