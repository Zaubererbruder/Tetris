using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class PositionTranslator : MonoBehaviour
    {
        [SerializeField] private float _scale = 0.5f;
        [SerializeField] private float _offsetY = 12;
        [SerializeField] private float _offsetX = 12;
        [SerializeField] private float _patternXOffset;
        [SerializeField] private float _patternYOffset;
        private Transform _transform;
        private Vector3 _levelPosition => _transform.position;

        public void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public Vector3 ToUnityPosition(Position position)
        {
            return new Vector3(_levelPosition.x + _offsetX + position.X * _scale, _levelPosition.y + _offsetY - position.Y * _scale);
        }

        public Vector3 GetPatternPosition(Position offset)
        {
            return new Vector3(_levelPosition.x + _patternXOffset + (offset.X * _scale), _levelPosition.y + _patternYOffset + (-offset.Y * _scale));
        }
    }
}
