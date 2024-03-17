using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonpanArmScript : MonoBehaviour
{
    public float targetRotation = 90f;  // ��ǥ ȸ�� ����
    public float rotationSpeed = 30f;   // ȸ�� �ӵ�

    public bool Rotating;

    public GameObject wonpanPlate;

    private WonpanArmAnimate waa;


    private void Start()
    {
       waa = wonpanPlate.GetComponent<WonpanArmAnimate>();
    }

    void Update()
    {
        // ���� ȸ�� ����
        float currentRotation = transform.rotation.eulerAngles.y;

        // ��ǥ ������ ���� ���� ������ ���� ���̸� ���
        float angleDifference = Mathf.DeltaAngle(currentRotation, targetRotation+45f);

        // ȸ�� �ӵ��� ���� ���̸� �̿��Ͽ� ȸ��
        float rotationAmount = Mathf.Min(rotationSpeed * Time.deltaTime, Mathf.Abs(angleDifference));

        // ȸ�� ���� ����
        float rotationDirection = Mathf.Sign(angleDifference);

        // ȸ�� ����
        if (waa.doorclear)
        transform.Rotate(Vector3.up, rotationDirection * rotationAmount);



        if (Mathf.Abs(angleDifference) < 0.01f)
        {
            Rotating = false;
        }
        else
        {
            Rotating = true;
        }
    }
}