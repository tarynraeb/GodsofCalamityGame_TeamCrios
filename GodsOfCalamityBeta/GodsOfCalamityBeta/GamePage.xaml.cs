using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;
using GodsOfCalamityBeta.ECSComponents;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GodsOfCalamityBeta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
		readonly Game1 _game;
        public Dictionary<int, UserControl> DisasterUCDictionary = new Dictionary<int, UserControl>(); // List of usercontrols in dictionary by their associated entityID
        //public Dictionary<int, Button> DisasterUCDictionary = new Dictionary<int, Button>();
        public TextBlock healthblock = new TextBlock();

        public GamePage()
        {
            this.InitializeComponent();

            // Set up Gaze interaction
            Windows.UI.Core.CoreWindow.GetForCurrentThread().KeyDown += new Windows.Foundation.TypedEventHandler<Windows.UI.Core.CoreWindow, Windows.UI.Core.KeyEventArgs>(delegate (Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args) {
                GazeInput.GetGazePointer(this).Click();
            });

            var sharedSettings = new ValueSet();
            GazeSettingsHelper.RetrieveSharedSettings(sharedSettings).Completed = new AsyncActionCompletedHandler((asyncInfo, asyncStatus) =>
            {
                GazeInput.LoadSettings(sharedSettings);
            });

            Game1.myGamePage = this;

            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
            swapChainPanel.Height = Game1.screenHeight;
            GameSpace.Height = Game1.screenHeight;
            swapChainPanel.Width = Game1.screenWidth;
            GameSpace.Width = Game1.screenWidth;
            swapChainPanel.Margin = new Thickness(0, 0, 0, 0);
            GameSpace.Margin = new Thickness(0, 0, 0, 0);
            Canvas.SetZIndex(GameSpace, 7);

            // Create the pause button
            Button pauseButton = new Button();
            pauseButton.Content = "Pause Game";
            pauseButton.FontFamily = new FontFamily("Old English Text MT");
            pauseButton.FontSize = 48;
            double buttonWidthFactor = 0.2;
            double buttonHeightFactor = 0.2;
            pauseButton.Height = (int)(buttonHeightFactor * (double)Game1.screenHeight);
            pauseButton.Width = (int)(buttonWidthFactor * (double)Game1.screenWidth);
            Thickness buttonMargin = new Thickness();
            buttonMargin.Top = Game1.screenHeight - pauseButton.Height;
            buttonMargin.Left = (Game1.screenWidth * .5) - (pauseButton.Width * .5);
            pauseButton.Margin = buttonMargin;
            pauseButton.Click += PauseButton_Click;

            GameSpace.Children.Add(pauseButton);

            //Define the village button
            Village.Height = 256;
            Village.Width = 256;
            Thickness villageMargin = new Thickness();
            villageMargin.Top = Game1.screenHeight / 2 - Village.Height / 2;
            villageMargin.Left = Game1.screenWidth / 2 - Village.Width / 2;
            Village.Margin = villageMargin;

            //Create the health box
            healthblock.Height = 80;
            healthblock.Width = 142;
            healthblock.FontSize = 72;
            healthblock.FontFamily = new FontFamily("Old English Text MT");
            healthblock.FontWeight = Windows.UI.Text.FontWeights.Bold;
            healthblock.Visibility = Visibility.Collapsed;

            //get the village entity
            var villageEntity = Game1._world.GetEntity(Game1.VillageID);
            var villageHealth = villageEntity.Get<HealthComponent>();
            var villageLocation = villageEntity.Get<PositionComponent>();

            //set the text of healthblock to villageHealth
            healthblock.Text = villageHealth.Health.ToString();
            //set the position of the health block to above the village
            Thickness healthMargin = new Thickness();
            healthMargin.Top = Game1.screenHeight / 2 - Village.Height * .80;
            healthMargin.Left = Game1.screenWidth / 2 - Village.Width * .18;
            healthblock.Margin = healthMargin;
            healthblock.HorizontalAlignment = HorizontalAlignment.Center;
            healthblock.VerticalAlignment = VerticalAlignment.Center;

            GameSpace.Children.Add(healthblock);

            // Calculate the portion of fireGrid occupied by the pause button here
            int buttonXPos = (int)buttonMargin.Left;
            int buttonYPos = (int)buttonMargin.Top;
            int buttonMaxXPos = buttonXPos + (int)pauseButton.Width;
            int buttonMaxYPos = buttonYPos + (int)pauseButton.Height;
            int fireGridX;
            int fireGridY;
            int spriteWidth = Game1.SpriteDict["fire"].texture.Width;
            int spriteHeight = Game1.SpriteDict["fire"].texture.Height;

            while (buttonXPos <= buttonMaxXPos)
            {
                while (buttonYPos <= buttonMaxYPos)
                {
                    fireGridX = (buttonXPos - (buttonXPos % spriteWidth)) / spriteWidth;
                    fireGridY = (buttonYPos - (buttonYPos % spriteHeight)) / spriteHeight;

                    fireGridX -= 1;
                    fireGridY -= 1;

                    Game1.fireGrid[fireGridX, fireGridY] = 1;

                    buttonYPos += Game1.SpriteDict["fire"].texture.Height;
                }
                buttonYPos = (int)buttonMargin.Top;
                buttonXPos += Game1.SpriteDict["fire"].texture.Width;
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)    // PauseButton_Click now needs to invoke the correct system.
        {
            Button pause = (Button)sender;
            sender = (Button)sender;
            if (!Game1.isPaused)
            {
                pause.Content = "Resume Game";
            }
            else
            {
                pause.Content = "Pause Game";
            }

            sender = pause;
            Game1.isPaused = !Game1.isPaused;
        }

        private void Village_Click(object sender, StateChangedEventArgs e)
        {
            if (e.PointerState == PointerState.Enter)
            {
                healthblock.Visibility = Visibility.Visible;
                return;
            }
            else if (e.PointerState == PointerState.Exit)
            {
                healthblock.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void OnInvokeProgress(object sender, DwellProgressEventArgs e)      // This comes from the original sln
        {
            // This is needed for dwelling xaml elements on the front end
            e.Handled = true;
        }

        public void RemoveControlFromGameSpace(UserControl disaster)
        {
            GameSpace.Children.Remove(disaster);
            int entityId = -1;
            
            switch (disaster.Tag.ToString())
            {
                case "lightning":
                    LightningControl lightning = (LightningControl)disaster;
                    entityId = lightning.entityId;
                    break;

                case "meteor":
                    MeteorControl meteor = (MeteorControl)disaster;
                    entityId = meteor.entityId;
                    break;

                case "fire":
                    FireControl fire = (FireControl)disaster;
                    entityId = fire.entityId;
                    break;
            }

            DisasterUCDictionary.Remove(entityId);
        }

        /// <summary>
        /// Creates a new Meteor user control that will coordinate with a Meteor entity
        /// </summary>
        /// <param name="entityId">The ID that will associate the user control with a specific entity.</param>
        public void CreateNewMeteor(int entityId)
        {
            MeteorControl newMeteor = new MeteorControl(entityId);
            DisasterUCDictionary.Add(entityId, newMeteor);
            GameSpace.Children.Add(newMeteor);
        }

        /// <summary>
        /// Creates a new Lightning user control that will coordinate with a Lightning entity
        /// </summary>
        /// <param name="entityId">The ID that will associate the user control with a specific entity.</param>
        public void CreateNewLightning(int entityId)
        {
            LightningControl newLightning = new LightningControl(entityId);
            DisasterUCDictionary.Add(entityId, newLightning);
            GameSpace.Children.Add(newLightning);
        }

        /// <summary>
        /// Creates a new Fire user contorl that will coordinate with a Fire entity
        /// </summary>
        /// <param name="entityId">The ID that will associate the user control with a specific entity.</param>
        public void CreateNewFire(int entityId)
        {
            FireControl newFire = new FireControl(entityId);
            DisasterUCDictionary.Add(entityId, newFire);
            GameSpace.Children.Add(newFire);
        }

        public void DestroyDisaster(int entityId)
        {   
            var entity = Game1._world.GetEntity(entityId);
            if (entity != null && DisasterUCDictionary.ContainsKey(entityId))
            {
                var status = entity.Get<StatusComponent>();
                status.IsDestroyed = true;
                RemoveControlFromGameSpace(DisasterUCDictionary[entityId]);
            }
        }

        public void UpdateDisasterPosition(int entityId)
        {
            var entity = Game1._world.GetEntity(entityId);
            var disaster = DisasterUCDictionary[entityId];
            var position = entity.Get<PositionComponent>();
            float spriteWidth = (float)disaster.Width;
            float spriteHeight = (float)disaster.Height;
            disaster.Margin = new Thickness(position.XCoor - (disaster.Width/2), position.YCoor - (disaster.Height/2), 0, 0);
        }

        public void UpdateFrontEnd(int entityId)
        {
            //update village health
            try
            {
                var villageEntity = Game1._world.GetEntity(Game1.VillageID);
                var villageHealth = villageEntity.Get<HealthComponent>().Health;
                healthblock.Text = villageHealth.ToString();
            }
            catch { };

            if (DisasterUCDictionary.ContainsKey(entityId))
            {
                try
                {
                    // Get the entity
                    var entity = Game1._world.GetEntity(entityId);
                    var disaster = DisasterUCDictionary[entityId];

                    if (entity == null)
                    {
                        //Remove user control - entity no longer exists
                        RemoveControlFromGameSpace(disaster);
                    }
                    else if (disaster == null)
                    {
                        //var status = entity.Get<StatusComponent>();
                        //status.IsDestroyed = true;
                        
                    }
                    else
                    {
                        var position = entity.Get<PositionComponent>();
                        disaster.Margin = new Thickness(position.XCoor - (disaster.Width / 2), position.YCoor - (disaster.Height / 2), 0, 0);
                    }
                }
                catch { };
            }
        }

        public void GameOver()
        {
            this.Frame.Navigate(typeof(GameOverPage));
        }

        private void GazeElement_StateChanged(object sender, StateChangedEventArgs e)
        {

        }
    }
}
