using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class PlayerPrefReset : MonoBehaviour
{
    [MenuItem("Tool/Player Prefs Remover")]
    public static void DeletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset PlayerPref thanh cong");
    }
}
