using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float _fallsInSecond = 2;
        [SerializeField] private float _acellerationRate = 5;
        [SerializeField] private BlockPresenter _blockPrefab;

        public float FallsInSecond => _fallsInSecond;
        public float AcellerationRate => _acellerationRate;
        public BlockPresenter BlockPrefab => _blockPrefab;
    }
}
