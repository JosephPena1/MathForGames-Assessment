using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Engine
    {
        private Camera3D _camera = new Camera3D();
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        public static int CurrentSceneIndex { get => _currentSceneIndex; set => _currentSceneIndex = value; }

        public Engine()
        {
            _scenes = new Scene[0];
        }

        /// <summary>
        /// Adds the given scene to the array of scenes.
        /// </summary>
        /// <param name="scene">The scene that will be added to the array</param>
        /// <returns>The index the scene was placed at. Returns -1 if
        /// the scene is null</returns>
        public static int AddScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return -1;

            //Create a new temporary array that one size larger than the original
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy values from old array into new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Store the current index
            int index = _scenes.Length;

            //Sets the scene at the new index to be the scene passed in
            tempArray[index] = scene;

            //Set the old array to the tmeporary array
            _scenes = tempArray;

            return index;
        }

        /// <summary>
        /// Finds the instance of the scene given that inside of the array
        /// and removes it
        /// </summary>
        /// <param name="scene">The scene that will be removed</param>
        /// <returns>If the scene was successfully removed</returns>
        public static bool RemoveScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            //Create a new temporary array that is one less than our original array
            Scene[] tempArray = new Scene[_scenes.Length - 1];

            //Copy all scenes except the scene we don't want into the new array
            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            //If the scene was successfully removed set the old array to be the new array
            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        /// <summary>
        /// Sets the current scene in the game to be the scene at the given index
        /// </summary>
        /// <param name="index">The index of the scene to switch to</param>
        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[CurrentSceneIndex].Started)
                _scenes[CurrentSceneIndex].End();

            //Update the current scene index
            CurrentSceneIndex = index;
        }

        public static Scene GetScenes(int index)
        {
            return _scenes[index];
        }

        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        /// <summary>
        /// Returns true while if key was pressed once
        /// </summary>
        /// <param name="key">The ascii value of the key to check</param>
        /// <returns></returns>
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        private void Start()
        {
            Raylib.InitWindow(1024, 760, "Math For Games");
            Raylib.SetTargetFPS(60);
            _camera.position = new System.Numerics.Vector3(0.0f, 40.0f, 30.0f);  // Camera position, y20 & z20 default
            _camera.target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f);      // Camera looking at point
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
            _camera.fovy = 45.0f;                                                // Camera field-of-view Y
            _camera.type = CameraType.CAMERA_PERSPECTIVE;                        // Camera mode type
            Raylib.GetMouseRay(Raylib.GetMousePosition(), _camera);

            Player player = new Player(0, 0, 0, Color.BLUE, Shape.SPHERE, 2);
            Partner partner = new Partner(5, 0, 0, Color.GREEN, Shape.CUBE, 2);
            Goal goal = new Goal(0, 0, -15, Color.GRAY, Shape.CUBE, 2);

            //make it so enemies follow the goal, the player will prevent them from reaching it

            player.Speed = 5;
            player.AddChild(partner);

            Scene scene = new Scene();
            scene.AddActor(player);
            scene.AddActor(partner);
            scene.AddActor(goal);

            Enemy.AddEnemy(10, partner, goal, scene);

            SetCurrentScene(AddScene(scene));
        }

        private void Update(float deltaTime)
        {
            if (!_scenes[CurrentSceneIndex].Started)
                _scenes[CurrentSceneIndex].Start();

            _scenes[CurrentSceneIndex].Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.BLACK);
            //Raylib.DrawText("Hello", 0, 0, 5, Color.GREEN);
            _scenes[CurrentSceneIndex].Draw();

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        private void End()
        {
            _scenes[CurrentSceneIndex].End();
        }

        public void Run()
        {
            Start();

            while (!GameManager.Gameover && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }

            End();
        }

    }
}