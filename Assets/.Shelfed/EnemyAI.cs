using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private int maxHP = 100;
    private int currentHP = 100;
    bool ongaurd = false;
    bool offensive = false;
    
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (infront())
        {
            ongaurd = true;
        }
        if (infront() && onsight())
        {
            offensive = true;
        }

      
        
    }

    bool infront()
    {
        Vector3 directionofplayer = transform.position - player.position;
        float angle = Vector3.Angle(transform.forward, directionofplayer);

        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
            return true;
        }
        return false;
    }

    bool onsight()
    {
        Vector3 directionofplayer = player.position - transform.position;

        if (Physics.Raycast(transform.position, directionofplayer, out RaycastHit hit, 50000f))
        {
            if (hit.transform.name == "Player")
            {
                Debug.DrawLine(transform.position, player.position, Color.green);
                return true;
            }
        }
        return false;

    }
}

