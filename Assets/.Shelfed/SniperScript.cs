using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScript : MonoBehaviour
{
    private int maxHP = 100;
    private int currentHP = 100;
    private int fullAmmo = 12;
    private int currentAmmo;
    public GameObject Sniper1HUD;
    public Text ammoCounter;
    public Slider HealthBar;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoCounter = Sniper1HUD.GetComponent<Text>();
        currentAmmo = fullAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || currentAmmo == 0)
        {
            currentAmmo--;
            ammoCounter.text = currentAmmo.ToString();
        }
        else if (Input.GetButton("R"))
        {
            currentAmmo = fullAmmo;
            ammoCounter.text = currentAmmo.ToString();
        }
        if (hit == true)
        {
            damageTaken();
        }
    }

    void damageTaken()
    {
        //HealthBar.value = ;
    }
}
