using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Pagination
{
    public class HomePage : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
