using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRemove : MonoBehaviour
{
    public GameObject targetObject;

    [SerializeField]
    private Animator anim;
    public GameObject pl;

    private bool rmatonce = false;
    public Animator anima;

    // Start is called before the first frame update
    void Start()
    {
        anima.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
            if (rmatonce == false)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    if (targetObject.transform.parent != null)
                    {
                        targetObject.transform.parent = null;
                        targetObject.AddComponent<TimerDelete>();
                    }

                    Rigidbody crb = targetObject.AddComponent<Rigidbody>();
                    crb.useGravity = true;

                    anima.speed = 2f;

                    rmatonce = true;
                }
            }
    }
}
