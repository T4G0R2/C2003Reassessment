using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interactable
{

    [SerializeField] private Transform _canvas;
    [SerializeField] private GameObject _interrogationHUD;
    [SerializeField] private GameObject _interrogationPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private CharacterSelector CS;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        CS = _player.GetComponent<CharacterSelector>();
    }


    void Update()
    {
        
    }

    protected override void Interact()
    {
        SetupInterrogation();
    }

    private void SetupInterrogation()
    {
        _interrogationHUD = Instantiate(_interrogationPrefab);
        _interrogationHUD.transform.SetParent(_canvas);
        _interrogationHUD.tag = "InterrogationHUD";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CS.IsPaused = true;
        Time.timeScale = 0;
    }
}
