using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject _negotiatorHUD;
    [SerializeField] private GameObject _sniper1HUD;
    [SerializeField] private GameObject _sniper2HUD;
    [SerializeField] private GameObject _sniper3HUD;
    [SerializeField] private GameObject _selectionIcon;
    [SerializeField] private Transform _canvas;
    private GameObject _negotiator;
    private GameObject _negotiatorPlaceholder;
    private CharacterController _negotioatorController;
    private GameObject _sniper1;
    private GameObject _sniper2;
    private GameObject _sniper3;
    private GameObject _selectionIconObject;
    private GameObject _currentHUD;
    private int _characterID = 1;
    private int _previousID = 1;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHUD(_characterID);
        _negotiator = GameObject.FindGameObjectWithTag("Player");
        _sniper1 = GameObject.FindGameObjectWithTag("Sniper1");
        _sniper2 = GameObject.FindGameObjectWithTag("Sniper2");
        _sniper3 = GameObject.FindGameObjectWithTag("Sniper3");
        _negotioatorController = _negotiator.GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Tab"))
        {
            _selectionIconObject = Instantiate(_selectionIcon);
            _currentHUD.transform.SetParent(_canvas);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetButton("Tab") && _characterID != 1)
        {
            _characterID = 1;
            InstantiateHUD(_characterID);
            SwapPos(_characterID);
            Destroy(_selectionIconObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetButton("Tab") && _characterID != 2)
        {
            _characterID = 2;
            InstantiateHUD(_characterID);
            SwapPos(_characterID);
            Destroy(_selectionIconObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetButton("Tab") && _characterID != 3)
        {
            _characterID = 3;
            InstantiateHUD(_characterID);
            SwapPos(_characterID);
            Destroy(_selectionIconObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Input.GetButton("Tab") && _characterID != 4)
        {
            _characterID = 4;
            InstantiateHUD(_characterID);
            SwapPos(_characterID);
            Destroy(_selectionIconObject);
        }
        if (Input.GetButtonUp("Tab"))
        {
            if (_selectionIconObject != null) Destroy(_selectionIconObject);
        }
    }

    private void InstantiateHUD(int ID)
    {
        if (_currentHUD != null) Destroy(_currentHUD);
        if (ID == 1) _currentHUD = Instantiate(_negotiatorHUD);
        else if (ID == 2) _currentHUD = Instantiate(_sniper1HUD);
        else if (ID == 3) _currentHUD = Instantiate(_sniper2HUD);
        else if (ID == 4) _currentHUD = Instantiate(_sniper3HUD);
        _currentHUD.transform.SetParent(_canvas);
    }

    private void SwapPos(int ID)
    {
        _negotioatorController.enabled = false;
        Vector3 _tempPos = _negotiator.transform.position;
        Vector3 _movePos;
        if (ID == 1)
        {
            if (_previousID == 2)
            {
                _negotiator.transform.position = _sniper1.transform.position;
                _sniper1.transform.position = _tempPos;
                _previousID = 1;
            }
            else if (_previousID == 3)
            {
                _negotiator.transform.position = _sniper2.transform.position;
                _sniper2.transform.position = _tempPos;
                _previousID = 1;
            }
            else if (_previousID == 4)
            {
                _negotiator.transform.position = _sniper3.transform.position;
                _sniper3.transform.position = _tempPos;
                _previousID = 1;
            }
        }
        else if (ID == 2)
        {
            if (_previousID == 1)
            {
                _negotiator.transform.position = _sniper1.transform.position;
                _sniper1.transform.position = _tempPos;
                _previousID = 2;
            }
            else if (_previousID == 3)
            {
                _movePos = _sniper1.transform.position;
                _sniper1.transform.position = _sniper2.transform.position;
                _sniper2.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 2;
            }
            else if (_previousID == 4)
            {
                _movePos = _sniper1.transform.position;
                _sniper1.transform.position = _sniper3.transform.position;
                _sniper3.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 2;
            }
        }
        else if (ID == 3)
        {
            if (_previousID == 1)
            {
                _negotiator.transform.position = _sniper2.transform.position;
                _sniper2.transform.position = _tempPos;
                _previousID = 3;
            }
            else if (_previousID == 2)
            {
                _movePos = _sniper2.transform.position;
                _sniper2.transform.position = _sniper1.transform.position;
                _sniper1.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 3;
            }
            else if (_previousID == 4)
            {
                _movePos = _sniper2.transform.position;
                _sniper2.transform.position = _sniper3.transform.position;
                _sniper3.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 3;
            }
        }
        else if (ID == 4)
        {
            if (_previousID == 1)
            {
                _negotiator.transform.position = _sniper3.transform.position;
                _sniper3.transform.position = _tempPos;
                _previousID = 4;
            }
            else if (_previousID == 2)
            {
                _movePos = _sniper3.transform.position;
                _sniper3.transform.position = _sniper1.transform.position;
                _sniper1.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 4;
            }
            else if (_previousID == 3)
            {
                _movePos = _sniper3.transform.position;
                _sniper3.transform.position = _sniper2.transform.position;
                _sniper2.transform.position = _tempPos;
                _negotiator.transform.position = _movePos;
                _previousID = 4;
            }
        }
        _negotioatorController.enabled = true;
    }
}
