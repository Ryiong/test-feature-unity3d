using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 10f;
    public Camera cameraPlayer;

    bool isCrowling = false;
    public float yCamera;

    void Start()
    {
        yCamera = cameraPlayer.transform.position.y;
    }
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveY;

        controller.Move(move * moveSpeed *Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrowling) { Stand(); } else { Crawl(); }
        }
    }
    void Crawl()
    {
        //Set Character Controler
        controller.height = controller.height / 2;
        Vector3 centerControler = controller.center;
        centerControler.y = controller.height / 2; ;
        controller.center = centerControler;

        //Set Camera
        Vector3 camCrawl = cameraPlayer.transform.position;
        camCrawl.y = controller.height / 2;
        cameraPlayer.transform.position = camCrawl;
        isCrowling = true;
        moveSpeed = moveSpeed / 1.5f;
    }

    void Stand()
    {
        //Set Character Controler
        controller.height = controller.height * 2;
        Vector3 centerControler = controller.center;
        centerControler.y = 0;
        controller.center = centerControler;

        //Set Camera
        Vector3 camCrawl = cameraPlayer.transform.position;
        camCrawl.y = yCamera;
        cameraPlayer.transform.position = camCrawl;
        isCrowling = false;
        moveSpeed = moveSpeed * 1.5f;

    }
}
