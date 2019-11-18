using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using MonoGame.Extended.Entities.Systems;
using GodsOfCalamityBeta.ECSComponents;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace GodsOfCalamityBeta.Systems
{
    class SpawnSystem : UpdateSystem
    {
        private int ElapsedTime { get; set; }
        private int SpawnInterval { get; set; } // Here is where one single spawn interval can be set for all disasters
        private Random randomNumGen;

        SpawnSystem()
        { }

        /// new SpawnSystems can be constructed with variable spawn intervals to increase difficulty later on
        public SpawnSystem(int newSpawnInterval)   
        {
            SpawnInterval = newSpawnInterval;
            randomNumGen = new Random();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime currentTime)     // The function runs on every Update() call of the main game loop
        {
            if (ElapsedTime == SpawnInterval)       // This function checks if the spawn interval has been reached
            {
                // If the spawn time interval has been reached
                // Reset Spawn timer
                ElapsedTime = 0;
                SpawnDisaster();    // Calls the SpawnDisaster helper function
                return;
            }

            else    // If it isn't time to spawn a disiaster
            {
                ElapsedTime++;  // increment elapsed time
                return;
            }
        }

        private void SpawnDisaster()
        {
            /// Spawn a new disaster
            // randomly pick a disaster type to spawn
            int disasterSelectionCase = randomNumGen.Next(1, 3);
            Entity newDisaster = Game1._world.CreateEntity();   // create the entity
            int newID = newDisaster.Id;     // track the new entity's id

            switch (disasterSelectionCase)
            {
                case 1:
                    // using newID make the new entity a lightning entity
                    BuildLightningEntity(newID);
                    break;
                case 2:
                    // using newID make the new entity a meteor entity
                    BuildMeteorEntity(newID);
                    break;
                default:
                    break;
            }

            return;
        }

        private async void BuildLightningEntity(int newLightningID)
        {
            /// using newLightningID we can grab the correct entity in _world and call Attach<T>(T component) to attach our own custom components to the existing entity
            Entity placeHolder = Game1._world.GetEntity(newLightningID);
            /// Attach all the correct components to the entity
            // First give it a starting position - this is randomly selected from 4 pre-defined spawn points
            // Second, give it a delta which should be calculated based off of the initial position
            GodsOfCalamityBeta.ECSComponents.PositionComponent InitialPosition;
  
            int spawnPoint = randomNumGen.Next(1, 5);
            float initX;
            float initY;
            float initAngle;
            switch (spawnPoint)
            {
                case 1: // calculate starting position
                    initX = (float) 0.25 * Game1.screenWidth;
                    initY = (float) 0.5 * Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 2: // calculate starting position
                    initX = (float) 0.5 * Game1.screenWidth;
                    initY = (float) 0.25 * Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 3: // calculate starting position
                    initX = (float) 0.75 * Game1.screenWidth;
                    initY = (float) 0.5 * Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 4: // calculate starting position
                    initX = (float) 0.5 * Game1.screenWidth;
                    initY = (float) 0.75 * Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                default:
                    break;
            }

            /// Initializes Components
            SpriteComponent new_sprite = new SpriteComponent(Game1.SpriteDict["lightning"], 1);
            DeltaComponent new_delta = new DeltaComponent(0, 0, 0);
            StatusComponent new_status = new StatusComponent(false, Game1.EntityType.Lightning);
            CollisionBoxComponet new_collision = new CollisionBoxComponet(new_sprite.Sprite, placeHolder.Get<PositionComponent>());

            /// Attaches Components
            placeHolder.Attach(new_sprite);
            placeHolder.Attach(new_delta);
            placeHolder.Attach(new_status);
            placeHolder.Attach(new_collision);

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            Game1.myGamePage.CreateNewLightning(newLightningID);
                        }
                        );
            
            return;
        }

        private async void BuildMeteorEntity(int newMeteorID)
        {
            // using newMeteorID we can grab the correct entity in _world and call Attach<T>(T component) to attach our own custom components to the existing entity
            Entity placeHolder = Game1._world.GetEntity(newMeteorID);
            // Attach all the correct components to the entity
            // First give it a starting position - this is randomly selected from 4 pre-defined spawn points
            // Second, give it a delta which should be calculated based off of the initial position
            GodsOfCalamityBeta.ECSComponents.PositionComponent InitialPosition;
            

            int spawnPoint = randomNumGen.Next(1, 5);
            float initX;
            float initY;
            float initAngle;
            switch (spawnPoint)
            {
                case 1:
                    // calculate starting position
                    initX = 0;
                    initY = 0;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 2:
                    // calculate starting position
                    initX = Game1.screenWidth;
                    initY = 0;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 3:
                    // calculate starting position
                    initX = Game1.screenWidth;
                    initY = Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                case 4:
                    // calculate starting position
                    initX = 0;
                    initY = Game1.screenHeight;
                    initAngle = 0;
                    InitialPosition = new PositionComponent(initX, initY, initAngle);
                    placeHolder.Attach(InitialPosition);

                    break;
                default:
                    break;
            }

            /// Initializes Components
            SpriteComponent new_sprite = new SpriteComponent(Game1.SpriteDict["meteor"], 1);
            DeltaComponent new_delta = new DeltaComponent(0, 0, 0);
            StatusComponent new_status = new StatusComponent(false, Game1.EntityType.Meteor);
            CollisionBoxComponet new_collision = new CollisionBoxComponet(new_sprite.Sprite, placeHolder.Get<PositionComponent>());

            /// Attaches Components
            placeHolder.Attach(new_sprite);
            placeHolder.Attach(new_delta);
            placeHolder.Attach(new_status);
            placeHolder.Attach(new_collision);
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            Game1.myGamePage.CreateNewMeteor(newMeteorID);
                        }
                        ); 
        }
    }
}
