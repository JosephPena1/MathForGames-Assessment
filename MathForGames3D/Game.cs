using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Game
    {
        private static bool _gameOver;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        private Camera3D _camera = new Camera3D();

        public static bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        public static int CurrentSceneIndex
        {
            get { return _currentSceneIndex; }
        }

        public static Scene GetScene(int index)
        {
            if (index < 0 || index > _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        /// <summary>
        /// Returns the scene that is at the index of the 
        /// current scene index
        /// </summary>
        /// <returns></returns>
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
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
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        }

        private void Start()
        {
            Raylib.InitWindow(1024, 760, "Math For Game 3D");
            Raylib.SetTargetFPS(60);

            _camera.position = new System.Numerics.Vector3(0.0f, 10.0f, 10.0f); // Camera position
            _camera.target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f);     // Camera looking at point
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);         // Camera up vector (rotation towards target)
            _camera.fovy = 45.0f;                                               // Camera Field-of-View Y
            _camera.type = CameraType.CAMERA_PERSPECTIVE;                       // Camera mode type

            Scene scene1 = new Scene();

            Actor testActor = new Actor(5, 5, 0, Color.BLUE, 'O');

            testActor.SetScale(5, 5, 5);

            scene1.AddActor(testActor);

            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);
            SetCurrentScene(startingSceneIndex);
        }

        private void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.RAYWHITE);
            _scenes[_currentSceneIndex].Draw();
            Raylib.DrawGrid(10, 1.0f);
            Raylib.DrawSphere(new System.Numerics.Vector3(0, 0, 0), 0.5f, Color.BLUE);

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        private void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }

        public void Run()
        {
            Start();

            while (!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }

            End();
        }

    }
}