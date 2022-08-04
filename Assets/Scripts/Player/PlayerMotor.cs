using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController _controller;
    public bool isGrounded;
    private Vector3 playerVelocity;
    public float gravity = -9.8f;

    private float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = _controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        _controller.Move(transform.TransformDirection(moveDirection * speed * Time.deltaTime));

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1;
        }
        _controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
        
        
    }
}
