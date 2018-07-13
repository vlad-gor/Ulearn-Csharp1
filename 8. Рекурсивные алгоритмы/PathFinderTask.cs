using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(
                                Point[] checkpoints)
        {
            double shortestDistance = double.PositiveInfinity;
            int[] order = new int[checkpoints.Length];
            int[] bestOrder = new int[checkpoints.Length];
            MakePermutations(order, 1, checkpoints, ref shortestDistance, ref bestOrder);
            return bestOrder;
        }

        public static int[] MakePermutations(int[] order, int position, Point[] checkpoints,
            ref double shortestDistance, ref int[] bestOrder)
        {
            var currentOrder = new int[position];
            Array.Copy(order, currentOrder, position);
            var pathLength = PointExtensions.GetPathLength(checkpoints, currentOrder);

            if (pathLength < shortestDistance)
            {
                if (position == order.Length)
                {
                    shortestDistance = pathLength;
                    bestOrder = (int[])order.Clone();
                    return order;
                }


                for (int i = 1; i < order.Length; i++)
                {
                    var index = Array.IndexOf(order, i, 0, position);
                    if (index != -1)
                        continue;
                    order[position] = i;
                    MakePermutations(order, position + 1, checkpoints, ref shortestDistance,
                        ref bestOrder);
                }
            }

            return order;
        }
    }
}