using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CatchableRopeScript : MonoBehaviour
{
    private Animator animator;
    public string newLayerName = "whatIsGrappleable";
    private Rigidbody rb;

    public bool throwed = false;
    public bool attached = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.speed = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collObject = collision.gameObject;

        if (collObject.layer != LayerMask.NameToLayer("whatIsPlayer"))
        {
            if (throwed && !attached)
            {
                if (collision.contacts.Length > 0)
                {
                    ContactPoint contact = collision.contacts[0];
                    Vector3 hitNormal = contact.normal;

                    // �浹 ǥ���� ���� ���ͷ� ȸ���� ���
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, hitNormal);

                    // ���� ������Ʈ�� ȸ������ ���� ȸ��
                    transform.rotation = rotation;

                    rb.isKinematic = true;

                    animator.speed = 1.0f;
                    attached = true;

                    transform.SetParent(collision.transform);
                }
            }
        }
    }

    void FixedUpdate()
    {
        // �ִϸ��̼��� ������ �ִϸ��̼� �ӵ��� 0���� ����
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            GoToEndFrame();

            if (throwed == false)
                GoToStartFrame();
        }
    }

    void GoToEndFrame()
    {
        // �ִϸ��̼��� �� ���������� �̵�
        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 0.9f);
        animator.speed = 0f;
    }

    void GoToStartFrame()
    {
        // �ִϸ��̼��� �� ���������� �̵�
        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 0f);
        animator.speed = 0f;
    }
}
