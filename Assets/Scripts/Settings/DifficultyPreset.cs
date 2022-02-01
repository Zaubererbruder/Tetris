using Assets.Scripts.Model;
using Assets.Scripts.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/DifficultySettings", order = 2)]
    public class DifficultyPreset : ScriptableObject
    {
        [SerializeField] private float _fallsInSecond = 2;
        [SerializeField] private float _acellerationRate = 5;
        [SerializeField] private int _fillRowsOnStartCount = 0;

        public GameSettings GetGameSettings()
        {
            return new GameSettings()
            {
                FallsInSecond = _fallsInSecond,
                AccelerationRate = _acellerationRate,
                OnStartBlockGenerator = new OnStartBlockGeneratorNormalDif() { Rows = _fillRowsOnStartCount }
            };
        }
    }

    public enum GameDifficulty
    {
        Easy = 0,
        Normal,
        Hard
    }
}
