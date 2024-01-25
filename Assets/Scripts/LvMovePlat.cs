using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvMovePlat : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.parent == transform)
        {

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                float lastFrameTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; // �ִϸ��̼� Ŭ���� ���̸� ������
                animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, lastFrameTime);
                // �ִϸ��̼��� ������ �����ӿ� �����ϸ� ���ǵ带 0���� ����
                animator.speed = 0;
            }
            else
                animator.speed = 1f;
        }
        else
        {
            animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 0); // ���� �ִϸ��̼� Ŭ���� 0���������� ���
            animator.speed = 0;
        }
    }
}
