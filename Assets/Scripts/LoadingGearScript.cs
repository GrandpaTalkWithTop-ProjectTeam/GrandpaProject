using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingGearScript : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // 오브젝트를 Z축을 기준으로 지속적으로 회전시키는 코드
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
    }
}
