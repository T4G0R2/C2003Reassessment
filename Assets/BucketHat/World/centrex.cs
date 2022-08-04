using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centrex : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    void Start()
    {
        float widthdiv2 = canvas.GetComponent<RectTransform>().rect.width / 2;
        transform.position = new Vector3 (widthdiv2,transform.position.y,0);
    }

    // Update is called once per frame
    void Update()
    {
        float widthdiv2 = canvas.GetComponent<RectTransform>().rect.width / 2;
        transform.position = new Vector3 (widthdiv2,transform.position.y,0);
    }
}
