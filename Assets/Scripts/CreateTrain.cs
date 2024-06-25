using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrain : MonoBehaviour
{
    private float CTcool = 1f;
    public bool BTrain_Call = false;

    [SerializeField]
    private GameObject CTrain;

    [SerializeField]
    private GameObject BTrain;

    [SerializeField]
    private Transform ct;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CTcool > 0)
            CTcool -= Time.smoothDeltaTime;
        else
        {
            if (BTrain_Call == false)
            {
                GameObject clone = Instantiate(CTrain, new Vector3(0, 0, 0), Quaternion.identity);
                clone.transform.parent = ct.transform;
                clone.SetActive(true);
            }

            if (BTrain_Call == true)
            {
                GameObject clone = Instantiate(BTrain, new Vector3(0, 0, 0), Quaternion.identity);
                clone.transform.parent = ct.transform;
                clone.SetActive(true);
                BTrain_Call = false;
            }

            CTcool = 10f;
        }
    }
}
