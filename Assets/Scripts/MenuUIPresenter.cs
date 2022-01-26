using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class MenuUIPresenter :  MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _difficultyText;

        public void ExitButtonPressed()
        {
            Application.Quit();
        }

        public void SliderValueChanged(float value)
        {
            _difficultyText.text = ((DifficulryLevels)(int)value).ToString();
        }
    }

    public enum DifficulryLevels
    {
        Easy = 0,
        Normal,
        Hard
    }
}
