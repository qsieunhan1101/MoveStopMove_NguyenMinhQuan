using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Character character)
    {
        
    }

    public void OnExecute(Character character)
    {
        Debug.Log("Day la AttackState");
    }

    public void OnExit(Character character)
    {
        
    }
}
