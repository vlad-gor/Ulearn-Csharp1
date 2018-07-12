namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            if (height > width)
            {
                MoveOnY(robot, width, height);
            }
            else
                MoveOnX(robot, width, height);
        }

        public static void MoveOnY(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                for (int i = 0; i < (height - 3) / (width - 3 + 1); i++)
                {
                    robot.MoveTo(Direction.Down);
                }
                if (!robot.Finished)
                    robot.MoveTo(Direction.Right);
            }
        }

        public static void MoveOnX(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                for (int i = 0; i < (width - 3) / (height - 3 + 1); i++)
                {
                    robot.MoveTo(Direction.Right);
                }
                if (!robot.Finished)
                    robot.MoveTo(Direction.Down);
            }
        }
    }
}
