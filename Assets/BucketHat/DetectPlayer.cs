using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttons;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        buttons.SetActive(true);
        Debug.Log("Enter");
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerExit(Collider other){
        buttons.SetActive(false);
        Debug.Log("Exit");
        Cursor.lockState = CursorLockMode.Locked;
    }
}
