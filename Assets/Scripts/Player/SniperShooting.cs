using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperShooting : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private int Sniper1Ammo = 5;
    [SerializeField] private int Sniper2Ammo = 5;
    [SerializeField] private int Sniper3Ammo = 5;
    [SerializeField] private GameObject AmmoUIGO;
    [SerializeField] private Camera Camera;
    [SerializeField] private Texture RegularSights;
    [SerializeField] private Texture ZoomSights;
    [SerializeField] private GameObject Crosshair;
    private RawImage CurrentCrosshair;
    public bool IsZoomedIn = false;
    private CharacterSelector CS;
    private int SniperMaxAmmo = 5;
    private Text AmmoUI;
    private GameEnd GE;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CS = Player.GetComponent<CharacterSelector>();
        CurrentCrosshair = Crosshair.GetComponent<RawImage>();
        GE = Player.GetComponent<GameEnd>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CS._characterID != 1 && CS.IsPaused == false)
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1) && (CS._characterID != 1 && CS.IsPaused == false))
        {
            if(IsZoomedIn == false)
            {
                Camera.fieldOfView = 10;
                CrossSwitch();
                IsZoomedIn = true;
            }
            else if(IsZoomedIn == true)
            {
                Camera.fieldOfView = 70;
                CrossSwitch();
                IsZoomedIn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && CS.IsPaused == false)
        {
            if (CS._characterID == 2)
            {
                Sniper1Ammo = SniperMaxAmmo;
                SetAmmo(2);
            }
            else if (CS._characterID == 3)
            {
                Sniper2Ammo = SniperMaxAmmo;
                SetAmmo(3);
            }
            else if (CS._characterID == 4)
            {
                Sniper3Ammo = SniperMaxAmmo;
                SetAmmo(4);
            }
        }
    }

    private void Shoot()
    {
        if (CS._characterID == 2 && (Sniper1Ammo != 0))
        {
            Sniper1Ammo -= 1;
            SetAmmo(2);
            ShootRayHit();
        }

        if (CS._characterID == 3 && (Sniper1Ammo != 0))
        {
            Sniper2Ammo -= 1;
            SetAmmo(3);
            ShootRayHit();
        }

        if (CS._characterID == 4 && (Sniper1Ammo != 0))
        {
            Sniper3Ammo -= 1;
            SetAmmo(4);
            ShootRayHit();
        }
    }

    public void SetAmmo(int ID)
    {
        AmmoUIGO = GameObject.FindGameObjectWithTag("CurrentAmmo");
        AmmoUI = AmmoUIGO.GetComponent<Text>();
        if (ID == 2)
        {
            AmmoUI.text = Sniper1Ammo.ToString();
        }
        else if (ID == 3)
        {
            AmmoUI.text = Sniper2Ammo.ToString();
        }
        else if (ID == 4)
        {
            AmmoUI.text = Sniper3Ammo.ToString();
        }
    }

    private void ShootRayHit()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
                GE.EndGame(0);
            }
        }
    }

    private void CrossSwitch()
    {
        if (IsZoomedIn == false)
        {
            CurrentCrosshair.texture = ZoomSights;
            Crosshair.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (IsZoomedIn == true)
        {
            CurrentCrosshair.texture = RegularSights;
            Crosshair.transform.localScale = new Vector3(0.01f, 0.02f, 1);
        }
    }
}
