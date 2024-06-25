using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBox : MonoBehaviour
{
    private float CTcool = 5f;

    [SerializeField]
    private GameObject BTrain;

    [SerializeField]
    private Transform ct;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CTcool > 0)
            CTcool -= Time.smoothDeltaTime;
        else
        {
            GameObject clone = Instantiate(
                BTrain,
                new Vector3(0, 0, 0),
                Quaternion.Euler(-90, 90, 0)
            );
            clone.transform.parent = ct.transform;
            clone.SetActive(true);

            CTcool = 5f;
        }
    }
}
