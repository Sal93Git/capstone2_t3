using UnityEngine;
using UnityEngine.SceneManagement;
public class TempNextButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void NextLevel(int index)
    {
        SceneManager.LoadScene(index);
        Debug.Log("nextlevel clicked");
    }
}
