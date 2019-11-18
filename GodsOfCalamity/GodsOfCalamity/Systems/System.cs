using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Systems
{
    public interface ISystem { }

    public class InitSystem : ISystem
    {   //Initilizes game
        public InitSystem()
        { }
    }

    public class TerminateSystem : ISystem
    {   //Terminates game
        public TerminateSystem()
        { }
    }

    public class RenderSystem : ISystem
    {   //Renders sprites into game
        public RenderSystem() { }
    }

    public class MovementSystem : ISystem
    {   //Controls entity's movement
        public MovementSystem()
        { }

        public GodsOfCalamity.Entity.Meteor MeteorMovementSystem(GodsOfCalamity.Entity.Meteor targetMeteor)
        {
            int currentX = Convert.ToInt32(targetMeteor.sprite.Margin.Left);
            int currentY = Convert.ToInt32(targetMeteor.sprite.Margin.Top);

            if (currentX >= 832 && currentX <= 1088 && currentY >= 412 && currentY <= 668)
            {
                // call damage method
                // call entity destruct on the meteor
            }

            // need one case for each of the four quadrants of the screen
            if (targetMeteor.sprite.Margin.Left >= 960 && targetMeteor.sprite.Margin.Top >= 540)
            {
                var x = targetMeteor.sprite.Margin.Left - 16;
                var y = targetMeteor.sprite.Margin.Top - 9;

                targetMeteor.sprite.Margin = new Windows.UI.Xaml.Thickness(x, y, 0, 0);
            }
            else if (targetMeteor.sprite.Margin.Left >= 960 && targetMeteor.sprite.Margin.Top <= 540)
            {
                var x = targetMeteor.sprite.Margin.Left - 16;
                var y = targetMeteor.sprite.Margin.Top + 9;

                targetMeteor.sprite.Margin = new Windows.UI.Xaml.Thickness(x, y, 0, 0);
            }
            else if (targetMeteor.sprite.Margin.Left <= 960 && targetMeteor.sprite.Margin.Top <= 540)
            {
                var x = targetMeteor.sprite.Margin.Left + 16;
                var y = targetMeteor.sprite.Margin.Top + 9;

                targetMeteor.sprite.Margin = new Windows.UI.Xaml.Thickness(x, y, 0, 0);
            }
            else if (targetMeteor.sprite.Margin.Left <= 960 && targetMeteor.sprite.Margin.Top >= 540)
            {
                var x = targetMeteor.sprite.Margin.Left + 16;
                var y = targetMeteor.sprite.Margin.Top - 9;

                targetMeteor.sprite.Margin = new Windows.UI.Xaml.Thickness(x, y, 0, 0);
            }

            return targetMeteor;
        }

        public GodsOfCalamity.Entity.Lightning LightningMovementSystem(GodsOfCalamity.Entity.Lightning targetLightning)
        {
            GodsOfCalamity.Entity.Lightning newLightningPosition = targetLightning;
            int currentXPos = Convert.ToInt32(targetLightning.sprite.Margin.Left);
            int currentYPos = Convert.ToInt32(targetLightning.sprite.Margin.Top);

            // Quadrant 1, X: [320,959) Y: [540, 270)   inc x, dec y
            if (currentXPos >= 320 - 64 && currentXPos < 959 - 64 && currentYPos > 270 - 64 && currentYPos <= 540 - 64)
            {
                currentXPos += 7;
                currentYPos -= 3;
                newLightningPosition.sprite.Margin = new Windows.UI.Xaml.Thickness(currentXPos, currentYPos, 0, 0);
                return newLightningPosition;
            }
            // Quadrant 2, X: [959, 1598) Y: [270, 540)  inc x, inc y
            else if (currentXPos >= 959 - 64 && currentXPos < 1598 - 64 && currentYPos >= 270 - 64 && currentYPos < 540 - 64)
            {
                currentXPos += 7;
                currentYPos += 3;
                newLightningPosition.sprite.Margin = new Windows.UI.Xaml.Thickness(currentXPos, currentYPos, 0, 0);
                return newLightningPosition;
            }
            // Quadrant 3, X: [1598, 959) Y: [540, 810)   dec x, inc y
            else if (currentXPos > 959 - 64 && currentXPos <= 1598 - 64 && currentYPos >= 540 - 64 && currentYPos < 810 - 64)
            {
                currentXPos -= 7;
                currentYPos += 3;
                newLightningPosition.sprite.Margin = new Windows.UI.Xaml.Thickness(currentXPos, currentYPos, 0, 0);
                return newLightningPosition;
            }
            // Quadrant 4, X: [959, 320) Y: [810, 540)   dec x, dec y
            else if (currentXPos > 320 - 64 && currentXPos <= 959 - 64 && currentYPos > 540 - 64 && currentYPos <= 810 - 64)
            {
                currentXPos -= 7;
                currentYPos -= 3;
                newLightningPosition.sprite.Margin = new Windows.UI.Xaml.Thickness(currentXPos, currentYPos, 0, 0);
                return newLightningPosition;
            }

            return newLightningPosition;
        }
    }

    public class DeathSystem : ISystem
    {   //Terminate entity (player, hazard, etc)
        public DeathSystem()
        { }
    }

    public class DamageSystem : ISystem
    {   //Manages entity's health
        public DamageSystem()
        { }
    }

    public class FireSpawn : ISystem
    {
        public FireSpawn()
        { }

        public GodsOfCalamity.Entity.Fire SpawnFire(int xPos, int yPos)
        {
            GodsOfCalamity.Entity.Fire newFire = new GodsOfCalamity.Entity.Fire();
            newFire.sprite.Margin = new Windows.UI.Xaml.Thickness(xPos, yPos, 0, 0);
            if (xPos >= 832 && xPos <= 1088 && yPos >= 412 && yPos <= 668)
            {
                // call damage method
                // do not destroy here, need to call damage on some number of ticks
            }
            return newFire;

        }
    }

    public class FirePropagationSystem : ISystem
    {
        // when this system is invoked it is assumed the logic for where to spawn the fire has been executed outside of this class's scope.
        // That is to say that if a new fire entity is going to be spawned north of an existing fire, then the GamePage has determined that there is room to spawn a fire there and that there isn't already a fire there
        // This system will return a new fire entity located exactly 64 pixels north, east, south or west of the existing fire entity

        public FirePropagationSystem()
        { }

        public GodsOfCalamity.Entity.Fire PropagateNorth(GodsOfCalamity.Entity.Fire currentFire)
        {
            FireSpawn newFireSystem = new FireSpawn();
            double currentY = currentFire.sprite.Margin.Top;
            double currentX = currentFire.sprite.Margin.Left;
            int Y = Convert.ToInt32(currentY);
            int X = Convert.ToInt32(currentX);
            Y -= 128;
            GodsOfCalamity.Entity.Fire newFire = newFireSystem.SpawnFire(X, Y);
            return newFire;
        }

        public GodsOfCalamity.Entity.Fire PropagateEast(GodsOfCalamity.Entity.Fire currentFire)
        {
            FireSpawn newFireSystem = new FireSpawn();
            double currentY = currentFire.sprite.Margin.Top;
            double currentX = currentFire.sprite.Margin.Left;
            int Y = Convert.ToInt32(currentY);
            int X = Convert.ToInt32(currentX);
            X += 128;
            GodsOfCalamity.Entity.Fire newFire = newFireSystem.SpawnFire(X, Y);
            return newFire;
        }

        public GodsOfCalamity.Entity.Fire PropagateSouth(GodsOfCalamity.Entity.Fire currentFire)
        {
            FireSpawn newFireSystem = new FireSpawn();
            double currentY = currentFire.sprite.Margin.Top;
            double currentX = currentFire.sprite.Margin.Left;
            int Y = Convert.ToInt32(currentY);
            int X = Convert.ToInt32(currentX);
            Y += 128;
            GodsOfCalamity.Entity.Fire newFire = newFireSystem.SpawnFire(X, Y);
            return newFire;
        }

        public GodsOfCalamity.Entity.Fire PropagateWest(GodsOfCalamity.Entity.Fire currentFire)
        {
            FireSpawn newFireSystem = new FireSpawn();
            double currentY = currentFire.sprite.Margin.Top;
            double currentX = currentFire.sprite.Margin.Left;
            int Y = Convert.ToInt32(currentY);
            int X = Convert.ToInt32(currentX);
            X -= 128;
            GodsOfCalamity.Entity.Fire newFire = newFireSystem.SpawnFire(X, Y);
            return newFire;
        }
    }

    public class visible
    {
        visible()
        { }

        public GodsOfCalamity.Entity.Meteor ShowHealth(GodsOfCalamity.Entity.Meteor healthBar)
        {
            healthBar.sprite.Visibility = Windows.UI.Xaml.Visibility.Visible;
            return healthBar;
        }
    }

    /* CLASS SYSTEMS TO IMPLEMENT
     AvertSystem
     DisplaySystem
     AudioSystem (Optional)
     */
}
