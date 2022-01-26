using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameModeSwitcher : MonoBehaviour
    {
        [SerializeField] private Vector3 _levelNormalModePosition;
        [SerializeField] private Vector3 _level1DoubleModePosition;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void SetNormalMode()
        {
            _transform.position = _levelNormalModePosition;
        }

        public void SetDoubleMode()
        {
            _transform.position = _level1DoubleModePosition;
        }
    }
}
