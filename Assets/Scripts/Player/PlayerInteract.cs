using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private InputManager _inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<PlayerLook>()._camera;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerUI.UpdateText(String.Empty);
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;
        
        Debug.DrawRay(ray.origin, ray.direction * _distance);
        
        if (Physics.Raycast(ray, out hit,_distance,_mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(interactable.promptMessage);
                
                if (_inputManager._onFootActions.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
