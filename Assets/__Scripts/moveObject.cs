using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour,Interactable
{
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;

    public movementPlayer script;

    // Start is called before the first frame update
    void Start()
    {
        //item.GetComponent<Rigidbody>().useGravity = true;
        //guide = GameObject.FindWithTag("guide").GetComponent<Transform>();
        tempParent = GameObject.FindWithTag("guide");
        guide = tempParent.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnMouseDown()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.position = guide.transform.position;
        //item.transform.rotation = guide.transform.rotation;
        item.transform.parent = tempParent.transform;
    }

    private void OnMouseUp()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.position = guide.transform.position;
        item.transform.parent = null ;
    }

    public void Interact()
    {
        Destroy(item);
    }

    public string GetDescription()
    {
        return "Eat";
    }
}
