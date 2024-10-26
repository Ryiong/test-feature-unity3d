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
    public Transform camera;
    public Vector3 target;

    private Quaternion targetRotation;
    private bool Rotating = false;
    private bool Rotated = false;
    private Vector3 rotateOnTime;
    

    [SerializeField] private float raycastRange = 24.1f;
    [SerializeField] private LayerMask layerMask;
    private float changingX;
    private float changingY;
    private float changingZ;
    private Vector3 startRoated;
    private Vector3 vectorOpen;
    private Vector3 vectorClose;

    // Start is called before the first frame update
    private void Start()
    {

        targetRotation = Quaternion.Euler(target);
        changingX = objectChange.transform.rotation.x;
        changingY = objectChange.transform.rotation.y;
        changingZ = objectChange.transform.rotation.z;
        startRoated = new Vector3(changingX, changingY, changingZ);
        if (changingX != target.x)
        {
            vectorOpen = Vector3.left;
            vectorClose = Vector3.right;
        } else if (changingY != target.y)
        {
            vectorOpen = Vector3.down;
            vectorClose = Vector3.up;
        } else if (changingZ != target.z)
        {
            vectorOpen = Vector3.back;
            vectorClose = Vector3.forward;
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
