using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGrapScript : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 initialRotation;
    public bool leftCheck;
    public bool switchOn=false;
    private int rotDir;
    public float rotNum=12;
    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        currentRotation = transform.localRotation.eulerAngles;
        initialRotation = transform.localRotation.eulerAngles;
        //currentRotation.z = transform.rotation.z;

        if (leftCheck)
            rotDir = 1;
        else
            rotDir = -1;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (switchOn)
        {
            currentRotation.z += ((initialRotation.z + rotNum*rotDir) - currentRotation.z) * 0.1f;
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        else
        {
            currentRotation.z += ((initialRotation.z) - currentRotation.z) * 0.1f;
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsActor"))
        {
            switchOn = true;
            target= other.gameObject;
            Invoke("DeactivateObject", 0.5f);
            

        }
            
        


    }

    void DeactivateObject()
    {
        if (target != null)
        target.SetActive(false);
    }
}
