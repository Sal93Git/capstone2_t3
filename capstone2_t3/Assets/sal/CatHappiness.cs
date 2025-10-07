using UnityEngine;

public class CatHappiness : MonoBehaviour
{
    public CatLike[] catLikes;

    CatDragAndDrop catDragAndDropRef;

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
        foreach(CatLike like in catLikes)
        {
            like.IsLikeSatisfied(this.gameObject);
        }
    }
}
