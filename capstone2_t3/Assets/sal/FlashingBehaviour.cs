using UnityEngine;
using System.Collections;

public class FlashingBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flashInterval = 0.2f;
    public float initialDelay = 1f;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FlashSprite());
    }

    private IEnumerator FlashSprite()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashInterval);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
