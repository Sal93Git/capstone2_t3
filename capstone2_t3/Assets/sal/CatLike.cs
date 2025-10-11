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
            float distance = Vector3.Distance(cat.transform.position, food.transform.position);

            if (distance <= 5f)
            {
                Debug.Log("Cat Like Satisfied : "+ distance.ToString());
                return true;
            }
        }

        Debug.Log("No food nearby.");
        return false;
    }

    // public Sprite GetLikeIcon()
    // {
    //     return catImage;
    // }
}
