using UnityEngine;

public class MeteorRotation : MonoBehaviour //rotates gameobject script is attached too, can adjust rotation direction, and speed
{
    public bool flipRotation;
    public float rotationSpeed;
    void Update()
    {   
        
        float direction = flipRotation ? -1f : 1f;
        transform.Rotate(0f, 0f, direction * rotationSpeed * Time.deltaTime);
    }
}

