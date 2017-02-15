using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{

    /// <summary>
    /// Define uma heuristica utilizada no algoritmo AStar
    /// </summary>
    public class Heuristic
    {
        Vector2 end;
        public Heuristic(Vector2 endNode)
        {
            end = endNode;
        }

        public float GetEstimatedCost(Vector2 startNode)
        {
            return (end - startNode).Length();
        }
    }
}
