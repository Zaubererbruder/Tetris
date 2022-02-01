using Assets.Scripts.SerializableDictionary.Editor;
using UnityEditor;

namespace Assets.Scripts.SerializableDictionaries.Editor
{
    [CustomPropertyDrawer(typeof(DifficultyDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

}
