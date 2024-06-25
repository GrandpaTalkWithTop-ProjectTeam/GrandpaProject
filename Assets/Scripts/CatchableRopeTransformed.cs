using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchableRopeTransformed : MonoBehaviour
{
    private CatchableRopeScript CRscript;
    public string newLayerName = "whatIsGrappleable";

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = transform.parent.gameObject;
        CRscript = parentObject.GetComponent<CatchableRopeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CRscript.throwed == true)
        {
            gameObject.layer = LayerMask.NameToLayer("whatIsGrappleable");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
