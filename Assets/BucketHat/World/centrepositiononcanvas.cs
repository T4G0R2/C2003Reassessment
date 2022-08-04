using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centrepositiononcanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    void Start()
    {
        float widthdiv2 = canvas.GetComponent<RectTransform>().rect.width / 2;
        transform.position = new Vector3 (widthdiv2,50,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
