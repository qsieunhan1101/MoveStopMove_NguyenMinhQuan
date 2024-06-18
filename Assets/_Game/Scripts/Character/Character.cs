using TMPro;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected CharacterState charState;
    [SerializeField] protected Transform targetAttack;
    [SerializeField] protected Collider[] enemyColliders;
    protected Vector3 bulletDirection;
    public LayerMask characterLayerMask;
    public Transform TargetAttack => targetAttack;


    //Weapon
    [Header ("Weapon")]
    [SerializeField] protected Transform bulletPoint;
    [SerializeField] protected Transform bulletPointDir;
    [SerializeField] protected float rangeAttack;
    protected float angle;
    private float rangeAttackDefault;
    public float fireRate = 1f;
    public float nextTimeToFire = 0f;

    [Header ("Weapon_Skin")]
    [SerializeField] protected Transform weaponHand;
    [SerializeField] protected GameObject weaponHandPrefab;
    [SerializeField] public WeaponType weaponType;


    [Header ("Body_Anim")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected string currentAnimName;
    [SerializeField] public Transform bodyTransform;
    protected Vector3 originalScale;

    //Text Name, Score
    [Header ("Character_UI")]
    [SerializeField] protected TextMeshPro characterTextName;
    [SerializeField] protected TextMeshPro characterTextScore;
    public int characterScore = 0;

    //pool
    [Header ("Pool")]
    [SerializeField] private BulletBase bulletBasePrefab;


    //
    [Header("Skin")]
    //Hat
    [SerializeField] private int idListEquipment = 0;
    [SerializeField] private string equipmentName;
    [SerializeField] protected GameObject hatPrefab;
    [SerializeField] protected Transform hatPosition;
    //Pant
    [SerializeField] protected SkinnedMeshRenderer pantMeshRenderer;
    [SerializeField] protected Material originPantMaterial;
    [SerializeField] protected Material pantEquippedMaterial;
    //Shield
    [SerializeField] protected GameObject shieldPrefabs;
    [SerializeField] protected Transform shieldPosition;
    //Set
    [SerializeField] protected SkinnedMeshRenderer setMeshRenderer;
    [SerializeField] protected Material setMaterial;
    [SerializeField] protected Material originSetMaterial;
    [SerializeField] protected GameObject setPrefabs;
    [SerializeField] protected Transform setPosition;



    private bool isMaxSize = false;

    protected virtual void OnInit()/////////////////
    {
        enemyColliders = new Collider[10];
        originalScale = transform.localScale;
        rangeAttackDefault = rangeAttack;

    }

    protected virtual void OnDespawn()//////////////////
    {

    }
    public virtual void ChangeAnim(string animName)///////////////
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }

    }
    public virtual void SpawnWeapon()
    {

        BulletBase bb = SimplePool.Spawn<BulletBase>((PoolType)weaponType, bulletPoint.position, bulletPoint.rotation);
        bb.SetCharacterOwner(this);

        Vector3 dir = (bulletPointDir.position - this.transform.position).normalized;
        
        bb.dir = dir;
        int sign = 1;
        if (bulletPointDir.position.x < this.transform.position.x)
        {
            sign = -1;
        }
        angle = Vector3.Angle(bulletPointDir.forward, Vector3.forward) * sign;
        bb.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Attack()
    {

        ChangeAnim(Constant.Anim_Attack);
        Invoke(nameof(SpawnWeapon), 0.5f);

    }

    void Wait()
    {

    }

    public virtual void Death()/////////////////
    {

    }
    protected virtual void GetTargetOtherCharacter()
    {
       
        int numberHitEnemyCatch = Physics.OverlapSphereNonAlloc(this.transform.position, rangeAttack, enemyColliders, characterLayerMask);
        float closestDistance = Mathf.Infinity;

        Transform lastTarget;

        for (int i = 0; i < numberHitEnemyCatch; i++)
        {

            float distance = Vector3.Distance(enemyColliders[i].transform.position, this.transform.position);
            if (distance < closestDistance && enemyColliders[i].transform != this.transform)
            {
                if (targetAttack != null && this is Player)
                {
                    lastTarget = targetAttack;
                    lastTarget.GetComponent<Enemy>().spriteRenderer.enabled = false;

                }
                targetAttack = enemyColliders[i].transform;
                closestDistance = distance;

            }
            if (numberHitEnemyCatch == 1)
            {
                targetAttack = null;
            }
        }

    }

    public virtual Quaternion GetRotation(Vector3 rotation)
    {
        return bodyTransform.rotation = Quaternion.LookRotation(rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, rangeAttack);
    }

    public virtual bool IsHaveTargetAttack()
    {
        if (targetAttack != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void SetScore()
    {
        characterScore++;
        characterTextScore.text = characterScore.ToString();
        //UpdateSize(characterScore);
    }
    public virtual void UpdateSize(int scorePoint)
    {
        if (scorePoint >= 2 && scorePoint < 5)
        {
            transform.localScale = originalScale * 1.05f;
            rangeAttack = rangeAttackDefault * 1.05f;
        }
        if (scorePoint >= 5 && scorePoint < 10)
        {
            transform.localScale = originalScale * 1.1f;
            rangeAttack = rangeAttackDefault * 1.1f;

        }
        if (scorePoint >= 10 && scorePoint < 25)
        {
            transform.localScale = originalScale * 1.15f;
            rangeAttack = rangeAttackDefault * 1.15f;

        }
        if (scorePoint >= 25 && isMaxSize == false)
        {
            transform.localScale = originalScale * 1.2f;
            rangeAttack = rangeAttackDefault * 1.2f;
            isMaxSize = true;
        }
    }

    public virtual void EquippedWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;
        weaponHandPrefab = LocalDataManager.Instance.UserData.GetWeaponData(this.weaponType).weaponHand;

        foreach (Transform child in weaponHand)
        {
            Destroy(child.gameObject);
        }

        Instantiate(weaponHandPrefab, weaponHand);
    }

    public virtual void EquippedSkinItemUI(int idListEquiment, string equipmentName)
    {
        ResetSkin();
        if (equipmentName == Constant.Default_EquipmentName)
        {
            return;
        }

        UserData userData = LocalDataManager.Instance.UserData;

        switch (idListEquiment)
        {
            case 0:
                hatPrefab = ((HatData)userData.GetEquipmentData(idListEquiment, equipmentName)).hatPrefab;
                Instantiate(hatPrefab, hatPosition);
                break;
            case 1:
                pantEquippedMaterial = ((PantData)userData.GetEquipmentData(idListEquiment, equipmentName)).pantMaterial;
                pantMeshRenderer.material = pantEquippedMaterial;
                break;
            case 2:
                shieldPrefabs = ((ShieldData)userData.GetEquipmentData(idListEquiment, equipmentName)).shieldPrefab;
                Instantiate(shieldPrefabs, shieldPosition);
                break;
            case 3:
                SkinData skinData = ((SkinData)userData.GetEquipmentData(idListEquiment, equipmentName));
                pantEquippedMaterial = skinData.pantMaterial;
                pantMeshRenderer.material = pantEquippedMaterial;
                setMaterial = skinData.skinMaterial;
                setMeshRenderer.material = setMaterial;
                setPrefabs = skinData.skinPrefab;
                Instantiate(setPrefabs, setPosition);
                break;
        }

    }

    protected virtual void ResetSkin()
    {
        foreach (Transform child in hatPosition)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in shieldPosition)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in setPosition)
        {
            Destroy(child.gameObject);
        }

        pantMeshRenderer.material = originPantMaterial;
        setMeshRenderer.material = originSetMaterial;
    }
}
