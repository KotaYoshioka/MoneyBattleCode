using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOrTwoMemory : MonoBehaviour
{
    private int a = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int AGet()
    {
        return a;
    }

    public void ASet(int b)
    {
        a = b;
    }
}
