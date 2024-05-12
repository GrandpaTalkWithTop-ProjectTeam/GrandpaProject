using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkyOnScript : MonoBehaviour
{
    private bool isEscapePressed = false;
    public Animator animm;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableReady();
        }


    }

    void DisableObject()
    {
        if (isEscapePressed)
        {
            gameObject.SetActive(false); 
        }
    }

    public void DisableReady()
    {
        isEscapePressed = true;
        animm.SetBool("TalkyDown", false);
        Invoke("DisableObject", 0.5f); // 1�� �Ŀ� DisableObject �Լ��� ȣ���մϴ�.
    }
}
