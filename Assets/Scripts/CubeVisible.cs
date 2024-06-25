using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisible : MonoBehaviour
{
    public Animator animator;
    public Animator anima;

    [SerializeField]
    private GameObject cb;

    [SerializeField]
    private GameObject cb2;

    void Start()
    {
        anima.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.2f)
        {
            cb.SetActive(true);
            cb2.SetActive(false);
        }
        else
        {
            cb.SetActive(false);
            cb2.SetActive(true);
        }
    }
}
