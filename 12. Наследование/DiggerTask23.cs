using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public static int X, Y = 0;
        public static int dX, dY = 0;
        
        public CreatureCommand Act(int x, int y)
        {
            X = x;
            Y = y;
           
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                    dY = 0;
                    dX = -1;
                    break;

                case System.Windows.Forms.Keys.Up:
                    dY = -1;
                    dX = 0;
                    break;

                case System.Windows.Forms.Keys.Right:
                    dY = 0;
                    dX = 1;
                    break;

                case System.Windows.Forms.Keys.Down:
                    dY = 1;
                    dX = 0;
                    break;

                default:
                    Stay();
                    break;
            }

            if (!(x + dX >= 0 && x + dX < Game.MapWidth &&
                y + dY >= 0 && y + dY < Game.MapHeight))
                Stay();

            if (Game.Map[x + dX, y + dY] != null)
            {
                if (Game.Map[x + dX, y + dY].ToString() == "Digger.Sack")
                    Stay();
            }

            return new CreatureCommand() { DeltaX = dX, DeltaY = dY };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {          
            var neighbor = conflictedObject.ToString();
            if (neighbor == "Digger.Gold")
                Game.Scores += 10;
            if (neighbor == "Digger.Sack" ||
                neighbor == "Digger.Monster")
            {
                
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }

        private static void Stay()
        {
            dX = 0;
            dY = 0;
        }
    }


    public class Sack : ICreature
    {
        private int counter = 0;
        public static bool deadlyForPlayer = false;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null ||
                    (counter > 0 && (map.ToString() == "Digger.Player" ||
                    map.ToString() == "Digger.Monster")))
                {
                    counter++;
                    return Fall();
                }
            }

            if (counter > 1)
            {
                counter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            counter = 0;
            return DoNothing();
        }


        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        private CreatureCommand Fall()
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
        }

        private CreatureCommand DoNothing()
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return (neighbor == "Digger.Player" ||
               neighbor == "Digger.Monster");
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }


    public class Monster : ICreature
    {

        public CreatureCommand Act(int x, int y)
        {
            int dx = 0;
            int dy = 0;

            if (IsPlayerAlive())
            {
                if (Player.X == x)
                {
                    if (Player.Y < y) dy = -1;
                    else if (Player.Y > y) dy = 1;
                }

                else if (Player.Y == y)
                {
                    if (Player.X < x) dx = -1;
                    else if (Player.X > x) dx = 1;
                }
                else
                {
                    if (Player.X < x) dx = -1;
                    else if (Player.X > x) dx = 1;
                }
            }
            else return Stay();

            if (!(x + dx >= 0 && x + dx < Game.MapWidth &&
                y + dy >= 0 && y + dy < Game.MapHeight))
                return Stay();

            var map = Game.Map[x + dx, y + dy];
            if (map != null)
                if (map.ToString() == "Digger.Terrain" ||
                    map.ToString() == "Digger.Sack" ||
                    map.ToString() == "Digger.Monster")
                    return Stay();
            return new CreatureCommand() { DeltaX = dx, DeltaY = dy };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return (neighbor == "Digger.Sack" ||
			neighbor == "Digger.Monster");
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        static private CreatureCommand Stay()
        {

            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }


        static private bool IsPlayerAlive()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    var map = Game.Map[i, j];
                    if (map != null)
                    {
                        if (map.ToString() == "Digger.Player")
                        {
                            Player.X = i;
                            Player.Y = j;
                            return true;
                        }
                    }
                }
            return false;
        }
    }
}
