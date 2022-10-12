using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WazaPanel : MonoBehaviour
{
    [SerializeField] GameObject waza, cd, detail;

    void Start()
    {
        gameObject.transform.position = new Vector2(Input.mousePosition.x - 150, Input.mousePosition.y);
    }

    /// <summary>
    /// マウスをトラッキングして、マウスの側に画面を出す。
    /// </summary>
    void Update()
    {
        float y = Input.mousePosition.y;
        if(y <= 145)
        {
            y = 145;
        }
        gameObject.transform.position = new Vector2(Input.mousePosition.x - 150,y);
    }

    public void Refresh(int id,int wazaID)
    {
        waza.GetComponent<Text>().text = CharaData.GetAbilityName(id,wazaID);
        cd.GetComponent<Text>().text = CharaData.GetCooldown(id, wazaID).ToString();
        detail.GetComponent<Text>().text = DataBase.GetWazaDetail(id, wazaID).ToString();
    }
}
