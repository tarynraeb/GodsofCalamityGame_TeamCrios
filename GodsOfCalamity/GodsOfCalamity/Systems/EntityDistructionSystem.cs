using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodsOfCalamity.Entity;
using GodsOfCalamity.Components;

namespace GodsOfCalamity.Systems
{
    class EntityDistructionSystem
    {
        public EntityDistructionSystem()
        {

        }
        public void RemoveEntity(List<BaseEntity> EntityList, BaseEntity GivenEntity)
        {
            //should simply remove the given entity from the list and should no longer exist in the list, therefore should be removed.
            Guid ID = GivenEntity.getID();
            int indextoremove = EntityList.FindIndex(item => item.getID() == ID);
            EntityList.RemoveAt(indextoremove);
        }
    }
}
