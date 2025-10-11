using UnityEngine;

public class CatDragAndDrop : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private CatHappiness catHappinessRef;
    public GameManager gameManagerRef;

    public bool dragging = false;

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

        // Pick up the cat
        if (Input.GetMouseButtonDown(0))
        {
            int catLayer = LayerMask.GetMask("Cat");
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, catLayer);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                dragging = true;
                startDragPosition = transform.position;

                Debug.Log("Picked up cat: " + this.gameObject.name);

                // if (gameManagerRef != null)
                // {
                //     gameManagerRef.currentlySelectedCat = this.gameObject;
                //     gameManagerRef.UpdateCatLikesGUI();
                // }
            }
        }
        if (Input.GetMouseButton(0))
        {
            int catLayer = LayerMask.GetMask("Cat");
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, catLayer);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                if (gameManagerRef != null)
                {
                    gameManagerRef.currentlySelectedCat = this.gameObject;
                    gameManagerRef.UpdateCatLikesGUI();
                }
            }
        }

        // Dragging
        if (dragging && Input.GetMouseButton(0))
        {
            transform.position = mousePos;
        }

        // Drop the cat
        if (dragging && Input.GetMouseButtonUp(0))
        {
            dragging = false;

            // Check drop areas
            int dropAreaLayer = LayerMask.GetMask("DropArea");
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, dropAreaLayer);

            if (hitCollider != null && hitCollider.TryGetComponent(out ICatDropArea catDropArea))
            {
                catDropArea.OnCatDrop(this, mousePos);
            }
            else
            {
                // No drop area, return to start
                transform.position = startDragPosition;
            }

            // Check likes
            catHappinessRef.CheckIfAllLikesAreSatisfied();
        }
    }

    private void OnMouseEnter()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get all colliders under the mouse
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos);

        foreach (Collider2D c in colliders)
        {
            if (c.CompareTag("Cat"))
            {
                Debug.Log("Hovering over cat: " + c.gameObject.name);

                // if (gameManagerRef != null)
                // {
                //     gameManagerRef.currentlySelectedCat = c.gameObject;
                //     gameManagerRef.UpdateCatLikesGUI();

                //     // Optional: highlight
                //     c.GetComponent<SpriteRenderer>().color = Color.green;
                // }
            }
        }
    }

    private void OnMouseExit()
    {
        // Only clear selection if we are not dragging this cat
        // if (!dragging)
        // {
        //     if (gameManagerRef != null)
        //     {
        //         gameManagerRef.currentlySelectedCat = null;
        //         gameManagerRef.UpdateCatLikesGUI();
        //     }
        // }
    }

    // public Vector3 GetMousePositionInWorldSpace()
    // {
    //     Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     p.z = 0f;
    //     return p;
    // }
}
