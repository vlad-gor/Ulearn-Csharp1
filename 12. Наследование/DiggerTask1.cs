// Вставьте сюда финальное содержимое файла DiggerTask.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
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
        //Координатные поля
        public static int X, Y = 0;
        public static int dX, dY = 0;

        public CreatureCommand Act(int x, int y)
        {
            X = x;
            Y = y;
            //Обработка нажатия клавиш присвоение значений дельтам
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
            //Запрет выхода за пределы карты 
            if (!(x + dX >= 0 && x + dX < Game.MapWidth &&
                y + dY >= 0 && y + dY < Game.MapHeight))
                Stay();
            //возвращение следующих координат отрисовки
            return new CreatureCommand() { DeltaX = dX, DeltaY = dY };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {           
            var neighbor = conflictedObject.ToString();
            if (neighbor == "Digger.Gold")
                Game.Scores += 10;
            if (neighbor == "Digger.Sack"||
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
}