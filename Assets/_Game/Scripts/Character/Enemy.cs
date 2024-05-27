using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    IState currentState;

    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent agent;

    Vector3 destinationTarget;

    [SerializeField] private float radiusDestination = 10f;




    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsHaveTargetAttack() == true)
            {

                Vector3 dir = (targetAttack.transform.position - this.transform.position).normalized;
                GetRotation(dir);

                Attack();
            }

        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(new MoveState());
        }


        GetTargetOtherCharacter();


    }


    protected override void OnInit()
    {
        base.OnInit();

    }


    public override void Attack()
    {
        base.Attack();
    }

    public void Move(Vector3 target)
    {

        agent.SetDestination(target);

    }

    public void StopMoving()
    {
        destinationTarget = this.transform.position;
        agent.SetDestination(destinationTarget);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public Vector3 SetAgentDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radiusDestination;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radiusDestination, -1);
        return navHit.position;
    }


}
