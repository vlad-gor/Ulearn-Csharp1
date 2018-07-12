// Вставьте сюда финальное содержимое файла PyramidMazeTask.cs

namespace Mazes
{
    public static class PyramidMazeTask
    {
        public static int LeftBound = 0;
        public static int RightBound = 0;

        public static void MoveOut(Robot robot, int width, int height)
        {
            LeftBound = 0;
            RightBound = 0;
            while (robot.Y > 2)
            {
                MainCondition(robot, width);
                if (robot.Y == 2)
                    break;
            }
        }

        public static void MainCondition(Robot robot, int width)
        {
            while (robot.X < width - 2 - RightBound)
                robot.MoveTo(Direction.Right);
            LeftBound += 2;

            MoveOnYTwoTimes(robot);
            MoveOnXMinus(robot);
        }

        public static void MoveOnXMinus(Robot robot)
        {
            while (robot.X > 1 + LeftBound)
                robot.MoveTo(Direction.Left);
            if (!robot.Finished)
                MoveOnYTwoTimes(robot);
            RightBound += 2;
        }

        public static void MoveOnYTwoTimes(Robot robot)
        {
            robot.MoveTo(Direction.Up);
            robot.MoveTo(Direction.Up);
        }
    }
}
