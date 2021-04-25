using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int gold;
    public int cost = 5;

    [SerializeField]GameObject moneyUI;
    [SerializeField]Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Pay()
    {
        gold = gold - cost;
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.GetComponent<Text>().text = gold.ToString();
        //moneyText.text = gold.ToString();
        if(gold < 0)
        {
            gold = 0;
        }
    }
}
