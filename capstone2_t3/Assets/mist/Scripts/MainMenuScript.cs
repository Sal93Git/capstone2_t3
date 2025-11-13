using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    private VisualElement MainMenuButtons;
    private VisualElement LevelButtons;
    private Button PlayBut;
    private Button QuitBut;
    private Button Lvl1But;
    private Button Lvl2But;
    private Button Lvl3But;
    private VisualElement BlackScreen;

    public AudioSource audioSource;


    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        MainMenuButtons = root.Q<VisualElement>("MainMenuButtons");
        LevelButtons = root.Q<VisualElement>("LevelButtons");
        BlackScreen = root.Q<VisualElement>("BlackScreen");

        PlayBut = root.Q<Button>("PlayBut");
        QuitBut = root.Q<Button>("QuitBut");

        Lvl1But = root.Q<Button>("Lvl1But");
        Lvl2But = root.Q<Button>("Lvl2But");
        Lvl3But = root.Q<Button>("Lvl3But");

        PlayBut.clicked += OnStartButtonClicked;
        QuitBut.clicked += OnQuitButtonClicked;

        Lvl1But.clicked += () => StartCoroutine(LoadLevelWithFade(2, Lvl1But));
        Lvl2But.clicked += () => StartCoroutine(LoadLevelWithFade(3, Lvl2But));
        Lvl3But.clicked += () => StartCoroutine(LoadLevelWithFade(4, Lvl3But));

        // Start with them hidden
        Lvl1But.AddToClassList("LvlUnclicked");
        Lvl2But.AddToClassList("LvlUnclicked");
        Lvl3But.AddToClassList("LvlUnclicked");
    }

    void OnStartButtonClicked()
    {
        Debug.Log("Start button pressed!");
        audioSource.Play();
        if (PlayBut.ClassListContains("Clicked"))
        {
            PlayBut.RemoveFromClassList("Clicked");
            QuitBut.RemoveFromClassList("Clicked");
            Lvl1But.AddToClassList("LvlUnclicked");
            Lvl2But.AddToClassList("LvlUnclicked");
            Lvl3But.AddToClassList("LvlUnclicked");
        }
        else
        {
            PlayBut.AddToClassList("Clicked");
            QuitBut.AddToClassList("Clicked");
            StartCoroutine(ShowLevelButtonsSequentially());
        }
    }

    IEnumerator ShowLevelButtonsSequentially()
    {
        float delay = 0.3f;

        Lvl1But.RemoveFromClassList("LvlUnclicked");
        Lvl1But.AddToClassList("LvlClicked");
        yield return new WaitForSeconds(delay);

        Lvl2But.RemoveFromClassList("LvlUnclicked");
        Lvl2But.AddToClassList("LvlClicked");
        yield return new WaitForSeconds(delay);

        Lvl3But.RemoveFromClassList("LvlUnclicked");
        Lvl3But.AddToClassList("LvlClicked");
    }

    IEnumerator LoadLevelWithFade(int sceneIndex, Button clickedButton)
    {
        // Trigger the fade-out animation
        BlackScreen.RemoveFromClassList("Unfaded");
        BlackScreen.AddToClassList("Faded");
        audioSource.Play(); //play click sfx
        clickedButton.AddToClassList("LvlFadeout");

        // Wait for fade duration (match this to your CSS transition time)
        yield return new WaitForSeconds(1f);

        // Now change the scene
        SceneManager.LoadScene(sceneIndex);
    }

    void OnQuitButtonClicked()
    {
        audioSource.Play();
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
