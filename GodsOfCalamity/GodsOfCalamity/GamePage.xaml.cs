using GodsOfCalamity.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
public enum ComponentType { Player, Health, Spawn, Render, Position, Damage, Speed, Animation }
public enum EntityType { Fire, Meteor, Lightning, Village, PlayerCursor, Background, PauseButton, HealthBar, MenuResume, MenuQuit }

namespace GodsOfCalamity
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        //static gamepage to switch out to game over. Allows transition of page
        public static GamePage MainGamePage;
        //GameLoop level vars go here.
        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer fireDamageTimer = new DispatcherTimer();
        private DispatcherTimer disasterTimer = new DispatcherTimer();
        public List<BaseEntity> entityManager = new List<BaseEntity>();
        private int maxEntities = 20;
        private Random randomizer = new Random();
        bool active = true;
        GazeElement villageGazeElement = new GazeElement();
        int villageHealth = 100;
        Systems.DamageHealthSystem damageVillage = new Systems.DamageHealthSystem();

		public GamePage()
        {
            this.InitializeComponent();

            Windows.UI.Core.CoreWindow.GetForCurrentThread().KeyDown += new Windows.Foundation.TypedEventHandler<Windows.UI.Core.CoreWindow, Windows.UI.Core.KeyEventArgs>(delegate (Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args) {
                GazeInput.GetGazePointer(this).Click();
            });

            var sharedSettings = new ValueSet();
            GazeSettingsHelper.RetrieveSharedSettings(sharedSettings).Completed = new AsyncActionCompletedHandler((asyncInfo, asyncStatus) =>
            {
                GazeInput.LoadSettings(sharedSettings);
            });
            
            Loaded += (sender, args) =>
            {
                timer.Tick += OnTick;
                fireDamageTimer.Tick += OnFireDamage;
                disasterTimer.Tick += OnDisasterTick;
                timer.Interval = new TimeSpan(600000);
                fireDamageTimer.Interval = new TimeSpan(28800000);
                disasterTimer.Interval = new TimeSpan(14400000);
                timer.Start();
                fireDamageTimer.Start();
                disasterTimer.Start();

            };
            
            /*
            BaseEntity staticFire = new Fire();
            staticFire.sprite.Margin = new Thickness(256, 256, 0, 0);
            Canvas.SetZIndex(staticFire.sprite, 7);
            GameSpace.Children.Add(staticFire.sprite); //ADDING THE ENTITY'S XAML ELEMENT TO THE SCREEN
            // int index = GameSpace.Children.IndexOf(staticFire.sprite);
            entityManager.Add(staticFire);
            */
            

            CompositionTarget.Rendering += GameLoop;
        }

        private void GameLoop(object sender, object e)
        {   

        }

        private void OnDisasterTick(object sender, object o)
        {
            if (entityManager.Count < maxEntities)
            {
                // spawn a disaster
                Random randDis = new Random();
                switch (randDis.Next(1, 3))
                {
                    case 1:
                        BaseEntity enemy = new Meteor();
                        Systems.SpawnSystem newSpawn = new Systems.SpawnSystem();
                        Components.PositionComponent spawnPoint = newSpawn.Spawn(EntityType.Meteor);
                        enemy.sprite.Margin = new Thickness((double)spawnPoint.getXPos(), (double)spawnPoint.getYPos(), 0, 0);
                        Canvas.SetZIndex(enemy.sprite, 7);
                        GameSpace.Children.Add(enemy.sprite); //ADDING THE ENTITY'S XAML ELEMENT TO THE SCREEN
                        int index = GameSpace.Children.IndexOf(enemy.sprite);
                        entityManager.Add(enemy);
                        break;
                    case 2:
                        BaseEntity lightningEnemy = new Lightning();
                        Systems.SpawnSystem newLightningSpawn = new Systems.SpawnSystem();
                        Components.PositionComponent lightningSpawnPoint = newLightningSpawn.Spawn(EntityType.Lightning);
                        lightningEnemy.sprite.Margin = new Thickness((double)lightningSpawnPoint.getXPos(), (double)lightningSpawnPoint.getYPos(), 0, 0);
                        Canvas.SetZIndex(lightningEnemy.sprite, 7);
                        GameSpace.Children.Add(lightningEnemy.sprite); //ADDING THE ENTITY'S XAML ELEMENT TO THE SCREEN
                        int lightningIndex = GameSpace.Children.IndexOf(lightningEnemy.sprite);
                        entityManager.Add(lightningEnemy);
                        break;

                }

                /*
                List<BaseEntity> tempList = entityManager;
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (tempList[i].EntType == EntityType.Lightning)
                    {
                        
                        BaseEntity newFire = new Fire();
                        newFire.sprite.Margin = new Thickness(tempList[i].sprite.Margin.Left, tempList[i].sprite.Margin.Left, 0, 0);
                        Canvas.SetZIndex(newFire.sprite, 7);
                        GameSpace.Children.Add(newFire.sprite); //ADDING THE ENTITY'S XAML ELEMENT TO THE SCREEN
                        int index = GameSpace.Children.IndexOf(newFire.sprite);
                        entityManager.Add(newFire);
                        
                    }
                }*/


                Components.DamageComponent hostileDamage = new Components.DamageComponent();    //Test Component
                hostileDamage.setInflictableDamage(10);
                InflictVillageDamage(hostileDamage);    //Damages Village
            }
        }

        private void OnTick(object sender, object o)
        {

            //TO DO: Add enemies on certain ticks, etc
            for (int i = 0; i < entityManager.Count; i++)
            {
                // call movement system on each entity
                GodsOfCalamity.Systems.MovementSystem moveSys = new GodsOfCalamity.Systems.MovementSystem();

                switch (entityManager[i].EntType)
                {
                    case EntityType.Meteor:
                        entityManager[i] = moveSys.MeteorMovementSystem((GodsOfCalamity.Entity.Meteor)entityManager[i]);
                        MeteorUC meteorSprite = (MeteorUC)entityManager[i].sprite;

                        if (entityManager[i].sprite.Margin.Top > 412 && entityManager[i].sprite.Margin.Left > 832)
                        {
                            RemoveEntityAndSprite(i);
                            DoDamage();
                        }
                        
                        if (meteorSprite.destroyMe)
                        {
                            RemoveEntityAndSprite(i);
                        }
                        break;
                    case EntityType.Lightning:
                        entityManager[i] = moveSys.LightningMovementSystem((GodsOfCalamity.Entity.Lightning)entityManager[i]);

                        LightningUC lightningSprite = (LightningUC)entityManager[i].sprite;
                        if (lightningSprite.destroyMe)
                        {
                            RemoveEntityAndSprite(i);
                        }
                        break;

                    case EntityType.Fire:
                        FireUC fireSprite = (FireUC)entityManager[i].sprite;
                        if (fireSprite.destroyMe)
                        {
                            RemoveEntityAndSprite(i);
                        }
                        break;

                }

                
            }

            
            if (villageHealth <= 0)
            {
                GameOver();
            }
            
        }

        private void RemoveEntityAndSprite(int index)
        {
            GameSpace.Children.Remove(entityManager[index].sprite);
            entityManager.Remove(entityManager[index]);
        }


        public void OnFireDamage(object sender, object o)
        {

            List<BaseEntity> currentEntities = entityManager;
            int currentEntityCount = currentEntities.Count;
            if (currentEntityCount >= maxEntities)
            {
                return;
            }
            Random randNumGen = new Random();

            Systems.FirePropagationSystem firePropagater = new Systems.FirePropagationSystem();
            Fire newFire = new Fire();

            for (int i = 0; i < currentEntityCount; i++)
            {
                if (currentEntities[i].EntType == EntityType.Fire)
                {
                    int rand = randNumGen.Next(0, 5);
                    int currentXPos;
                    int currentYPos;
                    // if current fire is in the village do damage here

                    switch (rand)
                    {                        
                        case 1:
                            newFire = firePropagater.PropagateNorth((Fire)currentEntities[i]);
                            currentXPos = Convert.ToInt32(newFire.sprite.Margin.Top);
                            currentYPos = Convert.ToInt32(newFire.sprite.Margin.Left);
                            if (currentYPos < 1080 && currentYPos > 0 && currentXPos < 1920 && currentXPos > 0)
                            {
                                Canvas.SetZIndex(newFire.sprite, 7);
                                GameSpace.Children.Add(newFire.sprite);
                            }                            
                            break;

                        case 2:
                            newFire = firePropagater.PropagateEast((Fire)currentEntities[i]);
                            currentXPos = Convert.ToInt32(newFire.sprite.Margin.Top);
                            currentYPos = Convert.ToInt32(newFire.sprite.Margin.Left);
                            if (currentYPos < 1080 && currentYPos > 0 && currentXPos < 1920 && currentXPos > 0)
                            {
                                Canvas.SetZIndex(newFire.sprite, 7);
                                GameSpace.Children.Add(newFire.sprite);
                            }
                            break;

                        case 3:
                            newFire = firePropagater.PropagateSouth((Fire)currentEntities[i]);
                            currentXPos = Convert.ToInt32(newFire.sprite.Margin.Top);
                            currentYPos = Convert.ToInt32(newFire.sprite.Margin.Left);
                            if (currentYPos < 1080 && currentYPos > 0 && currentXPos < 1920 && currentXPos > 0)
                            {
                                Canvas.SetZIndex(newFire.sprite, 7);
                                GameSpace.Children.Add(newFire.sprite);
                            }
                            break;

                        case 4:
                            newFire = firePropagater.PropagateWest((Fire)currentEntities[i]);
                            currentXPos = Convert.ToInt32(newFire.sprite.Margin.Top);
                            currentYPos = Convert.ToInt32(newFire.sprite.Margin.Left);
                            if (currentYPos < 1080 && currentYPos > 0 && currentXPos < 1920 && currentXPos > 0)
                            {
                                Canvas.SetZIndex(newFire.sprite, 7);
                                GameSpace.Children.Add(newFire.sprite);
                            }
                            break;
                    }
                }
                entityManager.Add(newFire);
            }

            return;
        }


        public void GameOver()
        {
            this.Frame.Navigate(typeof(GameOverPage));
        }

        //Pause Button
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (active)
            {
                active = false;
                this.timer.Stop();
                this.fireDamageTimer.Stop();
                this.disasterTimer.Stop();
                this.PauseButton.Content = "Resume Game";
            }
            else
            {
                active = true;
                this.timer.Start();
                this.fireDamageTimer.Start();
                this.disasterTimer.Start();
                this.PauseButton.Content = "Pause Game";
            }
        }

        // Showhealth - gaze village
        private void HealthHandler(object sender, StateChangedEventArgs e)
        {
            if (e.PointerState == PointerState.Enter)
            {
                this.HealthBlock.Visibility = Visibility.Visible;
                return;
            }

            else if (e.PointerState == PointerState.Exit)
            {
                this.HealthBlock.Visibility = Visibility.Collapsed;
                return;
            }
        }

        // DamageHealthHandler- Decrements Village Health when Meteor/Fire in contact
        private void DamageHealthHandler(object sender, StateChangedEventArgs e)
        {
            //if disaster touched village
            if (e.PointerState == PointerState.Enter)
            {
                int curHealth = Int32.Parse(this.HealthBlock.Text);
                curHealth -= 20;
                this.HealthBlock.Text = curHealth.ToString();
                return;
            }
        }

        //Modifies Village's health
        private void InflictVillageDamage(Components.DamageComponent hostileDamage)
        {
            string curHealth = this.HealthBlock.Text;
            this.HealthBlock.Text = damageVillage.InflictDamage(curHealth, hostileDamage);
        }

        private void OnInvokeProgress(object sender, DwellProgressEventArgs e)
        {
            e.Handled = true;
        }

        private void DoDamage()
        {
            villageHealth = villageHealth - 10;
            HealthBlock.Text = villageHealth.ToString();
        }

    }
}
