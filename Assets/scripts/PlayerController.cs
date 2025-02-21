using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpForce = 7f;
    public GameObject idlePrefab;
    public GameObject walkPrefab;
    public GameObject runPrefab;
    public GameObject jumpPrefab;

    private Rigidbody2D rb;
    private GameObject currentAnimation;
    private bool isGrounded;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayAnimation(idlePrefab);
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        float speed = isRunning ? runSpeed : walkSpeed;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Yön kontrolü
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();

        // Animasyon kontrolü
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayAnimation(jumpPrefab);
            isGrounded = false;
        }
        else if (!isGrounded)
        {
            PlayAnimation(jumpPrefab);
        }
        else if (moveInput != 0)
        {
            PlayAnimation(isRunning ? runPrefab : walkPrefab);
        }
        else
        {
            PlayAnimation(idlePrefab);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void PlayAnimation(GameObject animationPrefab)
    {
        if (currentAnimation?.name != animationPrefab.name + "(Clone)")
        {
            if (currentAnimation != null)
                Destroy(currentAnimation);

            currentAnimation = Instantiate(animationPrefab, transform.position, Quaternion.identity);
            currentAnimation.transform.SetParent(transform);
            currentAnimation.transform.localPosition = Vector3.zero;
            currentAnimation.transform.localScale = Vector3.one;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}