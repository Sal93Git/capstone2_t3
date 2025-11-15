using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    private VisualElement Buttons;
    private Button PlayBut;
    private Button LevelBut;
    private Button QuitBut;
    void OnEnable()
    {
        // Get the root VisualElement from the UI Document
        var root = GetComponent<UIDocument>().rootVisualElement;

        //get the buttons (and parent buttons are on) so we can do something when they are clicked
        Buttons = root.Q<VisualElement>("Buttons");
       
        PlayBut = root.Q<Button>("PlayBut");
        LevelBut = root.Q<Button>("LevelsBut");
        QuitBut = root.Q<Button>("QuitBut");

        //connect method to on button clicked
        PlayBut.clicked += OnStartButtonClicked;
        LevelBut.clicked += OnLevelButButtonClicked;
        QuitBut.clicked += OnQuitButtonClicked;
    }

    void OnStartButtonClicked()
    {
        Debug.Log("Start button pressed!");
        // Example action:
        SceneManager.LoadScene(1); //load main menu
     }

    void OnLevelButButtonClicked()
    {
        Debug.Log("Levelk button pressed!");
        // Example action:
        SceneManager.LoadScene("GameScene");
    }
    
    void OnQuitButtonClicked()
    {
        Debug.Log("Quit button pressed!");
        // Example action:
        Application.Quit();
    }
}
