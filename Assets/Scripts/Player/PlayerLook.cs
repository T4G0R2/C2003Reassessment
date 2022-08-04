using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera _camera;

    [SerializeField] private float _xRotataion = 0f, _xSensitivity = 30f, _ySensitivity = 30f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x, mouseY = input.y;

        _xRotataion -= (mouseY * Time.deltaTime) * _ySensitivity;
        _xRotataion = Mathf.Clamp(_xRotataion, -80, 80);
        _camera.transform.localRotation = quaternion.Euler(_xRotataion,0,0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * _xSensitivity);

    }
}
