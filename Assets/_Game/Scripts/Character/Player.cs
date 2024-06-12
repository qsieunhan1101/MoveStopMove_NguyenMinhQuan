using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    private Vector3 startTouch, currentTouch;
    private Vector3 moveDirection;

    private Vector3 rotationDirection;



    [SerializeField] private float speed;




    [SerializeField] private bool isMoving;
    [SerializeField] private bool isDead;
    public bool IsDead => isDead;


    [SerializeField] private Transform lastTargetAttack;







    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Vector3.back;
        isMoving = false;

        OnInit();

        //ChangeState(new IdleState(), this);
    }
    private void OnEnable()
    {
        CanvasWeapon.buttonSelectEvent += EquippedWeapon;
        SkinItemUI.itemOnclickEvent += EquippedSkin;

    }

    private void OnDisable()
    {
        CanvasWeapon.buttonSelectEvent -= EquippedWeapon;
        SkinItemUI.itemOnclickEvent -= EquippedSkin;


    }
    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            Move();

        }


        //attack
        //if (isMoving == false && IsHaveTargetAttack() && !isDead)
        //{
        //    time += Time.deltaTime;
        //    if (time >= frameRate)
        //    {
        //        Vector3 dir = (targetAttack.transform.position - this.transform.position).normalized;
        //        GetRotation(dir);
        //        time -= frameRate;
        //        Attack();
        //    }
        //}
        if (isMoving == false && IsHaveTargetAttack() && !isDead)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;

                Vector3 dir = (targetAttack.transform.position - this.transform.position).normalized;
                GetRotation(dir);
                Attack();
            }
        }
        if (isMoving)
        {
            nextTimeToFire = 0;
        }



        if (isMoving == true && !IsHaveTargetAttack())
        {
            nextTimeToFire = fireRate;
        }


        GetTargetOtherCharacter();

        ActiveEnemyTargetSprite();



        if (Input.GetKeyDown(KeyCode.N))
        {
            Attack();
            Debug.Log(originalScale);
            UpdateSize((int)characterScore);
        }




        if (isMoving)
        {
            anim.ResetTrigger(Cache.Anim_Attack);
            ChangeAnim(Cache.Anim_Run);
            CancelInvoke(nameof(SpawnWeapon));
        }
        if (!isMoving && !isDead)
        {
            ChangeAnim(Cache.Anim_Idle);
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
        isDead = false;
        this.gameObject.layer = LayerMask.NameToLayer("Character");
        //load data weapon
        this.weaponType = PlayerDataManager.Instance.GetWeaponState();
        EquippedWeapon(this.weaponType);

        EquippedSkin(PlayerDataManager.Instance.GetEquipmentState().Item1, PlayerDataManager.Instance.GetEquipmentState().Item2);
    }
    protected override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Death()
    {
        base.Death();
        isDead = true;
        ChangeAnim(Cache.Anim_Dead);
        this.gameObject.layer = LayerMask.NameToLayer("Default");

    }

    private void Move()
    {
        GetTouchDirection();
        if (isMoving == true)
        {
            GetRotation(rotationDirection);
        }
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }
    private void GetTouchDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position;

            }
            if (touch.phase == TouchPhase.Moved)
            {
                isMoving = true;
                currentTouch = touch.position;
                moveDirection = (currentTouch - startTouch).normalized;
                moveDirection.z = moveDirection.y;
                moveDirection.y = 0;
                rotationDirection = moveDirection;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveDirection = Vector3.zero;
                isMoving = false;

            }
        }

    }





    // ban ra vien dan theo huong truoc mat character
    public override void SpawnWeapon()
    {
        base.SpawnWeapon();

    }
    protected override void GetTargetOtherCharacter()
    {
        base.GetTargetOtherCharacter();
        //ActiveEnemyTargetSprite();
    }

    private void ActiveEnemyTargetSprite()
    {

        //if (IsHaveTargetAttack() == true)
        //{
        //    if (Vector3.Distance(this.transform.position, targetAttack.transform.position) <= rangeAttack)
        //    {
        //        targetAttack.GetComponent<Enemy>().spriteRenderer.enabled = true;
        //    }
        //    else
        //    {
        //        targetAttack.GetComponent<Enemy>().spriteRenderer.enabled = false;
        //    }
        //}

        if (IsHaveTargetAttack() == true)
        {
            lastTargetAttack = targetAttack;
            targetAttack.GetComponent<Enemy>().spriteRenderer.enabled = true;

        }
        if (IsHaveTargetAttack() == false && lastTargetAttack != null)
        {
            lastTargetAttack.GetComponent<Enemy>().spriteRenderer.enabled = false;
        }
    }


    public override void EquippedWeapon(WeaponType wType)
    {
        base.EquippedWeapon(wType);
        Debug.Log("thang canvasWeapon an Select");
        weaponType = wType;


        weaponHandPrefab = LocalDataManager.Instance.UserData.GetWeaponData(weaponType).weaponHand;

        foreach (Transform child in weaponHand)
        {
            Destroy(child.gameObject);
        }

        Instantiate(weaponHandPrefab, weaponHand);
    }

    public override void EquippedSkin(int idListEquiment, string equipmentName)
    {
        base.EquippedSkin(idListEquiment, equipmentName);

        ResetSkin();

        switch (idListEquiment)
        {
            case 0:
                equipmentPrefab = ((HatData)LocalDataManager.Instance.UserData.GetEquipmentData(idListEquiment, equipmentName)).hatPrefab;
                Instantiate(equipmentPrefab, equipmentPosition);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }



    }

    private void ResetSkin()
    {
        foreach (Transform child in equipmentPosition)
        {
            Destroy(child.gameObject);
        }
    }

}
