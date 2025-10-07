using UnityEngine;

public class CatDragAndDrop : MonoBehaviour
{

    private Collider2D col;
    private Vector3 startDragPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }


    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp()
    {
        // col.enabled = false;
        // Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        // col.enabled = true;
        // if (hitCollider != null && hitCollider.TryGetComponent(out ICatDropArea catDropArea))
        // {
        //     catDropArea.OnCatDrop(this);
        // }
        // else
        // {
        //     transform.position = startDragPosition;
        // }
        // -------------------------------------
        col.enabled = false;
        // Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        Collider2D hitCollider = Physics2D.OverlapPoint(GetMousePositionInWorldSpace());

        col.enabled = true;

        if (hitCollider != null && hitCollider.TryGetComponent(out ICatDropArea catDropArea))
        {
            // pass the exact mouse position where drop happened
            catDropArea.OnCatDrop(this, GetMousePositionInWorldSpace());
        }
        else
        {
            transform.position = startDragPosition;
        }
        // --------------------------
        // col.enabled = false; 
        // Collider2D[] results = new Collider2D[10]; // array to hold possible hits
        // ContactFilter2D filter = new ContactFilter2D();
        // filter.NoFilter(); // detect all layers
        // int count = col.Overlap(filter, results); // check everything overlapping cat

        // col.enabled = true;

        // bool dropped = false;
        // for (int i = 0; i < count; i++)
        // {
        //     if (results[i].TryGetComponent(out ICatDropArea catDropArea))
        //     {
        //         catDropArea.OnCatDrop(this, GetMousePositionInWorldSpace());
        //         dropped = true;
        //         break;
        //     }
        // }

        // if (!dropped)
        // {
        //     transform.position = startDragPosition; // return to start if no drop area
        // }

    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
