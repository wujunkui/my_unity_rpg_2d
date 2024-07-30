using System;
using UnityEngine;

namespace Environment
{
    public class WaterEffect : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;

        private void OnValidate()
        {
            transform.position= new Vector3(mainCamera.position.x, transform.position.y);
        }

        private void Update()
        {
            transform.position= new Vector3(mainCamera.position.x, transform.position.y);
        }
    }
}