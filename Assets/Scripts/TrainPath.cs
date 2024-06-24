using System.Collections.Generic;
using UnityEngine;

public class TrainPath : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    public bool isStopped = false;
    public string stopAtWaypointName = "StopWaypoint"; // 처음 멈출 웨이포인트의 이름
    private bool stopFunctionDisabled = false; // 멈추는 기능 비활성화 상태

    void Update()
    {
        if (isStopped || waypoints.Count == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            // 웨이포인트에 도착했을 때 처리
            if (targetWaypoint.name == stopAtWaypointName && !stopFunctionDisabled)
            {
                isStopped = true;
                stopFunctionDisabled = true; // 멈추는 기능 비활성화
                return; // 멈추고 다음 로직은 수행하지 않음
            }

            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0; // 루프
            }

            // 멈추는 기능이 비활성화된 상태에서 다른 StopWaypoint에 도착했을 때 다시 활성화
            if (targetWaypoint.name == stopAtWaypointName)
            {
                stopFunctionDisabled = false; // 멈추는 기능 활성화
            }
        }
        else
        {
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed * 0.2f);
        }
    }

    // 외부에서 호출하여 열차를 다시 움직이게 하는 메서드
    public void Resume()
    {
        isStopped = false;
    }
}