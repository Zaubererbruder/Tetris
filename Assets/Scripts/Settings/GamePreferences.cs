using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GamePreferences : ScriptableObject
    {
        [SerializeField] private BlockPresenter _blockPrefab;

        public BlockPresenter BlockPrefab => _blockPrefab;
    }
}
