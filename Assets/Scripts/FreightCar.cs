using UnityEngine;
using System.Collections.Generic;

public class FreightCar : MonoBehaviour
{
    public Transform train; // 메인 기차의 Transform
    public float followDelay = 1.0f; // 화물차가 기차를 따라가는 시간 지연 (초 단위)
    private Queue<Vector3> positionQueue = new Queue<Vector3>(); // 기차의 위치를 저장할 큐
    private Queue<Quaternion> rotationQueue = new Queue<Quaternion>(); // 기차의 회전을 저장할 큐
    private float timer = 0f; // 타이머 변수
    private float updateInterval = 0.1f; // 위치와 회전을 기록할 시간 간격
    public TrainPath tp;

    void Start()
    {
        // 초기화 시 큐를 비웁니다.
        positionQueue.Clear();
        rotationQueue.Clear();

        // 초기화 시점에 기차의 위치와 회전을 기록합니다.
        for (float t = 0; t < followDelay; t += updateInterval)
        {
            positionQueue.Enqueue(train.position);
            rotationQueue.Enqueue(train.rotation);
        }
    }

    void FixedUpdate()
    {
        if (tp.isStopped == false)
        {
            // 기차의 현재 위치와 회전을 큐에 저장합니다.
            positionQueue.Enqueue(train.position);
            rotationQueue.Enqueue(train.rotation);

            // 오래된 위치와 회전 정보를 큐에서 제거하고 화물차에 적용합니다.
            if (positionQueue.Count > Mathf.CeilToInt(followDelay / updateInterval))
            {
                transform.position = positionQueue.Dequeue();
                transform.rotation = rotationQueue.Dequeue();
            }

        }

        
    }
}