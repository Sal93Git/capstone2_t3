using UnityEngine;

public class CatHappiness : MonoBehaviour
{
    public CatLike[] catLikes;

    private CatDragAndDrop catDragAndDropRef;

    public bool catSatisfied = false;
    private bool partiallySatisfied = false;

    public GameObject catPlumbob;

    public Sprite happyPlumbob;
    public Sprite neutralPlumbob;
    public Sprite unhappyPlumbob;

    void Start()
    {
        catDragAndDropRef = GetComponent<CatDragAndDrop>();
    }

    public void CheckIfAllLikesAreSatisfied()
    {
        bool allSatisfied = true;
        partiallySatisfied = false;

        foreach (CatLike like in catLikes)
        {
            if (!like.IsLikeSatisfied(this.gameObject))
            {
                allSatisfied = false;
            }
            else
            {
                partiallySatisfied = true;
            }
        }

        // Make sure the plumbob is visible
        if (catPlumbob != null && !catPlumbob.activeSelf)
            catPlumbob.SetActive(true);

        var sr = catPlumbob.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("Cat Plumbob has no SpriteRenderer!");
            return;
        }

        if (allSatisfied)
        {
            catSatisfied = true;
            sr.sprite = happyPlumbob;
        }
        else if (partiallySatisfied)
        {
            catSatisfied = false;
            sr.sprite = neutralPlumbob;
        }
        else
        {
            catSatisfied = false;
            sr.sprite = unhappyPlumbob;
        }
    }
}
