using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
            GameObject[] likeObjects;

            if(this.name.Contains("Alone"))
            {
                // Add all objects to likeObjects that contain Cat but not UnplacedCat
                List<GameObject> likeObjectsList = new List<GameObject>();
                GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects)
                {
                    if (obj.tag.Contains("Cat") && !obj.tag.Contains("UnplacedCat"))
                    {
                        likeObjectsList.Add(obj); // add to list
                    }
                }

                likeObjects = likeObjectsList.ToArray();
            }
            else{
                likeObjects = GameObject.FindGameObjectsWithTag(likeTag);
            }
            
            
            // GameObject[] likeObjects = GameObject.FindGameObjectsWithTag(likeTag);

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
}

public enum Type
{
    Like,
    Dislike
}