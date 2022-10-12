using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiyaDummy : MonoBehaviour
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
        yield return new WaitForSeconds(KamiyaData.BAKA_LIVE_SECS);
        Destroy(gameObject);
    }
}
