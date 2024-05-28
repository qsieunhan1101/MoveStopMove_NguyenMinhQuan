using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    private Vector3 startTouch, currentTouch;
    private Vector3 moveDirection;

    private Vector3 rotationDirection;



    [SerializeField] private float speed;




    [SerializeField] private bool isMoving;


    [SerializeField] private Transform lastTargetAttack;




    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Vector3.back;
        isMoving = false;

        OnInit();

        //ChangeState(new IdleState(), this);
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        //attack
        if (isMoving == false && IsHaveTargetAttack())
        {
            time += Time.deltaTime;
            if (time >= frameRate)
            {
                Vector3 dir = (targetAttack.transform.position - this.transform.position).normalized;
                GetRotation(dir);
                time -= frameRate;
                Attack();
            }
        }
        if (isMoving == true && !IsHaveTargetAttack())
        {
            time = frameRate;
        }


        GetTargetOtherCharacter();

        ActiveEnemyTargetSprite();



        if (Input.GetKeyDown(KeyCode.N))
        {
            Attack();
        }




        if (isMoving)
        {
            anim.ResetTrigger(Cache.Anim_Attack);
            ChangeAnim(Cache.Anim_Run);
            CancelInvoke(nameof(SpawnWeapon));
        }
        if (!isMoving)
        {
            ChangeAnim(Cache.Anim_Idle);
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
    }
    protected override void OnDespawn()
    {
        base.OnDespawn();
    }

    private void Move()
    {
        GetTouchDirection();
        if (isMoving == true)
        {
            GetRotation(rotationDirection);
        }
        _rb.velocity = new Vector3(moveDirection.x * speed, _rb.velocity.y, moveDirection.z * speed);
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
        /*        if (IsHaveTargetAttack() == false && lastTargetAttack != null)
                {
                    lastTargetAttack.GetComponent<Enemy>().spriteRenderer.enabled = false;
                }*/
    }




}
