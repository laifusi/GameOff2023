using System;
using UnityEngine;

namespace _Scripts
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] float parallaxValue;
        [SerializeField] Camera cam;

        private float initialXPos;
        private float spriteWidth;
        private Vector3 currentCamPos;
        private float temp;
        private float distance;
        private Vector3 newPos;

        private void Start()
        {
            initialXPos = transform.position.x;
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        }



        private void Update()
        {
            currentCamPos = cam.transform.position;
            temp = currentCamPos.x * (1 - parallaxValue);
            distance = currentCamPos.x * parallaxValue;

            newPos = new Vector3(initialXPos + distance, transform.position.y, transform.position.z);

            transform.position = newPos;

            if (temp > initialXPos + (spriteWidth / 2))
            {
                initialXPos += spriteWidth;
            }
            else if (temp < initialXPos - (spriteWidth / 2))
            {
                initialXPos -= spriteWidth;
            }
        }
    }
}
