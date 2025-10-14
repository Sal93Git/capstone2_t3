using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] inLevelCats;
    public Image[] catLikeIcons;
    public GameObject currentlySelectedCat;

    public Transform checkBoxRef;
    public Transform textRef;

    public Sprite emptyIcon;

    public Sprite happyCheck;
    public Sprite unhappyCheck;

    void Start()
    {
        // Disable all cat like icons and clear text at scene start
        for (int i = 0; i < catLikeIcons.Length; i++)
        {
            if (catLikeIcons[i] != null)
            {
                catLikeIcons[i].enabled = false;
                ClearText(catLikeIcons[i]);
                Transform checkBoxRef = catLikeIcons[i].transform.GetChild(0);
                Image checkBoxImage = checkBoxRef.GetComponent<Image>();
                checkBoxImage.sprite = emptyIcon;
            }
        }
    }

    void Update()
    {
        // Optionally call this continuously if needed:
        // UpdateCatLikesGUI();
    }

    public void UpdateCatLikesGUI()
    {
        for (int i = 0; i < catLikeIcons.Length; i++)
        {
            if (currentlySelectedCat != null)
            {
                CatHappiness catHappiness = currentlySelectedCat.GetComponent<CatHappiness>();

                if (catHappiness.catLikes != null && i < catHappiness.catLikes.Length)
                {
                    var like = catHappiness.catLikes[i];

                    if (like != null && like.catImage != null)
                    {
                        // Enable and set sprite
                        catLikeIcons[i].enabled = true;
                        catLikeIcons[i].sprite = like.catImage;

                        // Update text
                        if (catLikeIcons[i].transform.childCount >= 2)
                        {
                            Transform textRef = catLikeIcons[i].transform.GetChild(1);
                            TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
                            if (textComponent != null)
                            {
                                textComponent.text = like.likeTag;
                            }

                            Transform checkBoxRef = catLikeIcons[i].transform.GetChild(0);
                            Image checkBoxImage = checkBoxRef.GetComponent<Image>();
                            if (like.IsLikeSatisfied(currentlySelectedCat))
                            {
                                checkBoxImage.sprite = happyCheck;
                            }
                            else
                            {
                                checkBoxImage.sprite = unhappyCheck;
                            }
                        }
                    }
                    else
                    {
                        // Disable image when no like
                        catLikeIcons[i].enabled = false;
                        ClearText(catLikeIcons[i]);
                        Transform checkBoxRef = catLikeIcons[i].transform.GetChild(0);
                        Image checkBoxImage = checkBoxRef.GetComponent<Image>();
                        checkBoxImage.sprite = emptyIcon;
                    }
                }
                else
                {
                    // Disable image if out of range
                    catLikeIcons[i].enabled = false;
                    ClearText(catLikeIcons[i]);
                    Transform checkBoxRef = catLikeIcons[i].transform.GetChild(0);
                    Image checkBoxImage = checkBoxRef.GetComponent<Image>();
                    checkBoxImage.sprite = emptyIcon;
                }
            }
            else
            {
                // No cat selected, disable all
                catLikeIcons[i].enabled = false;
                ClearText(catLikeIcons[i]);
                Transform checkBoxRef = catLikeIcons[i].transform.GetChild(0);
                Image checkBoxImage = checkBoxRef.GetComponent<Image>();
                checkBoxImage.sprite = emptyIcon;
            }
        }
    }

    private void ClearText(Image image)
    {
        if (image != null && image.transform.childCount >= 2)
        {
            Transform textRef = image.transform.GetChild(1);
            TextMeshProUGUI textComponent = textRef.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = "";
            }
        }
    }
}
