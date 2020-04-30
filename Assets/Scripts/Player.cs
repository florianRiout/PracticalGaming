using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : HealthSystem
{
    private float currentSpeed = 4;
    private float jumpForce = 400;
    private float turningSpeed = 100f;
    CameraControl myCamera;
    Animator animatorParameter;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        HealthPerSec = 40f;
        MaxHealth = 1000;
        CurrentHealth = 50;
        animatorParameter = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        myCamera = FindObjectOfType<CameraControl>();
        myCamera.Follow(this);
        this.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
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

        CurrentHealth += HealthPerSec * Time.deltaTime;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            HealthPerSec = 0;
            Die();
        }

        if (ShouldUsePotion()) UsePotion();
    }

    private bool ShouldUsePotion()
    {
        return Input.GetKeyDown(KeyCode.P);
    }

    private void UsePotion()
    {
        if(GameManager.Potions.Count > 0)
        {
            Potion p = (Potion)GameManager.Potions[0];
            p.Use();
            GameManager.Potions.Remove(p);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.CompareTag("Ground"))
            animatorParameter.SetBool("isJumping", false);

        if (c.collider.gameObject.CompareTag("Enemy"))
        {
            CurrentHealth -= 100;
        }

        if (c.collider.gameObject.CompareTag("Building") || c.collider.gameObject.TryGetComponent<IInteractable>(out _))
        {
            GameManager.Message.text = "press E to interact";
        }
    }

    void OnCollisionStay(Collision c)
    {
        if(c.collider.gameObject.CompareTag("Building"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                c.gameObject.GetComponentInParent<Building>().Interact();
            }
        }
        else if(c.collider.gameObject.TryGetComponent<IInteractable>(out _))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                c.gameObject.GetComponentInParent<IInteractable>().Interact();
            }
        }
    }

    void OnCollisionExit(Collision c)
    {
        if(c.collider.gameObject.CompareTag("Building") || c.collider.gameObject.TryGetComponent<IInteractable>(out _))
        {
            GameManager.Message.text = "";
        }
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
        return Input.GetKeyDown(KeyCode.Space) && !animatorParameter.GetBool("isJumping");
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
        transform.position -= currentSpeed / 2 * transform.forward * Time.deltaTime;
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
        if (!animatorParameter.GetBool("isJumping"))
            animatorParameter.SetBool("isWalking", true);
        transform.position += currentSpeed * transform.forward * Time.deltaTime;
    }

    private bool ShouldWalkForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    public void Die()
    {
        animatorParameter.SetTrigger("die");
        Time.timeScale = 0;
        GameManager.Death.SetActive(true);
    }

    public void Respawn()
    {
        animatorParameter.ResetTrigger("die");
        transform.position = GameManager.Spawn.transform.position;
        GameManager.Death.SetActive(false);
        Time.timeScale = 1;
    }
}
