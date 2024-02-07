using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNormalMove : MonoBehaviour
{
    public float speed = 20f; // �̵� �ӵ�
    private Rigidbody rb;
    public float deathTime = 300f;

    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // isKinematic�� true�� ����
    }

    void FixedUpdate()
    {
        // Ư�� �������� �̵�
        MoveForward();

        if (deathTime > 0)
            deathTime--;
        else
        {
            if (player.transform.parent == this.transform)
            {

                player.transform.parent = null;

            }

            Destroy(this.gameObject);
        }

    }

    void MoveForward()
    {
        // Rigidbody�� ���� �̿��Ͽ� Ư�� �������� �̵�
        Vector3 moveDirection = transform.parent.forward;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
    }
}