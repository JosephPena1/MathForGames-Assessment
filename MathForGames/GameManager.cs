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

        public static int Goalcount;
        public static GameEvent onWin;

        public static void Counter()
        {
            switch (Goalcount)
            {
                case 0:
                    Raylib.DrawText("0/3 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 1:
                    Raylib.DrawText("1/3 Goals", 0, 0, 20, Color.WHITE);
                    break;

                case 2:
                    Raylib.DrawText("2/3 Goals", 0, 0, 20, Color.WHITE);
                    break;
            }
        }

        public static void CheckWin()
        {
            if (Goalcount >= 3)
            {
                if (onWin != null)
                    onWin.Invoke();
            }
        }

    }
}
