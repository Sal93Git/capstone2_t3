using UnityEngine;

public class CatHappiness : MonoBehaviour
{
    public CatLike[] catLikes;

    CatDragAndDrop catDragAndDropRef;

    bool catSatisfied = false;

    void Start()
    {
        catDragAndDropRef = GetComponent<CatDragAndDrop>();
    }

    public void CheckIfAllLikesAreSatisfied()
    {
        // if(catDragAndDropRef.dragging)
        // {
        //     foreach(CatLike like in catLikes)
        //     {
        //         like.IsLikeSatisfied(this.gameObject);
        //     }
        // }
        bool allSatisfied = true;

        foreach(CatLike like in catLikes)
        {
            if(like.IsLikeSatisfied(this.gameObject))
            {
                allSatisfied = false;
                break;
            }
        }

        if(allSatisfied==true)
        {
            catSatisfied = true;
        }
        else if(allSatisfied==false)
        {
            catSatisfied = false;
        }
    }
}
