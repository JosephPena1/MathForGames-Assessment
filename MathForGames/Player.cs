using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _speed = 1;
        private float rotation;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private Sprite _sprite;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public override void Update(float deltaTime)
        {
            //changes controls based on number. 1 = mouse, 2 = WASD.
            int controls = 2;

            float xDirection = 0;
            float yDirection = 0;

            switch (controls)
            {
                case 1:

                    if (Raylib.GetMousePosition().X >= 512 && Raylib.GetMousePosition().Y >= 380)
                    {
                        xDirection = Raylib.GetMousePosition().X;
                        yDirection = Raylib.GetMousePosition().Y;
                    }
                    else if (Raylib.GetMousePosition().X < 512 && Raylib.GetMousePosition().Y >= 380)
                    {
                        xDirection = -Raylib.GetMousePosition().Y;
                        yDirection = Raylib.GetMousePosition().X;
                    }
                    else if (Raylib.GetMousePosition().X >= 512 && Raylib.GetMousePosition().Y < 380)
                    {
                        xDirection = Raylib.GetMousePosition().X;
                        yDirection = -Raylib.GetMousePosition().Y;
                    }
                    else
                    {
                        xDirection = -Raylib.GetMousePosition().Y;
                        yDirection = -Raylib.GetMousePosition().X;
                    }
                    break;

                case 2:

                    xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                        + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
                    yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                        + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));
                    break;
            }
            
            //SetRotation(rotation += (float)(Math.PI / 2) * deltaTime);
            

            //Set the actors current velocity to be the vector with the direction found scaled by the speed
            Velocity = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;

            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }
    }
}