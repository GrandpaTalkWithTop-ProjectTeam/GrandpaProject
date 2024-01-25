using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionPrint : MonoBehaviour
{
    public float constantSize = 5f; // ���ϴ� ��� ũ��
    public Camera pCamera;

    void Update()
    {
        // ī�޶���� ������� �Ÿ��� ���� ũ�� ����
        Vector3 dir = pCamera.transform.position - transform.position;
        float distance = Vector3.Distance(pCamera.transform.position, transform.position);
        float sizeFactor = distance / constantSize;
        transform.localScale = new Vector3(sizeFactor, sizeFactor, sizeFactor);
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
