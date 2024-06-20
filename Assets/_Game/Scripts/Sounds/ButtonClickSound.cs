using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private Button btnOnclick;
    private void Awake()
    {
        btnOnclick.onClick.AddListener(OnClickSound);
      
    }

    public void OnClickSound()
    {
        SoundManager.Instance.SpawnAndPlaySound(SoundType.ButtonSound);
    }
}
