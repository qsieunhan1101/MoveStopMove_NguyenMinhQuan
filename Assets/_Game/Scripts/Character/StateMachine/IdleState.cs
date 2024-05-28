using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float time;
    float timeIdle;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        time = 0;
        timeIdle = 2f;
        enemy.ChangeAnim(Cache.Anim_Idle);
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time >= timeIdle)
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
