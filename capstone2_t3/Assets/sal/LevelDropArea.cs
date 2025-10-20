using UnityEngine;

public class LevelDropArea : MonoBehaviour, ICatDropArea
{

    public void OnCatDrop(CatDragAndDrop cat, Vector3 dropPosition)
    {
        // cat.transform.position = transform.position;
        // Debug.Log("Cat dropped on "+gameObject.name);
        cat.transform.position = dropPosition;
    }


}
