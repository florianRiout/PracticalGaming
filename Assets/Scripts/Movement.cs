using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float currentSpeed = 1;
    private float jumpForce = 400;
    private float turningSpeed = 100f;
    CameraControl myCamera;
    Animator animatorParameter;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animatorParameter = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<CameraControl>();
        myCamera.Follow(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldWalkForward()) WalkForward();
        if (ShouldTurnLeft()) TurnLeft();
        if (ShouldTurnRight()) TurnRight();
        if (ShouldWalkBackward()) WalkBackward();
        if (ShouldIdle()) Idle();
        if (ShouldJump()) Jump();
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.CompareTag("Ground"))
            animatorParameter.SetBool("isJumping", false);
    }

    private void Jump()
    {
        animatorParameter.SetBool("isJumping", true);
        animatorParameter.SetBool("isWalking", false);
        animatorParameter.SetBool("isWalkingBackward", false);
        rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private bool ShouldJump()
    {
        return Input.GetKey(KeyCode.Space) && !animatorParameter.GetBool("isJumping");
    }

    private void Idle()
    {
        animatorParameter.SetBool("isWalking", false);
        animatorParameter.SetBool("isWalkingBackward", false);
    }

    private bool ShouldIdle()
    {
        return !ShouldWalkForward() && !ShouldWalkBackward();
    }

    private void WalkBackward()
    {
        if (!animatorParameter.GetBool("isJumping"))
            animatorParameter.SetBool("isWalkingBackward", true);
        transform.position -= currentSpeed * transform.forward * Time.deltaTime;
    }

    private bool ShouldWalkBackward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void TurnRight()
    {
        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
        myCamera.xCursor += turningSpeed * Time.deltaTime;
    }

    private bool ShouldTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void TurnLeft()
    {
        transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
        myCamera.xCursor -= turningSpeed * Time.deltaTime;
    }

    private bool ShouldTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void WalkForward()
    {
        if(!animatorParameter.GetBool("isJumping"))
            animatorParameter.SetBool("isWalking", true);
        transform.position += currentSpeed * transform.forward * Time.deltaTime;
    }

    private bool ShouldWalkForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    public void Die()
    {
        transform.Rotate(new Vector3(90, 0, 0));
    }
}
