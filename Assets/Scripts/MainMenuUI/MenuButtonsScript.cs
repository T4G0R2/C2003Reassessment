using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{

    [SerializeField] private Transform _canvas;
    [SerializeField] private GameObject _controlsPrefab;
    [SerializeField] private GameObject _controlsHUD;
    [SerializeField] private GameObject _verificationHUD;
    [SerializeField] private GameObject _player;
    private CharacterSelector _charSel;

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        _controlsHUD = Instantiate(_controlsPrefab);
        _controlsHUD.transform.SetParent(_canvas);
        _controlsHUD.tag = "ControlsHUD";
    }

    public void Back()
    {
        _controlsHUD = GameObject.FindGameObjectWithTag("ControlsHUD");
        Destroy(_controlsHUD);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _charSel = _player.GetComponent<CharacterSelector>();
        Destroy(_charSel._pauseMenu);
        _charSel.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        _verificationHUD.SetActive(true);
    }

    public void Yes()
    {
        SceneManager.LoadScene(0);
    }

    public void No()
    {
        _verificationHUD = GameObject.FindGameObjectWithTag("VerificationHUD");
        _verificationHUD.SetActive(false);
    }
}
