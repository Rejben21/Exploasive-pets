using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rgBody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float moveSpeed = 5;
    public float speedBonus = 1;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer sRUp;
    public AnimatedSpriteRenderer sRDown;
    public AnimatedSpriteRenderer sRLeft;
    public AnimatedSpriteRenderer sRRight;
    public AnimatedSpriteRenderer sRDeath;
    private AnimatedSpriteRenderer activeSR;

    private void Awake()
    {
        rgBody = GetComponent<Rigidbody2D>();
        activeSR = sRDown;
    }

    private void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            if (Input.GetKey(inputUp))
            {
                SetDirection(Vector2.up, sRUp);
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirection(Vector2.down, sRDown);
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.left, sRLeft);
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirection(Vector2.right, sRRight);
            }
            else
            {
                SetDirection(Vector2.zero, activeSR);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rgBody.position;
        Vector2 translation = direction * moveSpeed * Time.deltaTime;

        rgBody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer sR)
    {
        direction = newDirection;

        sRUp.enabled = sR == sRUp;
        sRDown.enabled = sR == sRDown;
        sRLeft.enabled = sR == sRLeft;
        sRRight.enabled = sR == sRRight;

        activeSR = sR;
        activeSR.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        AudioManager.instance.PlaySFX(2);

        enabled = false;
        GetComponent<PlayerBombsController>().enabled = false;

        sRUp.enabled = false;
        sRDown.enabled = false;
        sRLeft.enabled = false;
        sRRight.enabled = false;
        sRDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
