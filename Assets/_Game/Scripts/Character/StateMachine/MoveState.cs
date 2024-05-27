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
        target = enemy.SetAgentDestination();
    }

    public void OnExecute(Enemy enemy)
    {
        Debug.Log("Day la MoveState");

        time += Time.deltaTime;

        if (time >= randomTime)
        {
            enemy.ChangeState(new MoveState());
            time = 0;
        }

        if (enemy.IsHaveTargetAttack())
        {
            enemy.ChangeState(new AttackState());
        }

        enemy.Move(target);

    }

    public void OnExit(Enemy enemy)
    {

    }
}
