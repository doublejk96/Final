using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (null == instance)
                {
                    Debug.LogError("SoundManager Not Found");
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private AudioSource[] audioList;

    void Start()
    {
        audioList = GetComponentsInChildren<AudioSource>();
    }

    public void Play(string soundName)
    {
        foreach (AudioSource audio in audioList)
        {
            if (audio.name == soundName)
            {
                audio.Play();
            }
        }
    }
}
