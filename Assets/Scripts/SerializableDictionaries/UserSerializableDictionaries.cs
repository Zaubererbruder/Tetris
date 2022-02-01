using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.SerializableDictionaries
{
    [Serializable] public class DifficultyDictionary : SerializableDictionary<GameDifficulty, DifficultyPreset> { }
}
