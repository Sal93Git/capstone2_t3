using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public float detectionRadius = 2f;
    public bool buttonActive = false;

    private void Update()
    {
        buttonActive = CatNearby();
    }

    bool CatNearby()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj == this.gameObject) 
            {
                continue;
            }
            if (obj.tag.ToLower()!=("cat")) 
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, obj.transform.position);

            if (distance <= detectionRadius)
            {
                // cat found in range
                return true; 
            }
        }

        // no cats in range
        return false; 
    }
}
