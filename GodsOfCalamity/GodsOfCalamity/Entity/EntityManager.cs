using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Entity
{
    public class EntityManager
    {
        protected List<BaseEntity> Entities;    //Temp

        public EntityManager()
        {

        }
        public BaseEntity CreateDisasterEntity(string type)
        {
            //crates factories for each type of item
            DisasterMaker FireFactory = new FireMaker();
            DisastorClient FireClient = new DisastorClient(FireFactory);

            DisasterMaker LightningFactory = new LightningMaker();
            DisastorClient LightningClient = new DisastorClient(LightningFactory);

            DisasterMaker MeteorFactory = new MeteorMaker();
            DisastorClient MeteorClient = new DisastorClient(MeteorFactory);

            BaseEntity Disastor_Selected = null;

            //if given, will 
            if (type == "Fire")
            {
                Disastor_Selected = FireClient.GetEntity();
            }
            else if (type == "Lightning")
            {
                Disastor_Selected = LightningClient.GetEntity();
            }
            else if (type == "Meteor")
            {
                Disastor_Selected = MeteorClient.GetEntity();
            }

            return Disastor_Selected;
        }
        public BaseEntity GiveVillage()
        {
            Village Gamevillage = new Village();
            return Gamevillage;
        }
        public BaseEntity GiveBackground()
        {
            Background GameBackground = new Background();
            return GameBackground;
        }
        public BaseEntity GiveHealthBar()
        {
            HealthBar ItemHealthBar = new HealthBar();
            return ItemHealthBar;
        }
        public BaseEntity GivePauseButton()
        {
            PauseButton GamePause = new PauseButton();
            return GamePause;
        }
        public BaseEntity GiveMenuResume()
        {
            MenuResume ResumeGame = new MenuResume();
            return ResumeGame;
        }
        public BaseEntity GiveMenuQuit()
        {
            MenuQuit GameQuit = new MenuQuit();
            return GameQuit;
        }
        public BaseEntity GiveCursor()
        {
            PlayerCursor Cursor = new PlayerCursor();
            return Cursor;
        }
    }
    class PlayerCursor : BaseEntity
    {
        public PlayerCursor() : base(EntityType.PlayerCursor)
        {
            id = Guid.NewGuid();
        }
    }
    class Village : BaseEntity
    {
        public Village() : base(EntityType.Village)
        {
            id = Guid.NewGuid();
        }
    }
    class Background : BaseEntity
    {
        public Background() : base(EntityType.Background)
        {
            id = Guid.NewGuid();
        }
    }
    class HealthBar : BaseEntity
    {
        public HealthBar() : base(EntityType.HealthBar)
        {
            id = Guid.NewGuid();
        }
    }
}
