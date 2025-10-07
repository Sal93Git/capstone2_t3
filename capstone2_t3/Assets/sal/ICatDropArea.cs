using UnityEngine;

public interface ICatDropArea
{
    //void OnCatDrop(CatDragAndDrop cat);
    void OnCatDrop(CatDragAndDrop cat, Vector3 dropPosition);
}
