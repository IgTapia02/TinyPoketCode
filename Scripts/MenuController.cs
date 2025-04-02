using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_Text startButtonText;
    public TMP_Text creditsButtonText;

    public TMP_Text quitButtonText;

    public void StartGame()
    {
        SceneManager.LoadScene("Mazmorra1");  
    }
    public void Credits()
    {
        SceneManager.LoadScene("Creditos");

    }
    public void GoBack()
    {
        SceneManager.LoadScene("Menu");

    }

    public void QuitGame()
    {
        
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
