using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNormal : MonoBehaviour
{
    [Header("Crane Option")]
    public float coolTime = 5f;
    private float CTcool = 1f;
    public float deathTime = 300f;
    public float speed = 20f;

    [Header("Crane Object")]
    [SerializeField]
    private GameObject Cr;

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
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z)
            );
            clone.transform.parent = this.transform;
            clone.SetActive(true);

            CNormalMove cnm = clone.GetComponent<CNormalMove>();

            cnm.deathTime = deathTime;
            cnm.speed = speed;

            CTcool = coolTime;
        }
    }
}
