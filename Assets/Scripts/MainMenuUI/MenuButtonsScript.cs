using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{

    [SerializeField] private Transform _canvas;
    [SerializeField] private GameObject _controlsPrefab;
    [SerializeField] private GameObject _controlsHUD;

    public void StartGame()
    {
        Cursor.visible = false;
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
}
