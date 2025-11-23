using UnityEngine;

public class ColorStrobe : MonoBehaviour
{
    public bool isStrobing = false;
    public float strobeSpeed = 2f;     // Lower = smoother

    private SpriteRenderer sr;
    private Color originalColor;

    // Very soft green overlay (light tint)
    private Color softGreen = new Color(0.5f, 1f, 0.5f, 0.3f);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        if (isStrobing)
        {
            // t oscillates between 0 → 1 → 0 smoothly
            float t = (Mathf.Sin(Time.time * strobeSpeed) + 1f) * 0.5f;

            // Lerp between original color and a soft green tint
            sr.color = Color.Lerp(originalColor, softGreen, t);
        }
        else
        {
            // Reset smoothly to original if needed
            if (sr.color != originalColor)
                sr.color = originalColor;
        }
    }
}
