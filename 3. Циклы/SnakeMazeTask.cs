using System;


namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveRight(Robot robot, int width, int height)
        {
            for (int i = 0; i < width - 3; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveDown(Robot robot, int width, int height)
        {
            for (int i = 0; i < 2; i++)
                robot.MoveTo(Direction.Down);
        }

        public static void MoveLeft(Robot robot, int width, int height)
        {
            for (int i = 0; i < width - 3; i++)
                robot.MoveTo(Direction.Left);
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveRight(robot, width, height);
            MoveDown(robot, width, height);
            MoveLeft(robot, width, height);

            if (!robot.Finished)
            {
                MoveDown(robot, width, height);
                MoveOut(robot, width, height);
            }
        }
    }
}
