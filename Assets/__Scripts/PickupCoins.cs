using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    Money script;
    public int coinAmount = 5;

    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindWithTag("GameController").GetComponent<Money>();
    }

    private void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("pickup");
            script.gold += coinAmount;
            Destroy(gameObject);
        }
    }
}
