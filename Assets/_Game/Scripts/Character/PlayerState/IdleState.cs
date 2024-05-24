using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character character)
    {
    }

    public void OnExecute(Character character)
    {
        Debug.Log("Day la IdleState");
        

    }

    public void OnExit(Character character)
    {
        
    }
}
