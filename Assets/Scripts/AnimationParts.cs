using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimationParts : MonoBehaviour
{
    public Transform[] imageTransform; // ������ � ��������� ��� ������ ��������
    public Vector3[] imagePos; // ������ � ��������� ��� ������ ��������
    public float animationSpeed = 1f; // �������� ��������
    private int currentImageIndex; // ������ ������� ��������
    private bool isAnimating = false; // ����, ����������� �� ��, ���� �� ��������

    private void Start()
    {
        imagePos = new Vector3[imageTransform.Length];

        for (int i = 0; i < imageTransform.Length; i++)
        {
            imagePos[i] = imageTransform[i].position;
        }
    }

    public void StartSystemPoints()
    {
        currentImageIndex = 0;

        // ������������� ��� �������� � ��������� �������
        for (int i = 0; i < imageTransform.Length; i++)
        {
            imageTransform[i].gameObject.SetActive(false);
            imageTransform[i].position = imagePos[i];
        }

        isAnimating = true;
        StartCoroutine(AnimateImages());
        
    }

    private IEnumerator AnimateImages()
    {
        for (int i = 0; i < imageTransform.Length; i++)
        {
            // ������ ���������� ������� �������� � ��������� �������
            StartCoroutine(MoveImage(imageTransform[currentImageIndex], imageTransform[i]));

            // ������� ���������� ����������� ������� ��������
            yield return new WaitForSeconds(animationSpeed);

            // ���������� ��������� �������� � ������������� �� �� ����� �������
            imageTransform[i].gameObject.SetActive(true);
            imageTransform[currentImageIndex].position = imageTransform[i].position;

            // ��������� � ��������� ��������
            currentImageIndex = i;
        }

        isAnimating = false;
    }

    private IEnumerator MoveImage(Transform from, Transform to)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationSpeed)
        {
            // ������������� ������� �� from �� to � ������� Lerp
            from.position = Vector3.Lerp(from.position, to.position, elapsedTime / animationSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}