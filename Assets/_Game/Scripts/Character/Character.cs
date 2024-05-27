using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected CharacterState charState;

    public float rangeAttack;
    public LayerMask characterLayerMask;

    [SerializeField] protected Transform targetAttack;
    protected Vector3 bulletDirection;



    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform bulletPoint;
    [SerializeField] protected Transform bulletPointDir;
    protected float angle;


    [SerializeField] protected Collider[] enemyColliders;


    [SerializeField] float forceAttack;

    [SerializeField] private Transform bodyTransform;

    protected float frameRate = 1;
    protected float time = 1f;

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
    public virtual void ChangeAnim()///////////////
    {


    }
    public virtual void Attack()/////////////////
    {
       
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = bulletPoint.position;
        Vector3 dir = (bulletPointDir.position - this.transform.position).normalized;
        b.GetComponent<BulletBase>().rb.AddForce(dir * forceAttack);
        int sign = 1;
        if (bulletPointDir.position.x < this.transform.position.x)
        {
            sign = -1;
        }
        angle = Vector3.Angle(bulletPointDir.forward, Vector3.forward) * sign;
        b.transform.rotation = Quaternion.Euler(0, angle, 0);
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
