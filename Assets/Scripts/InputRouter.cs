using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts
{
    public class InputRouter : MonoBehaviour
    {
        private bool _movePressed;
        private float _moveInvokeDelay = 0.1f;
        private float _moveDelayCounter = 0;
        private int _moveDirection = 0;

        public event Action<int> MovePressed;
        public event Action<bool> RotatePressed;
        public event Action AcceleratePressed;
        public event Action AccelerateReleased;


        public void Update()
        {
            if (_movePressed)
            {
                _moveDelayCounter += Time.deltaTime;
                if (_moveDelayCounter >= _moveInvokeDelay)
                {
                    MovePressed?.Invoke(_moveDirection);
                    _moveDelayCounter = 0;
                }
            }
        }

        public void Move(CallbackContext callback)
        {
            _moveDirection = (int)callback.ReadValue<float>();
            if(callback.started)
            {
                MovePressed?.Invoke(_moveDirection);
            }
            if(callback.performed)
            {
                _movePressed = true;
            }
            if (callback.canceled)
            {
                _movePressed = false;
                _moveDelayCounter = 0;
            }
        }

        public void AccelerateFall(CallbackContext callback)
        {
            if(callback.performed)
            {
                AcceleratePressed?.Invoke();
            }
            if(callback.canceled)
            {
                AccelerateReleased?.Invoke();
            }
        }

        public void Rotate(CallbackContext callback)
        {
            if(callback.performed)
            {
                RotatePressed?.Invoke(true);
            }
        }
    }
}
