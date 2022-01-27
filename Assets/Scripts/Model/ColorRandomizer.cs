
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class ColorRandomizer
    {
        private IReadOnlyList<Color> _colorsList;

        public ColorRandomizer()
        {
            var list = new List<Color>();
            list.Add(Color.red);
            list.Add(Color.green);
            list.Add(Color.blue);
            list.Add(Color.yellow);
            list.Add(Color.cyan);
            _colorsList = list;
        }

        public Color GetRandomColor()
        {
            return _colorsList[Random.Range(0, _colorsList.Count)];
        }
    }
}
