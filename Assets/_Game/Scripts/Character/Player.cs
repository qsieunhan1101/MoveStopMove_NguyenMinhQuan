using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody _rb;
    private Vector3 startTouch, currentTouch;
    private Vector3 moveDirection;

    private Vector3 rotationDirection;


    [SerializeField] private Transform playerTransform;

    [SerializeField] private float speed;



    [SerializeField] private GameObject bulletPrefab;

    private bool isMoving;


    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform bulletPointDir;

    float angle;

    Vector3 dirBullet;

    float timee;


    [SerializeField] private Collider[] enemyColliderCatchs = new Collider[1];
   

    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Vector3.back;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        GetTargetOtherCharacter();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        timee += Time.deltaTime;
        if (timee >= 2f)
        {
            Attack();
            timee = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(bulletPointDir.position);
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

    protected override void Move()
    {
        base.Move();
        GetDirection();
        GetRotation(rotationDirection);
        _rb.velocity = new Vector3(moveDirection.x * speed, _rb.velocity.y, moveDirection.z * speed);
    }
    private Vector3 GetDirection()
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
                currentTouch = touch.position;
                moveDirection = (currentTouch - startTouch).normalized;
                moveDirection.z = moveDirection.y;
                moveDirection.y = 0;
                rotationDirection = moveDirection;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveDirection = Vector3.zero;

            }
        }
        return moveDirection;

    }

    private Quaternion GetRotation(Vector3 rotation)
    {
        return playerTransform.rotation = Quaternion.LookRotation(rotation);
    }

    public void ChangeState(IState<Character> newState)
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

    // ban ra vien dan theo huong truoc mat character
    public override void Attack()
    {
        //ChangeAnim();
        GameObject b = Instantiate(bulletPrefab);
        b.transform.position = bulletPoint.position;
        Vector3 dir = (bulletPointDir.position - this.transform.position).normalized;
        b.GetComponent<BulletBase>().rb.AddForce(dir * 500);
        int sign = 1;
        if (bulletPointDir.position.x < this.transform.position.x)
        {
            sign = -1;
        }
        angle = Vector3.Angle(bulletPointDir.forward, Vector3.forward) * sign;
        b.transform.rotation = Quaternion.Euler(0, angle, 0);

    }
    protected override void GetTargetOtherCharacter()
    {
        base.GetTargetOtherCharacter();

        int numberHitEnemyCatch = Physics.OverlapSphereNonAlloc(this.transform.position, rangeAttack, enemyColliderCatchs, characterLayerMask);

        if (numberHitEnemyCatch == 0)
        {
            Debug.Log("khong co ke dich nao");
            enemyColliderCatchs = new Collider[1];
        }
        else
        {
            Debug.Log("so luong ke dich" + numberHitEnemyCatch);
            //GetRotation((enemyColliderCatchs[0].transform.position - this.transform.position).normalized);

        }
    }

}
