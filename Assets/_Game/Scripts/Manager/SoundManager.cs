using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private bool isMute;
    private Transform tf;
    public Transform Tf
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    private void Start()
    {
        UnMuteAllAudio();
    }

    public void SpawnAndPlaySound(SoundType soundType)
    {
        if (isMute == false)
        {
            SimplePool.Spawn<SoundBase>((PoolType)soundType, Tf.position, Quaternion.identity);
        }
    }

    public void MuteAllAudio()
    {
        AudioListener.volume = 0.0f;
        isMute = true;
    }

    public void UnMuteAllAudio()
    {
        AudioListener.volume = 1.0f;
        isMute = false;
    }
}
