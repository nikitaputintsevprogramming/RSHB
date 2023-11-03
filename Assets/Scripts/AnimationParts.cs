using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Pagination
{
    public class AnimationParts : MonoBehaviour
    {
        public RectTransform[] imageTransform; // Массив с позициями для каждой картинки
        public Vector3[] imagePos; // Массив с позициями для каждой картинки
        public float animationSpeed = 1f; // Скорость анимации
        private int currentImageIndex; // Индекс текущей картинки
        private bool isAnimating = false; // Флаг, указывающий на то, идет ли анимация

        [SerializeField] private GameObject _pageRect;
        [SerializeField] private bool checkPos;

        private void Start()
        {

            imagePos = new Vector3[imageTransform.Length];

            for (int i = 0; i < imageTransform.Length; i++)
            {
                imagePos[i] = imageTransform[i].localPosition;
            }
        }

        public void StartSystemPoints()
        {
            if (isAnimating)
                return;

            currentImageIndex = 0;

            // Устанавливаем все картинки в начальную позицию
            for (int i = 0; i < imageTransform.Length; i++)
            {
                imageTransform[i].gameObject.SetActive(false);
                imageTransform[i].localPosition = imagePos[i];
            }

            isAnimating = true;
            StartCoroutine(AnimateImages());
        }

        private IEnumerator AnimateImages()
        {
            for (int i = 0; i < imageTransform.Length; i++)
            {
                // Плавно перемещаем текущую картинку к следующей позиции
                StartCoroutine(MoveImage(imageTransform[currentImageIndex], imageTransform[i]));

                // Ожидаем завершения перемещения текущей картинки
                yield return new WaitForSeconds(animationSpeed);

                // Показываем следующую картинку и устанавливаем ее на место текущей
                imageTransform[i].gameObject.SetActive(true);
                imageTransform[currentImageIndex].position = imageTransform[i].position;

                // Переходим к следующей картинке
                currentImageIndex = i;
            }

            isAnimating = false;
        }

        private IEnumerator MoveImage(Transform from, Transform to)
        {
            float elapsedTime = 0f;

            while (elapsedTime < animationSpeed)
            {
                // Интерполируем позицию от from до to с помощью Lerp
                from.position = Vector3.Lerp(from.position, to.position, elapsedTime / animationSpeed);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}