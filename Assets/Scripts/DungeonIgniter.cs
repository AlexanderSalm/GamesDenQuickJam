using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonIgniter : MonoBehaviour
{
    public GameObject dungeonGenerator;
    // Start is called before the first frame update
    private bool stop = false;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop) {
            dungeonGenerator.GetComponent<DungeonGenerator>().GenerateDungeon(5, 5);
            stop = true;
        }
    }
}
