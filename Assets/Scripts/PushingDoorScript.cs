using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushingDoorScript : MonoBehaviour
{
    public bool switchOn;    // ���� A
    public float distanceB;   // ���� B (�Ÿ�)
    public float speed;       // �̵� �ӵ�
    private Vector3 initialPosition;  // �ʱ� ��ġ

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    void Update()
    {
        if (switchOn)
        {
            // ���� A�� true�� �� �ʱ� ��ġ�� �������� x������ �̵�
            MoveObject();
        }
    }

    void MoveObject()
    {
        // ���� ��ġ�� ������
        Vector3 currentPosition = transform.position;

        // x������ �ʱ� ��ġ�� �������� distanceB��ŭ �̵�
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, initialPosition.x + distanceB, speed * Time.deltaTime);

        // ���ο� ��ġ�� ����
        transform.position = currentPosition;
    }
}