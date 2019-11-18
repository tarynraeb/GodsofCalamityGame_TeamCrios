using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodsOfCalamity;
using GodsOfCalamity.Entity;
using GodsOfCalamity.Components;

namespace GodsOfCalamity.Systems
{
    //should be something in game engine for this next item or maybe not
    //public delegate bool VillageDestroyed();
    class GameOverSystem
    {
        public EventHandler<VillageIsDestroyedEvent> OnHealthReachesZero;
        public GameOverSystem()
        {
          
        }

        public void Checkhealth(BaseEntity VillageEntity)
        {
            int healthlocation = VillageEntity.componentparts.FindIndex(componet => componet.GetComponentType() == ComponentType.Health);
            HealthComponent CurrentHealth = (HealthComponent)VillageEntity.componentparts[healthlocation];
            if (CurrentHealth.getHealth() <= 0)
            {
                OnHealthReachesZero(this, new VillageIsDestroyedEvent());
            }
            
        }

        static void VillageHealthIsZero(object sender, VillageIsDestroyedEvent e)
        {
            Console.WriteLine("Village health has reached game over state");
        }
    }
    public class VillageIsDestroyedEvent : EventArgs
    {
        public VillageIsDestroyedEvent()
        {
            //simple command for gamepage to simply move to a different page
            GamePage.MainGamePage.GameOver();
        }
    }
}
