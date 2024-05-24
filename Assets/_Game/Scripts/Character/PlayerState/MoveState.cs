using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    public void OnEnter(Character character)
    {
        
    }

    public void OnExecute(Character character)
    {
        Debug.Log("Day la MoveState");


    }

    public void OnExit(Character character)
    {
        
    }
}
