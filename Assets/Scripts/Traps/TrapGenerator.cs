using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Traps;


public class TrapGenerator : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateTraps();               
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void GenerateTraps()
    {
        for (int i = 1; i <= 10; i++)
            Trap.Generate(gameObject, 0, new Vector3(i, 0, 0), Quaternion.identity, Vector3.one);   
    }    

}
