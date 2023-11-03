using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ObjectPingPong : MonoBehaviour
{
    public float startPosition = 0f; // ��������� ������� �������
    public float targetPosition = 100f; // �������� ������� �������
    public float duration = 1f; // ����� ����������� (� ��������)
    public float pauseDuration = 1f; // ������������ ����� (� ��������)

    private IEnumerator Start()
    {

        while (true)
        {
            // �������� ������
            yield return LerpPosition(startPosition, targetPosition, duration);

            // �����
            yield return new WaitForSeconds(pauseDuration);

            // �������� �����
            yield return LerpPosition(targetPosition, startPosition, duration);

            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private IEnumerator LerpPosition(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            transform.localPosition = new Vector3(Mathf.Lerp(start, end, t), transform.localPosition.y, transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������� ������������� ������� (��� �����, ����� �������� ��������� �����������)
        transform.localPosition = new Vector3(end, transform.localPosition.y, transform.localPosition.z); ;
    }
}

