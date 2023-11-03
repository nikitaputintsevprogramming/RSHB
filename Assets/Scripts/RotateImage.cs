using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public Transform clockHand; // ссылка на объект стрелки часов
    public float forwardAngle = 360f / 60f * 10f; // угол поворота для перемещения вперед (10 минут)
    public float backwardAngle = -360f / 60f * 10f; // угол поворота для перемещения назад (10 минут)
    public float moveDuration = 1f; // время перемещения (в секундах)
    public float pauseDuration = 1f; // длительность паузы (в секундах)

    private IEnumerator Start()
    {
        while (true)
        {
            // перемещение вперед
            yield return MoveClockHandToAngle(forwardAngle, moveDuration);

            // пауза
            yield return new WaitForSeconds(pauseDuration);

            // перемещение назад
            yield return MoveClockHandToAngle(backwardAngle, moveDuration);

            // пауза
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

        // Установка окончательного поворота (это нужно, чтобы избежать небольшой погрешности)
        clockHand.rotation = endRotation;
    }
}
