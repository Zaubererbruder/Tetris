using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts
{
    public class BlockPresenterFactory : MonoBehaviour
    {
        [SerializeField] private BlockPresenter _blockComp;

        public void Create(Block block)
        {
            var instance = Instantiate(_blockComp, PositionTranslator.ToUnityPosition(block.Position), Quaternion.identity);
            instance.Init(block);
        }
    }
}

