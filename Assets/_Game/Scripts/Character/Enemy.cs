using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    IState currentState;

    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent agent;

    private Vector3 destinationTarget;
    public Vector3 DestinationTarget => destinationTarget;
    [SerializeField] private float radiusDestination = 10f;



    [SerializeField] private bool isDead;
    public bool IsDead => isDead;


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

                SpawnWeapon();
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
        isDead = false;
        ChangeState(new IdleState());
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
        Vector3 randomDirection = Random.insideUnitSphere.normalized * radiusDestination;
        randomDirection += transform.position;
        NavMeshHit navHit;
        bool hasHit = NavMesh.SamplePosition(randomDirection, out navHit, radiusDestination, -1);

        if (hasHit == false)
        {
            return this.transform.position;
        }
        return navHit.position;
    }

    public void EnemyGetRotation(Vector3 dir)
    {
        base.GetRotation(dir);
    }


    public override void Death()
    {
        base.Death();
        StopMoving();
        ChangeAnim(Cache.Anim_Dead);
        isDead = true;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }



}
