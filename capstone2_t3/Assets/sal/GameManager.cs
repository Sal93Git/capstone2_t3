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

    public void UpdateCatLikesGUI()
    {
        for (int i = 0; i < catLikeIcons.Length; i++)
        {
            if (currentlySelectedCat != null)
            {
                if(currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i] != null)
                {                   
                    checkBoxRef = catLikeIcons[i].transform.GetChild(0); // first child
                    textRef = catLikeIcons[i].transform.GetChild(1); // second child

                    catLikeIcons[i].sprite = currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i].catImage;

                    TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                    textComponent.text = currentlySelectedCat.GetComponent<CatHappiness>().catLikes[i].likeTag;
                }
            }
            else{
                catLikeIcons[i].sprite = emptyIcon;
            }
            
        }
    }

}
