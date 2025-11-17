using UnityEngine;

public class CatDragAndDrop : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private CatHappiness catHappinessRef;
    public GameManager gameManagerRef;

    public bool dragging = false;
    private Collider2D lastHoveredCat = null;

    //public Sprite idleCat;
    //public Sprite holdingCat; 
    public SpriteRenderer currentSprite;
    Animator anim;

    public CatType catType;

    void Start()
    {
        catHappinessRef = GetComponent<CatHappiness>();
        col = GetComponent<Collider2D>();

        gameManagerRef = FindObjectOfType<GameManager>();
        if (gameManagerRef == null)
        {
            Debug.LogWarning("No GameManager found in the scene!");
        }

        currentSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // PICK UP
        if (Input.GetMouseButtonDown(0) && gameManagerRef.levelFinished == false)
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            foreach (var hit in hits)
            {
                if (hit.gameObject.tag.Contains("Cat") && hit.gameObject == this.gameObject && gameManagerRef.isHoldingCat == false)
                {
                    dragging = true;
                    startDragPosition = transform.position;

                    Debug.Log("Picked up cat: " + this.gameObject.name);

                    if (gameManagerRef != null)
                    {
                        gameManagerRef.currentlySelectedCat = this.gameObject;
                        // gameManagerRef.UpdateCatLikesGUI();
                    }
                    gameManagerRef.isHoldingCat = true;
                    // Mark as being picked up
                    return;
                }
            }
        }

        if(dragging)
        {
            catHappinessRef.CheckIfAllLikesAreSatisfied();
        }
        // DRAGGING
        if (dragging && Input.GetMouseButton(0))
        {
            transform.position = mousePos;
            // currentSprite.sprite = holdingCat;
            if(gameObject.tag == "UnplacedCat")
            {
                gameObject.tag = "draggingCat";
            }
            anim.SetBool("isHeld", true); //set bool in animator
        
            //anim.enabled = false;
            //currentSprite.sprite = holdingCat;
        }

        // DROP
        if (dragging && Input.GetMouseButtonUp(0))
        {
            gameManagerRef.isHoldingCat = false;
            dragging = false;
            anim.SetBool("isHeld", false); //set bool in animator
            int dropAreaLayer = LayerMask.GetMask("DropArea");
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, dropAreaLayer);

            if (hitCollider != null && hitCollider.TryGetComponent(out ICatDropArea catDropArea))
            {
                catDropArea.OnCatDrop(this, mousePos);
                // this.gameObject.tag = "Cat"; // Successfully placed
                this.gameObject.tag = catType.ToString();
                // catHappinessRef.catPlumbob.SetActive(true);
                // catHappinessRef.CheckIfAllLikesAreSatisfied();
                gameManagerRef.CheckForLevelCompleteCondition();
            }
            else
            {
                transform.position = startDragPosition;
                if(gameObject.tag == "draggingCat")
                {
                    gameObject.tag = "UnplacedCat";
                }
            }

            // catHappinessRef.CheckIfAllLikesAreSatisfied();
        }

        // HOVER TRACKING (works even when dragging or dropped)
        UpdateHoveredCat(mousePos);

        bool heldValue = anim.GetBool("isHeld");
        Debug.Log("Animator reports isHeld = " + heldValue);
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
            // if (lastHoveredCat != null && lastHoveredCat.TryGetComponent<SpriteRenderer>(out var sr1))
            // {
            //     sr1.color = Color.white;
            // }

            lastHoveredCat = hoveredCat;

            // Highlight new cat and update selection
            if (hoveredCat != null)
            {
                // if (hoveredCat.TryGetComponent<SpriteRenderer>(out var sr2))
                //     sr2.color = Color.green;

                if (!dragging && gameManagerRef != null)
                {
                    gameManagerRef.currentlySelectedCat = hoveredCat.gameObject;
                    // gameManagerRef.UpdateCatLikesGUI();
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

public enum CatType
{
    Cat,
    AlienCat,
}
