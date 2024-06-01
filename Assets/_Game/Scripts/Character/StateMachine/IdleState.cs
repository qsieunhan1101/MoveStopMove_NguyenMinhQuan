using UnityEngine;

public class IdleState : IState
{
    float time;
    float timeIdle;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        time = 0;
        timeIdle = 2.5f;

        if (enemy.IsDead == false)
        {
            enemy.ChangeAnim(Cache.Anim_Idle);
        }
        else
        {
            enemy.ChangeAnim(Cache.Anim_Dead);

        }
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time >= timeIdle && enemy.IsDead == false)
        {
            enemy.ChangeState(new MoveState());
        }

        if (enemy.IsHaveTargetAttack() == true)
        {
            enemy.ChangeState(new AttackState());
        }

    }

    public void OnExit(Enemy enemy)
    {

    }
}
