using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBase : GameUnit
{
    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            OnDespawn();
        }
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
