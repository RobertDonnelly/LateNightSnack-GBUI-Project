using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerVoiceInteraction : MonoBehaviour
{
    private GrammarRecognizer gr;
    public bool inQueue = false;
    [SerializeField] private Animator waver;
    private string action_Out;
    [SerializeField] public GameObject[] foodPrefabs;
    [SerializeField] public Vector3 foodSpawner;

    Money script;

    public GameObject vendorUI;
    public GameObject foodToMake;
    public Transform foodToSpawn;

    public int cost = 5;

    // Start is called before the first frame update
    void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
         "Grammar.xml"),
         ConfidenceLevel.Low);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running");

        script = GameObject.FindWithTag("GameController").GetComponent<Money>();

    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        Debug.Log("recognised phrase");
        SemanticMeaning[] meanings = args.semanticMeanings;

        foreach (SemanticMeaning meaning in meanings)
        {
            string keyValue = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append(" Value :" + valueString + " , KEY: " + keyValue);

            if (inQueue == true)
            {
                switch (keyValue)
                {
                    case "order":
                        Debug.Log("ordered");
                        Order(valueString);
                        break;
                }
                Debug.Log(message);
            }
            else
            {
                Debug.Log("Your Order wont be taken unless you are in the Queue");
            }
        }
    }

    private void Order(string order)
    {
        Debug.Log("You have ordered  " + order);
        switch (order)
        {
            case "hamburger":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "chicken":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "waffles":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "milk":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "pizza":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "coke":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "icecream":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
            case "fries":
                foodToMake = Array.Find(foodPrefabs, foodToMake => foodToMake.name == order);
                break;
        }
        BuyFood(foodToMake);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int BuyFood(GameObject foodToMake)
    {
        if(script.gold >= cost)
        {
            script.Pay();
            Debug.Log("You have enough");
            FindObjectOfType<AudioManager>().Play("deposit");
            Instantiate(foodToMake, foodToSpawn.position, foodToSpawn.rotation);
        }
        else
        {
                        FindObjectOfType<AudioManager>().Play("deposit");

            Debug.Log("You don't have enough");
        }
        return script.gold;
    }


    private void OnTriggerEnter(Collider other)
    {
        //vendorUI.SetActive(true);
        waver.SetBool("wave", true);
        Debug.Log("object entered Trigger");
        inQueue = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //vendorUI.SetActive(false);
        inQueue = false;
        Debug.Log("Your Order wont be taken unless you are in the Queue");
        waver.SetBool("wave", false);
    }

    //failsafe stop mechanism    
    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }

  
}
