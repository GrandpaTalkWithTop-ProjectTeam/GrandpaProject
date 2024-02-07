using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGearScript : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // ������Ʈ�� Z���� �������� ���������� ȸ����Ű�� �ڵ�
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
    }
}
