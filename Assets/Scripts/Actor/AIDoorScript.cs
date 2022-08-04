using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoorScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Debug.Log("AI collided");
        }
    }
}
