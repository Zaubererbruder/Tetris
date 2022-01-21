using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts
{
    public class InputRouter : MonoBehaviour
    {
        private Level _level;
        private bool _movePressed;
        private float _moveInvokeDelay = 0.05f;
        private float _moveDelayCounter = 0;
        private int _moveDirection = 0;

        public void Init(Level level)
        {
            _level = level;
        }

        public void Update()
        {
            if(_movePressed)
            {
                _moveDelayCounter += Time.deltaTime;
                if (_moveDelayCounter >= _moveInvokeDelay)
                {
                    _level.DoMove(_moveDirection);
                    _moveDelayCounter = 0;
                }
            }
        }

        public void Move(CallbackContext callback)
        {
            _moveDirection = (int)callback.ReadValue<float>();
            if(callback.started)
            {
                _level.DoMove(_moveDirection);
            }
            if(callback.performed)
            {
                _movePressed = true;
            }
            if (callback.canceled)
            {
                _movePressed = false;
            }
        }

        public void AccelerateFall(CallbackContext callback)
        {
            if(callback.performed)
            {
                _level.AccelerateFall();
            }
            if(callback.canceled)
            {
                _level.CancelAcceleration();
            }
        }

        public void Rotate(CallbackContext callback)
        {
            if(callback.performed)
            {
                _level.DoRotate(true);
            }
        }
    }
}
