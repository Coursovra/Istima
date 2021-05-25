using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
