using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public Transform clockHand; // ������ �� ������ ������� �����
    public float forwardAngle = 360f / 60f * 10f; // ���� �������� ��� ����������� ������ (10 �����)
    public float backwardAngle = -360f / 60f * 10f; // ���� �������� ��� ����������� ����� (10 �����)
    public float moveDuration = 1f; // ����� ����������� (� ��������)
    public float pauseDuration = 1f; // ������������ ����� (� ��������)

    private IEnumerator Start()
    {
        while (true)
        {
            // ����������� ������
            yield return MoveClockHandToAngle(forwardAngle, moveDuration);

            // �����
            yield return new WaitForSeconds(pauseDuration);

            // ����������� �����
            yield return MoveClockHandToAngle(backwardAngle, moveDuration);

            // �����
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private IEnumerator MoveClockHandToAngle(float targetAngle, float time)
    {
        Quaternion startRotation = clockHand.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 0f, targetAngle) * startRotation;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            clockHand.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������� �������������� �������� (��� �����, ����� �������� ��������� �����������)
        clockHand.rotation = endRotation;
    }
}
