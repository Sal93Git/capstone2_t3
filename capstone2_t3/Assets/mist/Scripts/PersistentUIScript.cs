using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.CompilerServices;

public class PersistentUI : MonoBehaviour
{
    private static PersistentUI instance;
    private VisualElement BlackScreen;

    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeBlackScreen();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeBlackScreen();

        // Optional: start fully black again if needed
        BlackScreen.style.opacity = 1f;

        StartCoroutine(FadeIn());
    }

    private void InitializeBlackScreen()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        BlackScreen = root.Q<VisualElement>("BlackScreen");

        // Start fully black
        if (BlackScreen != null)
        {
            BlackScreen.style.opacity = 1f;
        }
        else
        {
            Debug.LogWarning("BlackScreen element not found in the current scene.");
        }
    }

    private IEnumerator FadeIn()
    {
        if (BlackScreen == null) yield break;

        // Force one frame to ensure UI is ready
        yield return new WaitForEndOfFrame();

        // Start fully black
        BlackScreen.style.opacity = 1f;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            BlackScreen.style.opacity = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        BlackScreen.style.opacity = 0f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2);
        Debug.Log("nextlevel clicked");
    }
}
