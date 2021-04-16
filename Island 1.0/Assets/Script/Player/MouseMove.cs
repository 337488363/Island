using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    public CharacterController character;
    public float speed = 1;
    private float mousespeed = 5f;
    private float minmouseY = -65F;
    private float maxmouseY = 65f;

    private float rotationY = 0f;
    private float rotationX = 0f;
    private Vector3 direction;
    private float gravity = 7f;

    public Transform targetcamera;


    void Update()
    {
        GameObject pack = GameObject.Find("Pack");
        if (pack.GetComponentInChildren<Image>().enabled==true)
        {
            return;
        }

        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        if (character.isGrounded)
        { 
            direction = new Vector3(_horizontal, 0, _vertical);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction.y += 70f * Time.deltaTime;
        }
        if (character.isGrounded)
        {
            gravity = 0.1f;
        }
        else
        {
            gravity = 7f;
        }
        
        direction.y -= gravity * Time.deltaTime;

        character.Move(character.transform.TransformDirection(direction * Time.deltaTime * speed));

        rotationX += Input.GetAxis("Mouse X") * mousespeed + targetcamera.transform.localEulerAngles.y;
        rotationY -= Input.GetAxis("Mouse Y") * mousespeed;
       
        rotationY = Mathf.Clamp(rotationY, minmouseY, maxmouseY);//限制视角

        this.transform.eulerAngles = new Vector3(rotationY, rotationX, 0);
        targetcamera.transform.eulerAngles=new  Vector3(rotationY,rotationX,0);

        
    }






}
