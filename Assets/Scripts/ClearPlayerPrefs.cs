using UnityEngine;

/// <summary>
/// Debug
/// </summary>
public class ClearPlayerPrefs : MonoBehaviour
{
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
