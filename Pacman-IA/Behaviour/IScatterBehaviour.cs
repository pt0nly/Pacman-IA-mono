using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_IA.Behaviour
{
    interface IScatterBehaviour
    {
        Dictionary<string, int> Scatter(Vector2 lastDirection);
        int checkScatter(Point direction);
    }
}
