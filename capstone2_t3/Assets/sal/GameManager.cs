using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject[] inLevelCats;
    public Image [] catLikeIcons;
    public GameObject currentlySelectedCat;

    public Transform checkBoxRef;
    public Transform textRef;

    public Sprite emptyIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateCatLikesGUI();
    }

    // public void UpdateCatLikesGUI()
    // {
    //     for (int i = 0; i < catLikeIcons.Length; i++)
    //     {
    //         if (currentlySelectedCat != null)
    //         {
    //             if(currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i])
    //             {
    //                 if(currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i] != null)
    //                 {                   
    //                     checkBoxRef = catLikeIcons[i].transform.GetChild(0); // first child
    //                     textRef = catLikeIcons[i].transform.GetChild(1); // second child

    //                     catLikeIcons[i].sprite = currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i].catImage;

    //                     TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
    //                     textComponent.text = currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i].likeTag;
    //                 }
    //             }
    //             else{
    //                 catLikeIcons[i].sprite = emptyIcon;
    //             }
    //         }
    //         else{
    //             catLikeIcons[i].sprite = emptyIcon;
    //         }
            
    //     }
    // }
    public void UpdateCatLikesGUI()
{
    for (int i = 0; i < catLikeIcons.Length; i++)
    {
        if (currentlySelectedCat != null)
        {
            CatHappiness catHappiness = currentlySelectedCat.GetComponent<CatHappiness>();

            // Only update if catLikes array exists and has this index
            if (catHappiness.catLikes != null && i < catHappiness.catLikes.Length)
            {
                var like = catHappiness.catLikes[i];

                if (like != null && like.catImage != null)
                {
                    // Set icon sprite
                    catLikeIcons[i].sprite = like.catImage;

                    // Set text
                    if (catLikeIcons[i].transform.childCount >= 2)
                    {
                        Transform textRef = catLikeIcons[i].transform.GetChild(1);
                        TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.text = like.likeTag;
                        }
                    }
                }
                else
                {
                    // Empty like
                    catLikeIcons[i].sprite = emptyIcon;

                    if (catLikeIcons[i].transform.childCount >= 2)
                    {
                        Transform textRef = catLikeIcons[i].transform.GetChild(1);
                        TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.text = "";
                        }
                    }
                }
            }
            else
            {
                // No like at this index, set empty
                catLikeIcons[i].sprite = emptyIcon;

                if (catLikeIcons[i].transform.childCount >= 2)
                {
                    Transform textRef = catLikeIcons[i].transform.GetChild(1);
                    TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                    if (textComponent != null)
                    {
                        textComponent.text = "";
                    }
                }
            }
        }
        else
        {
            // No cat selected, set all to empty
            catLikeIcons[i].sprite = emptyIcon;

            if (catLikeIcons[i].transform.childCount >= 2)
            {
                Transform textRef = catLikeIcons[i].transform.GetChild(1);
                TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = "";
                }
            }
        }
    }
}


}
