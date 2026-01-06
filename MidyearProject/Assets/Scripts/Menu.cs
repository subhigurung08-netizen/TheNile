using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject spawned;
    [SerializeField] private Vector3 initialPosition;

    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene("DenialIsARiverInEgypt");
        // Vector3 positionSpawned = new Vector3(Random.Range(0,100), Random.Range(5,20), Random.Range(0,100));
        // for(int i=0; i<spawned.Length; i++){
        //     Instantiate(spawned[i], positionSpawned, Quaternion.identity);
        // }
        

    }
    // Called when we click the "Quit" button.
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
