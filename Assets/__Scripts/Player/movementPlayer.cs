using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementPlayer : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -30f;
    public float jumpHeight = 1f;

    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Camera mainCam;
    public float interactDistance = 2f;
    public GameObject interactionUI;
    public Text interactionText;
    //ui's
    public GameObject starvationUI;
    public GameObject moneyUI;
    public GameObject Hungerbar;
    public GameObject crosshair;



    Vector3 velocity;
    bool isGrounded;

    [Header("Player Hunger")]
    public float Hunger =100f;
    public float HungerMax = 100f;
    public float HungerOT = 0.2f;
    public Slider HungerSlider;
    

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        InteractionRay();
        Hunger = Hunger - HungerOT * Time.deltaTime;
        HungerSlider.value = Hunger / HungerMax;

        // check if we have starved
        //stios movement and activeates UI
        if (HungerSlider.value <= 0)
        {
            moneyUI.SetActive(false);
            crosshair.SetActive(false);
            Hungerbar.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            starvationUI.SetActive(true);
            mainCam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else//if not move freely
        {



            isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);


        }
        
    }

     void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitsomething = false;
        if(Physics.Raycast(ray,out hit, interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                hitsomething = true;
                interactionText.text = interactable.GetDescription();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                    FindObjectOfType<AudioManager>().Play("eat");
                    Eat();
                }
            }
        }
        interactionUI.SetActive(hitsomething);
    }

   public void Eat()
    {
        Hunger = Hunger + 20;
    }
}
