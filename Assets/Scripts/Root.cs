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
    public class Root : MonoBehaviour
    {
        [SerializeField] private BlockPresenterFactory _blockFactory;
        [SerializeField] private InputRouter _inputRouter;
        private Level _level;

        public void Awake()
        {
            _level = new Level();
            _inputRouter.Init(_level);
        }

        public void Start()
        {
            _level.StartGame();
        }

        public void OnEnable()
        {
            _level.BrickLaunched += CreateBrick;
        }

        public void OnDisable()
        {
            _level.BrickLaunched -= CreateBrick;
        }

        public void Update()
        {
            _level.Update(Time.deltaTime);
        }

        private void CreateBrick(Brick brick)
        {
            foreach (var block in brick.Blocks)
            {
                _blockFactory.Create(block);
            }
        }
    }
}