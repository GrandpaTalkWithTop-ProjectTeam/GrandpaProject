using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyOnScript : MonoBehaviour
{
    private bool isEscapePressed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEscapePressed = true;
            Invoke("DisableObject", 0.2f); // 1�� �Ŀ� DisableObject �Լ��� ȣ���մϴ�.
        }


    }

    void DisableObject()
    {
        if (isEscapePressed)
        {
            gameObject.SetActive(false); // 1�� �Ŀ� �ش� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        }
    }
}
