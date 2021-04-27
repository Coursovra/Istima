using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Ui;
    public GameObject ObstacleController;
    public GameObject StartGameObjects;
    
    public void SwitchUi(bool value)
    {
        Menu.SetActive(!value);
        Ui.SetActive(value);
        ObstacleController.SetActive(value);
        StartGameObjects.SetActive(!value);
    }
}
