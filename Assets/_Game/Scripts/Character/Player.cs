using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform lastTargetAttack;
    [SerializeField] private float speed;
    [SerializeField] private bool isMoving;
    private Vector3 startTouch, currentTouch;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;




    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Vector3.back;
        isMoving = false;

        OnInit();

        characterName = "You";

        //ChangeState(new IdleState(), this);
    }
    private void OnEnable()
    {
        CanvasWeapon.buttonSelectEvent += EquippedWeapon;
        SkinItemUI.itemOnclickEvent += EquippedSkinItemUI;
        CanvasShop.exitShopEvent += EquippedSkinItemUI;
        CanvasVictory.victoryEvent += UpdateGold;
        CanvasFail.failEvent += UpdateGold;
    }

    private void OnDisable()
    {
        CanvasWeapon.buttonSelectEvent -= EquippedWeapon;
        SkinItemUI.itemOnclickEvent -= EquippedSkinItemUI;
        CanvasShop.exitShopEvent -= EquippedSkinItemUI;
        CanvasVictory.victoryEvent -= UpdateGold;
        CanvasFail.failEvent -= UpdateGold;
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
        if (isMoving == false && IsHaveTargetAttack() == true && !isDead)
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

        if (GameManager.Instance.CurrentState == GameState.Gameplay)
        {

            GetTargetOtherCharacter();
        }

        ActiveEnemyTargetSprite();




        if (isMoving)
        {
            anim.ResetTrigger(Constant.Anim_Attack);
            ChangeAnim(Constant.Anim_Run);
            CancelInvoke(nameof(SpawnWeapon));
        }
        if (!isMoving && !isDead)
        {
            ChangeAnim(Constant.Anim_Idle);
        }
    }

    protected override void OnInit()
    {
        base.OnInit();

        isDead = false;
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Character);
        //load data weapon
        this.weaponType = PlayerDataManager.Instance.GetWeaponState();
        EquippedWeapon(this.weaponType);

        EquippedSkinItemUI(PlayerDataManager.Instance.GetEquipmentState().Item1, PlayerDataManager.Instance.GetEquipmentState().Item2);
    }
    protected override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Death()
    {
        base.Death();
        isDead = true;
        ChangeAnim(Constant.Anim_Dead);
        this.gameObject.layer = LayerMask.NameToLayer(Constant.Layer_Default);

        GameManager.Instance.Fail();

        SoundManager.Instance.SpawnAndPlaySound(SoundType.DeadSound_1);

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
        if (GameManager.Instance.GetCurrentState() != GameState.Gameplay)
        {
            return;
        }
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

        SoundManager.Instance.SpawnAndPlaySound(SoundType.WeaponThrowSound);

    }
    protected override void GetTargetOtherCharacter()
    {
        base.GetTargetOtherCharacter();
        //ActiveEnemyTargetSprite();
    }

    private void ActiveEnemyTargetSprite()
    {
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


    public override void EquippedWeapon(WeaponType weaponType)
    {
        base.EquippedWeapon(weaponType);

    }

    public override void EquippedSkinItemUI(int idListEquiment, string equipmentName)
    {
        base.EquippedSkinItemUI(idListEquiment, equipmentName);


    }


    private void UpdateGold()
    {
        int golds = characterScore;
        PlayerDataManager.Instance.playerWeaponData.golds += golds;
        PlayerDataManager.Instance.UpdateDataPlayer();
        Debug.Log(characterScore);
    }


}
