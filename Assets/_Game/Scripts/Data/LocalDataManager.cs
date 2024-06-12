using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataManager : Singleton<LocalDataManager>
{
    [SerializeField] private UserData userData;
    public UserData UserData => userData;
}
