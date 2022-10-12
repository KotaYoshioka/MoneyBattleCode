using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCamera : MonoBehaviour
{
    GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        float y = target.transform.position.y;
        if(y <= 0)
        {
            y = 0;
        }
        if (y >= 4.56f)
        {
            y = 4.56f;
        }
        Vector3 position = new Vector3(target.transform.position.x, y, -10);
        gameObject.transform.position = position;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
