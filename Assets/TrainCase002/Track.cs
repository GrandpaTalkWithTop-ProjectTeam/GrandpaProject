using UnityEngine;

public abstract class Track : MonoBehaviour
{
    // �߻� �޼���: ���� Ŭ�������� �����ؾ� ��
    public abstract void MoveTrain(Train train, float speed);
}