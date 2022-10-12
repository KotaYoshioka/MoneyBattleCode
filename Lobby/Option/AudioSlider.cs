using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オプションのスライダー
/// スライダーの量に応じてBGM、SEを調整する。
/// </summary>
public class AudioSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] bool isBGM;
    
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = isBGM ? AudioData.GetBGMVolume() : AudioData.GetSEVolume();
    }

    public void OnValueChange()
    {
        if (isBGM) AudioData.SetBGMVolume(slider.value);
        else AudioData.SetSEVolume(slider.value);
    }
}
