using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PullObject : MonoBehaviour
{
    public float openSpeed = 2.0f; 
    public float maxOpenDistance = 4f; 
    private Vector3 closedPosition; 
    private Vector3 openPosition; 
    bool isOpen = false;

    [SerializeField] private float raycastRange = 24.1f;
    [SerializeField] private LayerMask layerMask;
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + new Vector3(-maxOpenDistance, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, raycastRange, layerMask))
        {

            if (hit.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && hit.collider.gameObject.CompareTag("Pullable"))
                {
                    isOpen = !isOpen;
                }
            }
        }
        if (isOpen)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, openPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, closedPosition, Time.deltaTime * openSpeed);
        }
    }
}
