using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ObjectPingPong : MonoBehaviour
{
    public float startPosition = 0f; // начальна€ позици€ объекта
    public float targetPosition = 100f; // конечна€ позици€ объекта
    public float duration = 1f; // врем€ перемещени€ (в секундах)
    public float pauseDuration = 1f; // длительность паузы (в секундах)

    private IEnumerator Start()
    {

        while (true)
        {
            // движение вправо
            yield return LerpPosition(startPosition, targetPosition, duration);

            // пауза
            yield return new WaitForSeconds(pauseDuration);

            // движение влево
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

        // ”становка окончательной позиции (это нужно, чтобы избежать небольшой погрешности)
        transform.localPosition = new Vector3(end, transform.localPosition.y, transform.localPosition.z); ;
    }
}

