using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayer
{
    public class BackgroundScroll : MonoBehaviour
    {
        public float speed;
        private Vector2 offset;
        private Material material;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            offset = new Vector2(speed, 0);
            material.mainTextureOffset += offset * Time.deltaTime;
        }
    }
}