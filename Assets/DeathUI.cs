using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public GameObject deathPanel;

    void Start()
    {
        
        deathPanel.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..."); 
    }
}
