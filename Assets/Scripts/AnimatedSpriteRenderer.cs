using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer sR;

    public Sprite idleSprite;
    public Sprite[] animationSprites;

    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sR.enabled = true;
    }

    private void OnDisable()
    {
        sR.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;

        if(loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if(idle)
        {
            sR.sprite = idleSprite;
        }
        else if(animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            sR.sprite = animationSprites[animationFrame];
        }
    }
}
