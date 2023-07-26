using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawnPos;
   
    public void Spawn(int count)
    {
        Instantiate(characters[count],spawnPos.position,Quaternion.identity);        
    }
}
