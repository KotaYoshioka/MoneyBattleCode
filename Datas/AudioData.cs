using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour
{
    //BGM、SEの音量
    private static float BGMVolume = 0.5f;
    private static float SEVolume = 0.25f;

    public static void PlayBGM(AudioSource audioSource,AudioClip audioClip)
    {
        audioSource.volume = BGMVolume;
        audioSource.PlayOneShot(audioClip);
    }

    public static void PlaySE(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.volume = SEVolume;
        audioSource.PlayOneShot(audioClip);
    }

    //Getter Setter
    public static float GetBGMVolume()
    {
        return BGMVolume;
    }

    public static void SetBGMVolume(float BGMVolume)
    {
        AudioData.BGMVolume = BGMVolume;
    }

    public static float GetSEVolume()
    {
        return SEVolume;
    }

    public static void SetSEVolume(float SEVolume)
    {
        AudioData.SEVolume = SEVolume;
    }
}
