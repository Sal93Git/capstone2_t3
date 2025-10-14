using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Like", menuName = "Cat/Like")]
public class CatLike : ScriptableObject
{
    public string likeTag;
    public Sprite catImage;

    public bool IsLikeSatisfied(GameObject cat)
    {
        if (cat == null)
        {
            Debug.LogWarning("No cat reference provided!");
            return false;
        }

        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag(likeTag);

        foreach (GameObject food in foodObjects)
        {
            // Skip if this object *is* the same as the provided cat
            if (food == cat)
                continue;

            float distance = Vector3.Distance(cat.transform.position, food.transform.position);

            if (distance <= 1.65f)
            {
                Debug.Log($"Cat Like Satisfied : {distance}");
                return true;
            }
        }

        Debug.Log("No food nearby.");
        return false;
    }
}
