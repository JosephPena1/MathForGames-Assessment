using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames3D
{
    delegate void GameEvent();

    class GameManager
    {
        //Make a game where enemies chase you and you use a box to defeat them. P.S. play game if you forgot what im talking about me.

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
