using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    IState currentState;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radiusDestination = 10f;
    [SerializeField] private bool isDead;

    private Vector3 destinationTarget;
    public Vector3 DestinationTarget => destinationTarget;
    public SpriteRenderer spriteRenderer;



    public bool IsDead => isDead;

    private void OnEnable()
    {
        OnInit();
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }


        if (GameManager.Instance.CurrentState == GameState.Gameplay)
        {
            GetTargetOtherCharacter();

        }


    }


    protected override void OnInit()
    {
        base.OnInit();

        isDead = false;
        ChangeState(new IdleState());
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Character);

    }

    protected override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
        transform.position = new Vector3(-44, 0.5f, 43);
        enemyColliders = new Collider[10];
    }


    public void Move(Vector3 target)
    {
        if (isDead)
        {
            return;
        }
        agent.SetDestination(target);

    }

    protected override void GetTargetOtherCharacter()
    {
        base.GetTargetOtherCharacter();
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
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized * radiusDestination;
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
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Default);
        StopMoving();
        ChangeAnim(Constant.Anim_Dead);
        isDead = true;
        targetAttack = null;

        EnemyManager.Instance.UpdateTotalEnemy();

        OnDespawn();
    }



}
