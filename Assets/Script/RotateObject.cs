using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateObject : MonoBehaviour
{
    public Transform objectChange;
    public float rotationSpeed;
    public Vector3 target;

    private Quaternion targetRotation;
    private bool Rotating = false;
    private bool Rotated = false;
    private Vector3 rotateOnTime;
    
    private float changingX;
    private float changingY;
    private float changingZ;
    private Vector3 startRoated;
    private Vector3 vectorOpen;
    private Vector3 vectorClose;

    private void Start()
    {
        targetRotation = Quaternion.Euler(target);
        changingX = objectChange.transform.rotation.x;
        changingY = objectChange.transform.rotation.y;
        changingZ = objectChange.transform.rotation.z;
        startRoated = new Vector3(changingX, changingY, changingZ);
        if (Mathf.Abs(changingX - target.x) >= 1f)
        {
            vectorOpen = Vector3.left;
            vectorClose = Vector3.right;
            Debug.Log("Rotate X");
            Debug.Log( "X: "+changingX + " - " + target.x);
        }
        if (Mathf.Abs(changingY - target.y) >= 1f)
        {
            vectorOpen = Vector3.down;
            vectorClose = Vector3.up;
            Debug.Log("Rotate Y");
            Debug.Log("Y: " + changingY + " - " + target.y);
        }
        if (Mathf.Abs(changingZ - target.z) >= 1f)
        {
            vectorOpen = Vector3.back;
            vectorClose = Vector3.forward;
            Debug.Log("Rotate Z");
            Debug.Log("Z: " + changingZ + " - " + target.z);

        }
    }
    void Update()
    {
        if (Rotated == false)
        {
            if (Rotating)
            {
                Vector3 rotating = vectorOpen * rotationSpeed * Time.deltaTime;
                objectChange.Rotate(rotating);
                changingX += rotating.x;
                changingY += rotating.y;
                changingZ += rotating.z;
                rotateOnTime = new Vector3(changingX, changingY, changingZ);
                if (Vector3.Distance(rotateOnTime, target) < 1f)
                {
                    Rotating = false;
                    rotateOnTime = target;
                    Rotated = true;
                }
            }
        }
        else
        {
            if (Rotating)
            {
                Vector3 rotating = vectorClose * rotationSpeed * Time.deltaTime;
                objectChange.Rotate(rotating);
                changingX += rotating.x;
                changingY += rotating.y;
                changingZ += rotating.z;
                rotateOnTime = new Vector3(changingX, changingY, changingZ);
                if (Vector3.Distance(rotateOnTime, startRoated) < 1f)
                {
                    Rotating = false;
                    rotateOnTime = startRoated;
                    Rotated = false;
                }
                Debug.Log(rotateOnTime);
            }
        }
        Debug.Log(Rotating);
    }

    public void StartRotation()
    {
        Rotating = true;
    }
    public void StopRotation()
    {
        Rotating = false;
    }
}
