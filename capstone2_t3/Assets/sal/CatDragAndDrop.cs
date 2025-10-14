using UnityEngine;

public class CatDragAndDrop : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private CatHappiness catHappinessRef;
    public GameManager gameManagerRef;

    public bool dragging = false;
    private Collider2D lastHoveredCat = null;

    void Start()
    {
        catHappinessRef = GetComponent<CatHappiness>();
        col = GetComponent<Collider2D>();

        gameManagerRef = FindObjectOfType<GameManager>();
        if (gameManagerRef == null)
        {
            Debug.LogWarning("No GameManager found in the scene!");
        }
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // PICK UP
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            foreach (var hit in hits)
            {
                if (hit.gameObject.tag.Contains("Cat") && hit.gameObject == this.gameObject)
                {
                    dragging = true;
                    startDragPosition = transform.position;

                    Debug.Log("Picked up cat: " + this.gameObject.name);

                    if (gameManagerRef != null)
                    {
                        gameManagerRef.currentlySelectedCat = this.gameObject;
                        gameManagerRef.UpdateCatLikesGUI();
                    }

                    // Mark as being picked up
                    break;
                }
            }
        }

        // DRAGGING
        if (dragging && Input.GetMouseButton(0))
        {
            transform.position = mousePos;
        }

        // DROP
        if (dragging && Input.GetMouseButtonUp(0))
        {
            dragging = false;

            int dropAreaLayer = LayerMask.GetMask("DropArea");
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, dropAreaLayer);

            if (hitCollider != null && hitCollider.TryGetComponent(out ICatDropArea catDropArea))
            {
                catDropArea.OnCatDrop(this, mousePos);
                this.gameObject.tag = "Cat"; // Successfully placed
                catHappinessRef.catPlumbob.SetActive(true);
            }
            else
            {
                transform.position = startDragPosition;
            }

            catHappinessRef.CheckIfAllLikesAreSatisfied();
        }

        // HOVER TRACKING (works even when dragging or dropped)
        UpdateHoveredCat(mousePos);
    }

    private void UpdateHoveredCat(Vector2 mousePos)
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
        Collider2D hoveredCat = null;

        foreach (var hit in hits)
        {
            if (hit.gameObject.tag.Contains("Cat"))
            {
                hoveredCat = hit;
                break; // only first cat
            }
        }

        if (hoveredCat != lastHoveredCat)
        {
            // Clear previous highlight
            if (lastHoveredCat != null && lastHoveredCat.TryGetComponent<SpriteRenderer>(out var sr1))
            {
                sr1.color = Color.white;
            }

            lastHoveredCat = hoveredCat;

            // Highlight new cat and update selection
            if (hoveredCat != null)
            {
                if (hoveredCat.TryGetComponent<SpriteRenderer>(out var sr2))
                    sr2.color = Color.green;

                if (!dragging && gameManagerRef != null)
                {
                    gameManagerRef.currentlySelectedCat = hoveredCat.gameObject;
                    gameManagerRef.UpdateCatLikesGUI();
                }
            }
            else
            {
                if (!dragging && gameManagerRef != null)
                {
                    gameManagerRef.currentlySelectedCat = null;
                    gameManagerRef.UpdateCatLikesGUI();
                }
            }
        }
    }
}
