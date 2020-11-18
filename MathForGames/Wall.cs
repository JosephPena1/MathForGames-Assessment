using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Wall : Actor
    {
        private float _speed = 1;
        private Actor _player;
        private Sprite _sprite;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Wall(float x, float y, Actor player, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _player = player;
            _sprite = new Sprite("Images/wall.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Wall(float x, float y, Color rayColor, Actor player, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _player = player;
            _sprite = new Sprite("Images/wall.png");
        }

        public override void Update(float deltaTime)
        {
            //If the player is in range of the goal, end the game
            if (CheckCollision(_collisionTarget))
                Engine.SetGameOver(true);

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Player && _seconds > 1)
                Engine.SetGameOver(true);

            base.OnCollision(actor);
        }

        public override void Draw()
        {
            _sprite.Draw(_globalTransform);

            base.Draw();
        }
    }
}
