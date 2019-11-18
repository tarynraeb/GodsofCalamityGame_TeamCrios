using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class SpawnComponent : Component
    {
        public bool isSpawned;

        public SpawnComponent() : base(ComponentType.Spawn)
        { 
            isSpawned = true;
        }

        public bool GetSpawnStatus()
        {
            return isSpawned;
        }

        public void SetSpawnStatus(bool status)
        {
            isSpawned = status;
        }
    }
}
