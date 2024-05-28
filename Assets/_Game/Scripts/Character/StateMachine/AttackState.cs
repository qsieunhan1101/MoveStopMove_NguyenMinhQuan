using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        enemy.frameRate = 1;
        enemy.time = 1;
    }

    public void OnExecute(Enemy enemy)
    {
        Debug.Log("Day la AttackState");
        enemy.time += Time.deltaTime;
        if (enemy.IsHaveTargetAttack() == true && enemy.time >= enemy.frameRate)
        {
            Vector3 dir = (enemy.TargetAttack.transform.position - enemy.transform.position).normalized;
            enemy.EnemyGetRotation(dir);
            enemy.time -= enemy.frameRate;
            enemy.ChangeAnim(Cache.Anim_Attack);
            enemy.SpawnWeapon();
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
