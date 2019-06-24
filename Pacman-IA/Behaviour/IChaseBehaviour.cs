
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pacman_IA.Behaviour
{
    interface IChaseBehaviour
    {
        Dictionary<string, int> Chase(Vector2 lastDirection);
    }
}
