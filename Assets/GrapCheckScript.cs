using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapCheckScript : MonoBehaviour
{
    public bool grapSwitchOn = false;
    public LayerMask targetLayer;


    private void OnCollisionStay(Collision collision)
    {
  
            // �浹�� ������Ʈ�� B ���̾ ���ϴ��� Ȯ��
            if (collision.gameObject.layer == targetLayer)
            {
                // �浹�� ������Ʈ�� ��ũ��Ʈ�� �����Ͽ� cCheck ������ true�� ����
                SphereGrapScript scriptOnCollidedObject = collision.gameObject.GetComponent<SphereGrapScript>();
                if (scriptOnCollidedObject != null)
                {
                    // cCheck ������ true�� ����
                    scriptOnCollidedObject.switchOn = true;
                }
            }


    }
}