using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Like", menuName = "Cat/Like")]
public class CatLike : ScriptableObject
{
    public string likeTag;
    public Sprite catImage;
    public Type type;



    public bool IsLikeSatisfied(GameObject cat)
    {
        if (cat == null)
        {
            Debug.LogWarning("No cat reference provided!");
            return false;
        }

        if (cat.tag != "UnplacedCat")
        {
            GameObject[] likeObjects = GameObject.FindGameObjectsWithTag(likeTag);

            // For Like, true if ANY within range
            if (type == Type.Like)
            {
                foreach (GameObject like in likeObjects)
                {
                    if (like == cat)
                        continue;

                    float distance = Vector3.Distance(cat.transform.position, like.transform.position);
                    if (distance <= 1.65f)
                    {
                        Debug.Log($"Cat Like Satisfied : {distance}");
                        return true;
                    }
                }

                return false; // none close enough
            }

            // For Dislike, true only if ALL are far enough
            else if (type == Type.Dislike)
            {
                foreach (GameObject like in likeObjects)
                {
                    if (like == cat)
                        continue;

                    float distance = Vector3.Distance(cat.transform.position, like.transform.position);
                    if (distance < 3f)
                    {
                        Debug.Log($"Cat Dislike NOT satisfied, too close ({distance})");
                        return false; // one too close = fail
                    }
                }

                Debug.Log("Cat Dislike Satisfied (all far away)");
                return true; // all far enough
            }
        }

        Debug.Log("No like nearby.");
        return false;
    }

//     public bool IsLikeSatisfied(GameObject cat)
//     {
//         if (cat == null)
//         {
//             Debug.LogWarning("No cat reference provided!");
//             return false;
//         }


//         if(cat.tag != "UnplacedCat")
//         {
//             GameObject[] likeObjects = GameObject.FindGameObjectsWithTag(likeTag);

//             foreach (GameObject like in likeObjects)
//             {
//                 // Skip if this object *is* the same as the provided cat
//                 if (like == cat)
//                     continue;

//                 float distance = Vector3.Distance(cat.transform.position, like.transform.position);

//                 if(type == Type.Like)
//                 {
//                     if (distance <= 1.65f)
//                     {
//                         Debug.Log($"Cat Like Satisfied : {distance}");
//                         return true;
//                     }
//                 }
//                 else if(type == Type.Dislike)
//                 {
//                     if (distance >= 3f)
//                     {
//                         Debug.Log($"Cat Dislike Satisfied : {distance}");
//                         return true;
//                     }
//                     else
//                     {
//                         return false;
//                     }
//                 }
//             }
//         }

//         Debug.Log("No like nearby.");
//         return false;
//     }
}

public enum Type
{
    Like,
    Dislike
}