using UnityEngine;

public class Player : MonoBehaviour
{
    private float currentSpeed = 1;
    private float jumpForce = 400;
    private float turningSpeed = 100f;
    CameraControl myCamera;
    Animator animatorParameter;
    private new Rigidbody rigidbody;

    public static int MaxHealth { get; set; }
    public static float CurrentHealth { get; set; }
    public static float HealthPerSec { get; set; }

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
            CurrentHealth = MaxHealth;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.CompareTag("Ground"))
            animatorParameter.SetBool("isJumping", false);

        if (c.collider.gameObject.CompareTag("Enemy"))
            CurrentHealth -= 100;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            HealthPerSec = 0;
            Die();
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
        GameManager.Death.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Respawn()
    {
        Start();
        Time.timeScale = 1;
        animatorParameter.ResetTrigger("die");
        GameManager.Death.gameObject.SetActive(false);
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }
}
