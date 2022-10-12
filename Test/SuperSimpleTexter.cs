using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperSimpleTexter : MonoBehaviour
{
    Text myText;
    private void Awake()
    {
        DataBase.Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        myText.text = DataBase.GetWin().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        myText.text = DataBase.GetWin().ToString();
        DataBase.Save();
    }
}
