using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBatteryInteract : MonoBehaviour
{
    public Swinging sw;
    public GameObject batObject;
    public Rigidbody rb;
    private bool prior;
    private float prdel;
    private float matdel;
    public Material changeMaterial;
    private Material originMaterial;


    // Start is called before the first frame update
    void Start()
    {
        originMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (sw.catchHitObject == this.gameObject)
        {
            prior = true;
            rb.useGravity = true;
        }
        else
        {
            prior = false;
        }

        if (this.transform.parent == batObject.transform)
        {
            matdel = 0.1f;
        }
    }

    private void FixedUpdate()
    {
        if (prdel > 0)
        {
            
        }
        else
        {
            prior = false; 
        }

        if (matdel > 0)
        {
            matdel -= Time.smoothDeltaTime;

            if (!prior)
            {
                GetComponent<Renderer>().material = changeMaterial;
            }

        }
        else
        {
            if (!prior)
            {
                GetComponent<Renderer>().material = originMaterial;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == batObject) && (!prior))
        {
            this.transform.SetParent(batObject.transform);
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.position = batObject.transform.position;
            transform.rotation = batObject.transform.rotation;
            
        }

    }
}
