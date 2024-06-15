using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    float time;
    float randomTime;
    Vector3 target;
    public void OnEnter(Enemy enemy)
    {
        time = 0f;
        randomTime = Random.Range(3,5);
        //enemy.bodyTransform.rotation = Quaternion.identity;
        enemy.ChangeAnim(Constant.Anim_Run);
        target = enemy.SetAgentDestination();
        enemy.Move(target);
        enemy.bodyTransform.transform.rotation = Quaternion.LookRotation((target - enemy.transform.position).normalized);
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.transform.rotation = Quaternion.identity;

        time += Time.deltaTime;

        if (time >= randomTime)
        {
            enemy.ChangeState(new IdleState());
            time = 0;
        }

        if (enemy.IsHaveTargetAttack() == true && (randomTime - time) < 0.5f)
        {
            
            enemy.ChangeState(new AttackState());
        }


    }

    public void OnExit(Enemy enemy)
    {

    }
}
