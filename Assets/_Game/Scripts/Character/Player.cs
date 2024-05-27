using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    private Vector3 startTouch, currentTouch;
    private Vector3 moveDirection;

    private Vector3 rotationDirection;



    [SerializeField] private float speed;




    [SerializeField] private bool isMoving;






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
    public override void Attack()
    {
        base.Attack();

    }
    protected override void GetTargetOtherCharacter()
    {
        base.GetTargetOtherCharacter();
        ActiveEnemyTargetSprite();
    }

    private void ActiveEnemyTargetSprite()
    {

        if (IsHaveTargetAttack() == true)
        {

            SpriteRenderer targetSprite = targetAttack.GetComponent<Enemy>().spriteRenderer;

            if (Vector3.Distance(this.transform.position, targetAttack.transform.position) <= rangeAttack)
            {
                targetSprite.enabled = true;
            }
            else
            {
                targetSprite.enabled = false;
            }
        }
    }




}
