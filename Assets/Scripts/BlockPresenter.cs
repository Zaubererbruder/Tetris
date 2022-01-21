using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.Bricks;
using Assets.Scripts.Model;

namespace Assets.Scripts
{
    public class BlockPresenter : MonoBehaviour
    {
        private Block _block;
        private Transform _transform;
        private SpriteRenderer _renderer;

        public void Awake()
        {
            _transform = GetComponent<Transform>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Block block)
        {
            _block = block;
            _renderer.color = _block.Color;
            enabled = true;
        }

        public void OnEnable()
        {
            _block.BlockMoved += MoveBlock;
            _block.BlockDestroyed += DestroyBlock;
        }

        public void OnDisable()
        {
            _block.BlockMoved -= MoveBlock;
            _block.BlockDestroyed -= DestroyBlock;
        }

        private void DestroyBlock()
        {
            Destroy(gameObject);
        }

        private void MoveBlock()
        {
            _transform.position = PositionTranslator.ToUnityPosition(_block.Position);
        }
    }
}
