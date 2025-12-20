using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject[] spawned;

    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        PlaySpawn();
        // SceneManager.LoadScene(1);
        // Vector3 positionSpawned = new Vector3(Random.Range(0,100), Random.Range(5,20), Random.Range(0,100));
        // for(int i=0; i<spawned.Length; i++){
        //     Instantiate(spawned[i], positionSpawned, Quaternion.identity);
        // }

    }

    void PlaySpawn()
    {
        
        Vector3 positionSpawned = new Vector3(Random.Range(0,100), Random.Range(5,20), Random.Range(0,100));
        for(int i=0; i<spawned.Length; i++){
            Instantiate(spawned[i], positionSpawned, Quaternion.identity);
        }
        Debug.Log("spawned");
        SceneManager.LoadScene(1);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
