using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WazaDetail : MonoBehaviour , IPointerEnterHandler ,IPointerExitHandler
{
    [SerializeField] int abilityID;
    [SerializeField] GameObject discriptionPanel;

    /// <summary>
    /// マウスを重ねて詳細画面を表示する処理
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        discriptionPanel.SetActive(true);
        discriptionPanel.GetComponent<WazaPanel>().Refresh(UserData.GetSelectChara(),abilityID);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        discriptionPanel.SetActive(false);
    }
}
