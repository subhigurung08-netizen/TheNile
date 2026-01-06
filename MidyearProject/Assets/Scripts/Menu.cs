using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject spawned;
    [SerializeField] private Vector3 positionSpawned;

    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
       
        
        
    }

 
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
