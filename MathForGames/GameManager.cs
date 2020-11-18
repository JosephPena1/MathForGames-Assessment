using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    delegate void GameEvent();

    static class GameManager
    {
        private static bool _gameover;

        public static bool Gameover { get => _gameover; set => _gameover = value; }

        public static int enemyCount;
        public static GameEvent onWin;

        public static void CheckWin()
        {
            if (enemyCount <= 0)
            {
                if (onWin != null)
                    onWin.Invoke();
            }
        }

    }
}
