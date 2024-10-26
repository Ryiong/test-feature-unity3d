using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractionControl : MonoBehaviour
{
    [SerializeField] private float raycastRange = 24.1f;
    [SerializeField] private LayerMask layerMask;
    public Transform camera;
    public TextMeshProUGUI Grap_Text;
    public TextMeshProUGUI Pull_Text;
    public TextMeshProUGUI Key_Text;
    public TextMeshProUGUI Open_Text;
    public TextMeshProUGUI Close_Text;
    public TextMeshProUGUI NeedKey_Text;
    public TextMeshProUGUI LockOtherSide_Text;
    public TextMeshProUGUI InteractText_Left;
    public TextMeshProUGUI InteractText_Right;

    public Transform holdPosition;

    private GameObject currentObject;
    private Rigidbody currentRigidbody;

    public Light sceneLight;


    public float throwForce = 20f;

    bool isPickup = false;

    public MKDoor mkDoor;
    public MDoor mDoor;

    private RotateObject currentRotatingObject;
    // Start is called before the first frame update
    void Start()
    {
        Grap_Text.gameObject.SetActive(false);
        Pull_Text.gameObject.SetActive(false);
        Key_Text.gameObject.SetActive(false);
        Open_Text.gameObject.SetActive(false);
        Close_Text.gameObject.SetActive(false);
        NeedKey_Text.gameObject.SetActive(false);
        LockOtherSide_Text.gameObject.SetActive(false);
        InteractText_Left.gameObject.SetActive(false);
        InteractText_Right.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, raycastRange, layerMask))
        {
            DisplayInteractText();
            if (hit.collider.gameObject.CompareTag("canPickUp") && isPickup == false)
            {
                Grap_Text.gameObject.SetActive(true);
            }
            if (hit.collider.gameObject.CompareTag("LightControl"))
            {

            }
            if (hit.collider.gameObject.CompareTag("door"))
            {
                if (mkDoor.MKDAnimation.GetBool("lock"))
                {
                    NeedKey_Text.gameObject.SetActive(false);
                }
                else
                {
                    if (mkDoor.MKDAnimation.GetBool("open"))
                    {
                        Open_Text.gameObject.SetActive(true);
                    }
                    else
                    {
                        Close_Text.gameObject.SetActive(true);
                    }
                }
            }
            if (hit.collider.gameObject.CompareTag("doorUnlock"))
            {
                if (mDoor.MAnimation.GetBool("lock"))
                {
                    LockOtherSide_Text.gameObject.SetActive(true);
                }
            }
            if (hit.collider.gameObject.CompareTag("key"))
            {
                Key_Text.gameObject.SetActive(true);
            }
            if (hit.collider.gameObject.CompareTag("Pullable"))
            {
                Pull_Text.gameObject.SetActive(true);
            }

            if (hit.collider != null)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.gameObject.CompareTag("canDestroy"))
                    {
                        Destroy(hit.collider.gameObject);
                        Debug.Log("Object destroyed: " + hit.collider.gameObject.name);
                    }
                    if (hit.collider.gameObject.CompareTag("canPickUp") && isPickup == false)
                    {
                        PickUpObject();
                    }
                    if (hit.collider.gameObject.CompareTag("LightControl"))
                    {
                        CheckLight();
                    }
                    if (hit.collider.gameObject.CompareTag("door"))
                    {
                        if (mkDoor.MKDAnimation.GetBool("lock"))
                        {
                            mkDoor.TriggerLock();
                        }
                        else
                        {
                            if (mkDoor.MKDAnimation.GetBool("open"))
                            {
                                mkDoor.TriggerClose();
                                Destroy(currentObject);
                            }
                            else
                            {
                                mkDoor.TriggerOpen();
                            }
                        }
                    }
                    if (hit.collider.gameObject.CompareTag("doorUnlock"))
                    {
                        if (mDoor.MAnimation.GetBool("lock"))
                        {
                            mDoor.TriggerLock();
                        }
                    }
                    if (hit.collider.gameObject.CompareTag("key"))
                    {
                        PickUpObject();
                        mkDoor.TriggerUnLock();
                    }
                    if (hit.collider.gameObject.CompareTag("rotateObject"))
                    {
                        RotateObject rotateScript = hit.collider.GetComponent<RotateObject>();
                        if (rotateScript != null)
                        {
                            rotateScript.StartRotation();
                            currentRotatingObject = rotateScript;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Did not Hit");
            Grap_Text.gameObject.SetActive(false);
            Pull_Text.gameObject.SetActive(false);
            Key_Text.gameObject.SetActive(false);
            Open_Text.gameObject.SetActive(false);
            Close_Text.gameObject.SetActive(false);
            NeedKey_Text.gameObject.SetActive(false);
            LockOtherSide_Text.gameObject.SetActive(false);
            InteractText_Left.gameObject.SetActive(false);
            InteractText_Right.gameObject.SetActive(false);
            
        }
        if (isPickup) {
            if(Input.GetMouseButtonDown(0)) { ThrowObject(); }
        }
    }
    void PickUpObject()
    {
        isPickup = true;
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, raycastRange, layerMask))
        {            
            currentObject = hit.collider.gameObject;
            currentRigidbody = currentObject.GetComponent<Rigidbody>();

            if (currentRigidbody != null)
            {
                currentRigidbody.useGravity = false;
                currentRigidbody.isKinematic = true;
                currentObject.transform.position = holdPosition.position;
                currentObject.transform.SetParent(holdPosition);
            }
        }
    }
    void ThrowObject()
    {
        isPickup = false;
        if (currentRigidbody != null)
        {
            currentObject.transform.SetParent(null);
            currentRigidbody.useGravity = true;
            currentRigidbody.isKinematic = false;

            currentRigidbody.AddForce(camera.transform.forward * throwForce, ForceMode.VelocityChange);

            currentObject = null;
        }
    }

    void DisplayInteractText()
    {
        InteractText_Left.gameObject.SetActive(true);
        InteractText_Right.gameObject.SetActive(true);
    }

    public void TurnOnLight()
    {
        sceneLight.enabled = true;
    }

    public void TurnOffLight()
    {
        sceneLight.enabled = false;
    }
    public void CheckLight()
    {
        if (sceneLight != null)
        {
            if (sceneLight.enabled == false) { Debug.Log("is Off"); TurnOnLight(); }
            else { Debug.Log("is On"); TurnOffLight(); }
        }
    }
}
