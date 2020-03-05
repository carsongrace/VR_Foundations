using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabber : MonoBehaviour
{
    private bool _grabbingObject;
    private bool _intersectingObject;
    public float throwForce = 10;
  //  public OVRInput.Controller touchController;

    public bool userGrab;
    private GameObject grabbedObject;

    public Material canGrabMaterial;
    private Material _savedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _grabbingObject = false;
        grabbedObject = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (!userGrab && _grabbingObject)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            _grabbingObject = false;
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_intersectingObject)
        {
            _savedMaterial = other.gameObject.GetComponent<Renderer>().material;
            other.gameObject.GetComponent<Renderer>().material = canGrabMaterial;
            _intersectingObject = true;
        }


    }

    public void OnTriggerStay(Collider other)
    {
        if (userGrab && !_grabbingObject)
        {
            if (other.gameObject.CompareTag("Grabbable"))
            {
                _grabbingObject = true;
                grabbedObject = other.gameObject;
                grabbedObject.transform.SetParent(this.transform);
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }



    }

    public void OnTriggerExit(Collider other)
    {
        if (_intersectingObject)
        {
            other.gameObject.GetComponent<Renderer>().material = _savedMaterial;
           // GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller)* 3;
            _intersectingObject = false;
        }

    }

}
