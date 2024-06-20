
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    IState currentState;
    [Header ("Character_Enemy")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float radiusDestination = 10f;
    [SerializeField] private Material[] enemyMaterial;
    private Vector3 defaultEnemyPosition = new Vector3(-44, 0.5f, 43);

    private Vector3 destinationTarget;
    public Vector3 DestinationTarget => destinationTarget;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        originSetMaterial = enemyMaterial[Random.Range(0, enemyMaterial.Length)];
        EquippedRandomWeapon();
        EquippedRandomSkin();
        RandomName();
    }


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
        if (GameManager.Instance.CurrentState != GameState.Gameplay)
        {
            StopMoving();
        }


    }


    protected override void OnInit()
    {
        base.OnInit();

        isDead = false;
        ChangeState(new IdleState());
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Character);
        characterScore = 0;
    }

    protected override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
        transform.position = defaultEnemyPosition;
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
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Default);
        StopMoving();
        ChangeAnim(Constant.Anim_Dead);
        isDead = true;
        targetAttack = null;
        characterScore = 0;

        EnemyManager.Instance.UpdateTotalEnemy();

        Invoke(nameof(OnDespawn), 1.5f);
        //OnDespawn();
    }

    public override void EquippedWeapon(WeaponType weaponType)
    {
        base.EquippedWeapon(weaponType);
    }

    public override void EquippedSkinItemUI(int idListEquiment, string equipmentName)
    {
        base.EquippedSkinItemUI(idListEquiment, equipmentName);
    }

    private void EquippedRandomWeapon()
    {
        weaponType = (WeaponType)Random.Range(0, System.Enum.GetValues(typeof(WeaponType)).Length);
        EquippedWeapon(weaponType);
    }

    private void EquippedRandomSkin()
    {

        int idListEquiment = Random.Range(0, 4);
        string equipmentName = Constant.Default_EquipmentName;
        UserData userData = LocalDataManager.Instance.UserData;
        switch (idListEquiment)
        {
            case 0:
                equipmentName = userData.ListHatDatas[Random.Range(0, userData.ListHatDatas.Count)].equipmentName;
                break;
            case 1:
                equipmentName = userData.ListPantDatas[Random.Range(0, userData.ListPantDatas.Count)].equipmentName;
                break;
            case 2:
                equipmentName = userData.ListShieldDatas[Random.Range(0, userData.ListShieldDatas.Count)].equipmentName;
                break;
            case 3:
                equipmentName = userData.ListSkinDatas[Random.Range(0, userData.ListSkinDatas.Count)].equipmentName;
                break;
        }

        EquippedSkinItemUI(idListEquiment, equipmentName);

    }

    public override void SetScore()
    {
        base.SetScore();
    }

    private void RandomName()
    {
        List<string> listName = LocalDataManager.Instance.UserData.ListNames;
        characterName = listName[Random.Range(0, listName.Count)];
    }
}
