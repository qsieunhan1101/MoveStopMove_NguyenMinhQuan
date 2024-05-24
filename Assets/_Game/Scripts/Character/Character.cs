using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] protected IState currentState;


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

    //ChangeState-------------------------------
    public void ChangeState(IState newState, Character character)
    {
        if (currentState != null)
        {
            currentState.OnExit(character);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(character);
        }

    }

    public void StateExecute(Character character)
    {
        //State Execute----------
        if (currentState != null)
        {
            currentState.OnExecute(character);
        }
    }

    public virtual void TestMethod()
    {
        Debug.Log("day la Character---");
    }
}
