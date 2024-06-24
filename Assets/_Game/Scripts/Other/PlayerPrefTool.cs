using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PlayerPrefTool : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tool/Player Prefs Remover")]
    public static void DeletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Reset PlayerPref thanh cong");
    }
#endif
}
