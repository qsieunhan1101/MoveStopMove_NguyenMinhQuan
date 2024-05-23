using UnityEngine;

public class Character : MonoBehaviour
{
    public IState<Character> currentState;


    public float rangeAttack;
    public LayerMask characterLayerMask;

    protected Transform targetAttack;
    protected Vector3 bulletDirection;
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

    }

    protected virtual void OnDespawn()//////////////////
    {

    }

    protected virtual void Move()///////////////
    {

    }
    public virtual void ChangeAnim()///////////////
    {


    }
    public virtual void Attack()/////////////////
    {

    }
    protected virtual void Death()/////////////////
    {

    }
    protected virtual void GetTargetOtherCharacter()
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, rangeAttack);
    }
}
