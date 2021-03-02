using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main Instance; 
    public Log log;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        log = GetComponent<Log>();
    }

}
