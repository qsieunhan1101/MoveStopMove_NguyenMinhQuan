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
        enemy.ChangeAnim(Cache.Anim_Run);
        time = 0f;
        randomTime = Random.Range(3,5);
        target = enemy.SetAgentDestination();
        enemy.Move(target);
        enemy.bodyTransform.transform.rotation = Quaternion.LookRotation((target - enemy.transform.position).normalized);
    }

    public void OnExecute(Enemy enemy)
    {
        Debug.Log("Day la MoveState");
        enemy.transform.rotation = Quaternion.identity;

        time += Time.deltaTime;

        if (time >= randomTime)
        {
            enemy.ChangeState(new IdleState());
            time = 0;
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
