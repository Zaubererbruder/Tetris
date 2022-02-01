using Assets.Scripts.SerializableDictionaries;
using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class ListOfLevelPresenterExtensions
    {
        public static void Init(this List<LevelPresenter> listOfLevels, GamePreferences gameSettings, DifficultyPreset difficultyPreset, InputRouter inputRouter)
        {
            foreach (var level in listOfLevels)
            {
                level.Init(gameSettings, difficultyPreset, inputRouter);
            }
        }

        public static void SetEnabled(this List<LevelPresenter> listOfLevels, bool enabled)
        {
            foreach (var level in listOfLevels)
            {
                level.enabled = enabled;
            }
        }

        public static ScoreAggregator GetScoreAggregator(this List<LevelPresenter> listOfLevels)
        {
            List<ScoreCounter> scList = new List<ScoreCounter>();
            foreach (var level in listOfLevels)
            {
                scList.Add(level.ScoreCounter);
            }
            return new ScoreAggregator(scList);
        }
    }
}
