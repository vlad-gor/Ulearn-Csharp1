using System;


namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveRight(robot, width);
            MoveDown(robot, height);
        }

        private static void MoveRight(Robot robot, int count)
        {
            for (int i = 0; i < count - 3; i++)
            {
                robot.MoveTo(Direction.Right);
            }
        }

        private static void MoveDown(Robot robot, int count)
        {
            for (int i = 0; i < count - 3; i++)
            {
                robot.MoveTo(Direction.Down);
            }
        }
    }
}
