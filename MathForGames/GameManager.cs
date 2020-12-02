using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    delegate void GameEvent();

    static class GameManager
    {
        private static bool _gameover;

        public static bool Gameover { get => _gameover; set => _gameover = value; }

        public static int livesCount = 3;
        public static int goalCount;
        public static GameEvent onWin;

        public static void LivesCounter()
        {
            switch(livesCount)
            {
                case 3:
                    Raylib.DrawText("Lives: 3", 0, 740, 20, Color.GOLD);
                    break;
                case 2:
                    Raylib.DrawText("Lives: 2", 0, 740, 20, Color.GOLD);
                    break;
                case 1:
                    Raylib.DrawText("Lives: 1", 0, 740, 20, Color.GOLD);
                    break;
                case 0:
                    Raylib.DrawText("Lives: 0", 0, 740, 20, Color.GOLD);
                    break;
            }
        }

        public static void Counter()
        {
            switch (goalCount)
            {
                case 0:
                    Raylib.DrawText("0/5 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 1:
                    Raylib.DrawText("1/5 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 2:
                    Raylib.DrawText("2/5 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 3:
                    Raylib.DrawText("3/5 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 4:
                    Raylib.DrawText("4/5 Goals", 0, 0, 20, Color.WHITE);
                    break;
            }
        }

        public static void CheckWin()
        {
            if (goalCount >= 5)
            {
                if (onWin != null)
                    onWin.Invoke();
            }
        }

    }
}
