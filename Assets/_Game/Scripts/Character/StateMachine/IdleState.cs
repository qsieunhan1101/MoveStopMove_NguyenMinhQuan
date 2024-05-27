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
        timeIdle = 1f;
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time >= timeIdle)
        {
            enemy.ChangeState(new MoveState());
        }
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
