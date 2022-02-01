using System.Reflection;
using Assets.Scripts.Model.Bricks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.Settings
{
    public class GameSettings
    {
        private Dictionary<string, object> _settings = new Dictionary<string, object>();

        
        [DefaultValue(12)] public int LevelWidth { get => GetValue<int>(); set => SetValue(value); }
        [DefaultValue(24)] public int LevelHeight { get => GetValue<int>(); set => SetValue(value); }
        [DefaultValue(2f)] public float FallsInSecond { get => GetValue<float>(); set => SetValue(value); }
        [DefaultValue(5f)] public float AccelerationRate { get => GetValue<float>(); set => SetValue(value); }

        [DefaultValue(typeof(DefaultColorRandomizer))] 
        public IColorPicker ColorPicker { get => GetValue<IColorPicker>(); set => SetValue(value); }

        [DefaultValue(typeof(DefaultBrickPatternRandomizer))] 
        public IBrickPatternPicker BrickPatternPicker { get => GetValue<IBrickPatternPicker>(); set => SetValue(value); }

        [DefaultValue(typeof(DefaultOnStartBlockGenerator))]
        public IOnStartBlockGenerator OnStartBlockGenerator { get => GetValue<IOnStartBlockGenerator>(); set => SetValue(value); }


        private T GetValue<T>([CallerMemberName] string key = "")
        {
            return (T)_settings.GetValueOrDefault(key, GetDefaultValue<T>(key));
        }

        private void SetValue(object value, [CallerMemberName] string key = "")
        {
            if (_settings.ContainsKey(key))
                _settings[key] = value;
            else
                _settings.Add(key, value);
        }

        private T GetDefaultValue<T>(string setName)
        {
            var pInfo = typeof(GameSettings).GetProperty(setName);
            var defAttr = pInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (defAttr.Value is not Type)
                return (T)defAttr.Value;

            var res = (T)Activator.CreateInstance((Type)defAttr.Value);
            return res;
        }
    }
}
