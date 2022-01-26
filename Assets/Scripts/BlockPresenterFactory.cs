using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.Settings;

namespace Assets.Scripts
{
    public class BlockPresenterFactory : MonoBehaviour
    {
        private GameSettings _settings;

        public void Init(GameSettings settings)
        {
            _settings = settings;
        }

        public void Create(Block block, PositionTranslator positionTranslator, Transform parent)
        {
            var instance = Instantiate(_settings.BlockPrefab, positionTranslator.ToUnityPosition(block.Position), Quaternion.identity, parent);
            instance.Init(block, positionTranslator);
        }
    }
}

