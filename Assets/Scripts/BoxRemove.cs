using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRemove : MonoBehaviour
{
    public GameObject targetObject;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator anima;
    public GameObject pl;

    private bool isRemoved = false;

    void Start()
    {
        anima.speed = 0f;
    }

    void Update()
    {
        if (targetObject == null || isRemoved)
            return;

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            DetachAndAddComponents();
            anima.speed = 2f;
            isRemoved = true;
        }
    }

    private void DetachAndAddComponents()
    {
        if (targetObject.transform.parent != null)
        {
            targetObject.transform.parent = null;
        }

        targetObject.AddComponent<TimerDelete>();
        Rigidbody crb = targetObject.AddComponent<Rigidbody>();
        crb.useGravity = true;
    }
}