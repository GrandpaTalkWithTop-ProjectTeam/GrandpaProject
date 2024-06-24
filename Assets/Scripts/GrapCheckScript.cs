using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapCheckScript : MonoBehaviour
{
    public bool grapSwitchOn = false;
    public LayerMask targetLayer;


    private void OnCollisionStay(Collision collision)
    {
  
            // 충돌한 오브젝트가 B 레이어에 속하는지 확인
            if (collision.gameObject.layer == targetLayer)
            {
                // 충돌한 오브젝트의 스크립트에 접근하여 cCheck 변수를 true로 변경
                SphereGrapScript scriptOnCollidedObject = collision.gameObject.GetComponent<SphereGrapScript>();
                if (scriptOnCollidedObject != null)
                {
                    // cCheck 변수를 true로 변경
                    scriptOnCollidedObject.switchOn = true;
                }
            }


    }
}