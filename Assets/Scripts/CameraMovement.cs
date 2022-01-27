using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void MoveCameraByX(float x)
        {
            _transform.position = new Vector3(x, _transform.position.y, _transform.position.z);
        }

    }
}