using UnityEngine;

public class MapSection : MonoBehaviour
{
    public string sectionName; // For reference

    private void OnMouseDown()
    {
        Debug.Log("Clicked on section: " + sectionName);
        MapManager.Instance.LoadSection(sectionName);
    }
}
