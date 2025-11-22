using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject lightControlPanel;
    public GameObject lights;

    // Update is called once per frame
    void Update()
    {
        if(lightControlPanel.GetComponent<ActivateButton>().buttonActive == true)
        {
            lights.SetActive(true);
        }
        else
        {
            lights.SetActive(false);
        }
    }
}
