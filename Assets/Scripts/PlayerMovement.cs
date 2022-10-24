using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    private Vector2 moveDirection;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    public float dashingCooldown = 2f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Slider slider;
    private float fillTime = 0f;
    public float fillValue = 0.45f;
    void Update() //Player Inputs
    {
        if (!canDash)
            UpdateDashBar();
        if (isDashing)
            return;

        ProcessInputs();

        if (Input.GetKeyDown(KeyCode.Space) && moveDirection != Vector2.zero && canDash)
        {
            slider.value = 0;
            StartCoroutine(Dash());
        }

        Flip(rb.velocity.x);
    }

    void FixedUpdate() //Physics Calculation
    {
        if (isDashing)
            return;

        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;//Movement direction
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);//Movement apply
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
            sr.flipX = true;
        else if (_velocity < -0.1f)
            sr.flipX = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashingPower, moveDirection.y * dashingPower);//Dash
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);//0.2f for the dashing time
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);//2f for the cooldown
        fillTime = 0f;
        canDash = true;
    }

    void UpdateDashBar()//Update DashBar with time
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);
        fillTime += fillValue * Time.deltaTime;
    }
}
