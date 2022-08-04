using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : Interactable
{
    public Material[] material;

    private Renderer rend;

    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        Debug.Log("interacted with" + gameObject.name);
        
        if (i < material.Length){
            rend.sharedMaterial = material[i];
            Debug.Log("Flag 1, Material Changed");
            i++ ;
        }
        else if (i >= material.Length)
        {
            i = 0;
            rend.sharedMaterial = material[i];
            Debug.Log("Flag 2, Material Changed, i looped to 0");
            i++;
        }

    }
}
