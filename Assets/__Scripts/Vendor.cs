using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    Money script;

    public GameObject vendorUI;
    public GameObject foodToMake;
    public Transform foodToSpawn;

    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindWithTag("GameController").GetComponent<Money>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
