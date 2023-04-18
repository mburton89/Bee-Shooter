using UnityEngine;

public class PulsatingGlow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float glowSpeed = 1f;
    public float glowRange = 0.5f;
    public float fadeSpeed = 1f;

    private float baseIntensity;
    private bool fadingIn = true;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        baseIntensity = spriteRenderer.color.a;
    }

    void Update()
    {
        float glow = Mathf.Sin(Time.time * glowSpeed) * glowRange + glowRange;
        Color color = spriteRenderer.color;
        color.a = baseIntensity * glow;
        spriteRenderer.color = color;

        if (fadingIn)
        {
            if (color.a >= baseIntensity)
            {
                fadingIn = false;
            }
        }
        else
        {
            if (color.a <= 0f)
            {
                fadingIn = true;
            }
        }

        float fadeAmount = Time.deltaTime * fadeSpeed;
        if (fadingIn)
        {
            color.a += fadeAmount;
        }
        else
        {
            color.a -= fadeAmount;
        }
        spriteRenderer.color = color;
    }
}