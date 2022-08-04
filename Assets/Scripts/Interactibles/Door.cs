using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Door : Interactable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private bool isDoorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        doorAnimator = GetComponent<Animator>();
        if (isDoorOpen == false)
        {
            doorAnimator.SetTrigger("trOpen");
            isDoorOpen = true;
        }
        else if (isDoorOpen == true)
        {
            doorAnimator.SetTrigger("trClose");
            isDoorOpen = false;
        }

    }
}
