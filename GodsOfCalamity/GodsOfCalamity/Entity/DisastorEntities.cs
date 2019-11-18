using GodsOfCalamity.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Entity
{
    public class Fire : BaseEntity
    {
        public Fire() : base(EntityType.Fire)
        {
            id = Guid.NewGuid();
            sprite = new FireUC();
            AddComponent(new PositionComponent(0, 0)); 
        }
    }
    public class Meteor : BaseEntity
    {
        public Meteor() : base(EntityType.Meteor)
        {
            id = Guid.NewGuid();
            sprite = new MeteorUC();
            AddComponent(new PositionComponent(0, 0));
        }
    }
    public class Lightning : BaseEntity
    {
        public Lightning() : base(EntityType.Lightning)
        {
            id = Guid.NewGuid();
            sprite = new LightningUC();
            AddComponent(new PositionComponent(0, 0));
        }
    }

}
