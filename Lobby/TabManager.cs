using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] GameObject[] tabs;
    private int currentID = -1;

    //音声関係
    [SerializeField] AudioClip clickSE;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 現在のタブを消して、新しいタブを表示する。
    /// </summary>
    public void OpenTab(int tabid)
    {
        if (currentID != tabid)
        {
            HideTab(currentID);
            tabs[tabid].SetActive(true);
            currentID = tabid;
            //AudioData.PlaySE(audioSource, clickSE);
        }
    }
    private void HideTab(int tabid)
    {
        if (tabid == -1) return;
        tabs[tabid].SetActive(false);
    }
}
