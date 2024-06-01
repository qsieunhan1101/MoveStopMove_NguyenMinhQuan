using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        enemy.fireRate = 1.5f;
        enemy.nextTimeToFire = 1;
    }

    public void OnExecute(Enemy enemy)
    {
        Debug.Log("Day la AttackState");
        if (enemy.IsHaveTargetAttack() == true && enemy.IsDead == false)
        {
            if (Time.time >= enemy.nextTimeToFire)
            {
                enemy.nextTimeToFire = Time.time + enemy.fireRate;
                Vector3 dir = (enemy.TargetAttack.transform.position - enemy.transform.position).normalized;
                enemy.EnemyGetRotation(dir);
                enemy.ChangeAnim(Cache.Anim_Attack);
                enemy.Attack();
                enemy.ChangeAnim(Cache.Anim_Idle);
            }
        }
        if (enemy.IsHaveTargetAttack() == false)
        {
            enemy.ChangeState(new IdleState());
        }

    }

    public void OnExit(Enemy enemy)
    {

    }
}
