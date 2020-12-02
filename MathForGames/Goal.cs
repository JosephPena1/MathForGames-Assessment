using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// This is the goal the player must reach to end the game. 
    /// </summary>
    class Goal : Actor
    {
        private float _collisionRadius;
        private Actor _player;
        private Sprite _sprite;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Goal(float x, float y, Actor player)
            : base(x, y)
        {
            _sprite = new Sprite("Images/goal.png");
            _player = player;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Goal(float x, float y, float collisionRadius, Actor player)
            : base(x, y)
        {
            _sprite = new Sprite("Images/goal.png");
            _player = player;
        }

        /// <summary>
        /// Checks to see if the player is in range of the goal.
        /// </summary>
        /// <returns></returns>
        private bool CheckPlayerDistance()
        {
            float distance = (_player.GlobalPosition - GlobalPosition).Magnitude;
            return distance <= 1;
        }

        public override void Update(float deltaTime)
        {
            GameManager.CheckWin();
            //If the player is in range of the goal, end the game
            if (CheckCollision(_collisionTarget))
                GameManager.Gameover = true;

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor[] actor)
        {
            Random randomPos = new Random();
            for (int i = 0; i < actor.Length; i++)
            {
                if (actor[i] is Player && _seconds > 1)
                {
                    SetTranslate(new Vector2(randomPos.Next(5, 30), randomPos.Next(5, 20)));
                    GameManager.Goalcount++;
                }
            }
            
            base.OnCollision(actor);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);

            base.Draw();
        }
    }
}