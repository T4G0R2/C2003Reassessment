using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    public float distance = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distance)==true)
            {
                if (hit.collider.TryGetComponent(out IInteeraction interaction)==true)
                {
                    interaction.Activate();
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.green, 0.1f);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.yellow, 0.1f);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * distance, Color.red, 0.1f);
            }
        }
    }
}

public interface IInteeraction
{
    void Activate();
}