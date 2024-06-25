using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrane2 : MonoBehaviour
{
    private float CTcool = 7f;

    [SerializeField]
    private GameObject Cr;

    [SerializeField]
    private Transform ct;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CTcool > 0)
            CTcool -= Time.smoothDeltaTime;
        else
        {
            GameObject clone = Instantiate(
                Cr,
                this.transform.position,
                Quaternion.Euler(-90, 0, 90)
            );
            clone.transform.parent = ct.transform;
            clone.SetActive(true);

            CTcool = 5f;
        }
    }
}
