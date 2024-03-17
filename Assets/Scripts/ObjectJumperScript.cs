using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJumperScript : MonoBehaviour
{
    public float jumpForce = 10f;
    private float jDel = 10f;
    public LayerMask targetLayers;

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
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jDel = 1f;
            }
            
        }
    }
}