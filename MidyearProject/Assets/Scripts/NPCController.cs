using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour
{
    private List<string> states;
   
 
    // Start is called before the first frame update
 
    void Start()
    {
        states = new List<string>();
       
        states.Add("Roam");
        states.Add("Chase");
        states.Add("Enchantment");
    }

    // Update is called once per frame
    void Update()
    {
        
        int num = Random.Range(0, states.Count);
        Debug.Log(states[num]);
    }
}

  
