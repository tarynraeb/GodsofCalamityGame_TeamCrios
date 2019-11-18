using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

/************************* NOTE:
 App.xaml is the entry point of every windows store application
 Game1.cs contains the logic of the game (updating data, refreshing display)
 GamePage.xaml is the default page initialized with the Game1 class

    Lifecycle of a game application:
    Initialize() ----> LoadContent() ----> Update() ----> UnloadContent()
                                             | ^
                                          Game Loop
                                             V |
                                            Draw()

    Data Binding will be the primary way that xaml elements interact with the underlying entities.
    More info at: docs.microsoft.com/en-us/dotnet/framework/wpf/data/data-binding-overview

    Need to bind the xaml elements to the appropriate entities
    EX:
        Binding Target                                     Binding Source
    xaml Fire UC current Position        ------------>   Position component
 **************************/

namespace GodsOfCalamityBeta
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static float screenWidth;    // must be accessible by systems when creating new entities
        public static float screenHeight;
        public static int VillageID;
        public enum EntityType { Village, Meteor, Lightning, Fire };
        Texture2D village;
        Texture2D map;
        Texture2D lightning;
        Texture2D meteor;
        Texture2D fire;
        public static World _world;         // Must be accessible by systems to create new entities
        public static Dictionary<string, SpriteClass> SpriteDict = new Dictionary<string, SpriteClass>();   // will be passed to spawn system to attach sprite components
        public static bool isPaused;
        public static bool isGameOver = false;
        public static int[,] fireGrid; // 2d array represents the grid fire can be spawned on. The array will store entityIDs
        public static int fireGridMaxX;
        public static int fireGridMaxY;

        public static GamePage myGamePage;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Game does not start in paused state
            isPaused = false;

            /// Launch game in fullscreen
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            
            /// Get screen Width and Height
            screenHeight = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Height);
            var screenHeight2 = GraphicsDevice.Viewport.Bounds.Height;
            screenWidth = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Width);
            var screenWidth2 = GraphicsDevice.Viewport.Bounds.Width;

            base.Initialize();
        }

        /// LoadContent called once and is the place to load all of your content.
        protected override void LoadContent()
        {
            /// Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Texture2D>("map");
            village = Content.Load<Texture2D>("Village");
            lightning = Content.Load<Texture2D>("lightning");
            meteor = Content.Load<Texture2D>("meteor");
            fire = Content.Load<Texture2D>("fire");
            

            /// Creates the SpriteTextureComponents so they can be attached to entities
            SpriteClass lightningSprite = new SpriteClass(GraphicsDevice, "Assets/Lightning.png", 1); // scale of sprites may be adjusted here
            SpriteClass meteorSprite = new SpriteClass(GraphicsDevice, "Assets/Meteor.png", 1); // scale of sprites may be adjusted here
            SpriteClass fireSprite = new SpriteClass(GraphicsDevice, "Assets/Fire.png", 1); // scale of sprites may be adjusted here
            SpriteClass villageSprite = new SpriteClass(GraphicsDevice, "Assets/Village.png", 1); // scale of sprites may be adjusted here

            SpriteDict.Add("lightning", lightningSprite);
            SpriteDict.Add("meteor", meteorSprite);
            SpriteDict.Add("fire", fireSprite);
            SpriteDict.Add("village", villageSprite);


            // ***************Default Aspect Builder --Subject to change!--***************
            AspectBuilder defaultAspectBuilder = new AspectBuilder();
            fireGridMaxX = (int)(screenWidth / fire.Width);
            fireGridMaxY = (int)(screenHeight / fire.Height);
            fireGrid = new int[fireGridMaxX, fireGridMaxY]; // initialize fireGrid to fit the users screen dimensions
            fireGridMaxX -= 1;
            fireGridMaxY -= 1;

            // Load world ***********************************************************************************
            _world = new WorldBuilder()
                .AddSystem(new Systems.MovementSystem(screenWidth, screenHeight))
                .AddSystem(new Systems.RenderSystem(defaultAspectBuilder))
                .AddSystem(new Systems.SpawnSystem(90))
                .AddSystem(new Systems.EntityDestructSystem())
                .AddSystem(new Systems.FireSpawnSystem(defaultAspectBuilder))
                .AddSystem(new Systems.UpdateFrontEndSystem(defaultAspectBuilder))
                .AddSystem(new Systems.DamageSystem(5))
                /// Add systems here via ".AddSystem(new [SystemName](Parameters))"
                
                .Build();

            //load and build village
            Entity VillageEntity = _world.CreateEntity();
            ECSComponents.PositionComponent villageposition = new ECSComponents.PositionComponent((float) 1/2 * screenWidth, (float) 1/2 * screenHeight, 0);
            ECSComponents.StatusComponent villagestatus = new ECSComponents.StatusComponent(false, EntityType.Village);
            ECSComponents.SpriteComponent villagesprite = new ECSComponents.SpriteComponent(SpriteDict["village"], 1);
            ECSComponents.CollisionBoxComponet villageHitBox = new ECSComponents.CollisionBoxComponet(villagesprite.Sprite, villageposition);
            ECSComponents.HealthComponent villageHealth = new ECSComponents.HealthComponent(100);

            VillageEntity.Attach(villageposition);
            VillageEntity.Attach(villagestatus);
            VillageEntity.Attach(villagesprite);
            VillageEntity.Attach(villageHitBox);
            VillageEntity.Attach(villageHealth);

            VillageID = VillageEntity.Id;
            // ***********************************************************************************************
        }

        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            if (isGameOver)
            {
                Exit();
            }
            if (!isPaused)
            {
                _world.Update(gameTime);
                base.Update(gameTime);
            }
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(map, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);   // draws map to the screen

            spriteBatch.End();
            _world.Draw(gameTime);
            base.Draw(gameTime);
        }

        /// Used to scale to high DPI displays
        public static float ScaleToHighDPI(float f)
        {
            Windows.Graphics.Display.DisplayInformation d = Windows.Graphics.Display.DisplayInformation.GetForCurrentView();
            f *= (float)d.RawPixelsPerViewPixel;
            return f;
        }
    }
}
