using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Entity
{
    interface DisasterMaker
    {
        IsDisaster getDisastorType();
    }
    class FireMaker : DisasterMaker
    {
        public IsDisaster getDisastorType()
        {
            return new FireCreator();
        }
    }
    class LightningMaker : DisasterMaker
    {
        public IsDisaster getDisastorType()
        {
            return new LightningCreator();
        }
    }
    class MeteorMaker : DisasterMaker
    {
        public IsDisaster getDisastorType()
        {
            return new MeteorCreator();
        }
    }
    interface IsDisaster
    {
        BaseEntity DisastorCreation();
    }
    class FireCreator : IsDisaster
    {
        public BaseEntity DisastorCreation()
        {
            return new Fire();
        }
    }
    class LightningCreator : IsDisaster
    {
        public BaseEntity DisastorCreation()
        {
            return new Lightning();
        }
    }
    class MeteorCreator : IsDisaster
    {
        public BaseEntity DisastorCreation()
        {
            return new Meteor();
        }
    }
    class DisastorClient
    {
        IsDisaster Disaster;

        public DisastorClient(DisasterMaker factory)
        {
            Disaster = factory.getDisastorType();
        }
        public BaseEntity GetEntity()
        {
            return Disaster.DisastorCreation();
        }

    }
}
