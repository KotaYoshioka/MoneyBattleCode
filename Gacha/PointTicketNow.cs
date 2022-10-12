using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTicketNow : MonoBehaviour
{
    public Text point, ticket;
    // Start is called before the first frame update
    void Start()
    {
        point.text = DataBase.GetPoint().ToString();
        ticket.text = DataBase.GetTicket().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
