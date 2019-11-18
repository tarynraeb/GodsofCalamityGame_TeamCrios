using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class SpeedComponent : Component
    {
        public int currentSpeed { get; private set; }

        public SpeedComponent() : base(ComponentType.Speed) { }

        public SpeedComponent(int initialSpeed) : base(ComponentType.Speed)
        {
            currentSpeed = initialSpeed;
        }

        public void SetCurrentSpeed(int newSpeed)
        {
            currentSpeed = newSpeed;
        }

        public int GetSpeed()
        {
            return currentSpeed;
        }
    }
}
