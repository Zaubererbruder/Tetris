using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField] private Text _scoreUI;

        public void ScoreChangedHandler(int score)
        {
            _scoreUI.text = $"Score: {score}";
        }
    }
}
