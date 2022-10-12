using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinzardDummy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(NinzardData.DECOI_INVISIBLE_SECS);
        Destroy(gameObject);
    }
}
