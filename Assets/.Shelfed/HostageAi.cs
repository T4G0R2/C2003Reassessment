using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageAI : MonoBehaviour
{
    private int maxHP = 100;
    private int currentHP = 100;
    bool attention = false;
    Transform user;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (infront() && onsight())
        {
            attention = true;
        }

      

    }

    bool infront()
    {
        Vector3 directionofplayer = transform.position - user.position;
        float angle = Vector3.Angle(transform.forward, directionofplayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, user.position, Color.red);
            return true;
        }
        return false;
    }

    bool onsight()
    {
        Vector3 directionofplayer = user.position - transform.position;

        if (Physics.Raycast(transform.position, directionofplayer, out RaycastHit hit, 50000f))
        {
            if (hit.transform.name == "Player")
            {
                Debug.DrawLine(transform.position, user.position, Color.green);
                return true;
            }
        }
        return false;

    }
}
