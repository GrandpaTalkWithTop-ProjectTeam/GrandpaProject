using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElebatorButton : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도

    private float upperLimit = 10f; // 위로 이동 가능한 최대 높이
    private float lowerLimit = 0f; // 아래로 이동 가능한 최소 높이

    public float Limiter = 10f;
    public bool moveUp = false; // 위로 이동 중인지 여부를 나타내는 플래그
    Vector3 currentPosition;

    [HideInInspector]
    public Material Testm1;
    public Material Testm2;

    private Renderer rend;

    private float mdel;


    private void Start()
    {
        currentPosition = transform.parent.transform.position;
        lowerLimit = currentPosition.y;
        upperLimit = currentPosition.y+Limiter;

        rend = gameObject.GetComponent<Renderer>();
        Testm1 = rend.material;
    }

    void FixedUpdate()
    {
        // 움직임 제어
        MoveUpDown();

        if (mdel > 0)
        {

            rend.material = Testm2;
            mdel--;

            moveUp = true;

        }
        else
        {

            rend.material = Testm1;
            
            moveUp = false;
        }

    }

    void MoveUpDown()
    {
        // 현재 위치를 저장
        

        // 위 방향으로 이동할 양 계산
        float moveAmount = moveSpeed * Time.deltaTime;

        // moveUp 플래그에 따라 위 또는 아래로 이동
        if (moveUp)
        {
            currentPosition.y = Mathf.Min(currentPosition.y + moveAmount, upperLimit);
        }
        else
        {
            currentPosition.y = Mathf.Max(currentPosition.y - moveAmount, lowerLimit);
        }

        // 새로운 위치로 이동
        transform.parent.transform.position = currentPosition;
    }



    private void OnTriggerStay(Collider collision)
    {
        int targetLayer = LayerMask.NameToLayer("whatIsCatchable");

        if (collision.gameObject.layer == targetLayer)
        {

            mdel = 3f;

        }
    }
}

