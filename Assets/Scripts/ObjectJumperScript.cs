using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJumperScript : MonoBehaviour
{
    public float jumpForce = 10f;
    private float jDel = 0f;
    public LayerMask targetLayers;

    public Animator animator;
    public string aniClip;

    void Start()
    {

        if (animator != null)
        {
            // �ִϸ��̼��� �ӵ��� 0���� ����
            animator.speed = 0f;
        }
    }

    private void LateUpdate()
    {
        if (jDel>0)
            jDel-=Time.smoothDeltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // �浹�� ������Ʈ�� targetLayers�� ���Ե� ���̾ ���ϴ��� Ȯ��
        if (jDel<=0)
        if ((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            // �浹�� ������Ʈ�� �÷��̾��� ��쿡�� ���� ����
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                    animator.Play(aniClip, 0, 0f); // ���⼭ "YourAnimationName"�� ����� �ִϸ��̼��� �̸��Դϴ�.
                    animator.speed = 1f;
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jDel = 1f;
            }
            
        }
    }
}