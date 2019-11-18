using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class PositionComponent : Component
    {
        public int xCoor { get; private set; }
        public int yCoor { get; private set; }

        public PositionComponent() : base(ComponentType.Position) { }

        public PositionComponent(int initialX, int initialY) : base(ComponentType.Position)
        {
            xCoor = initialX;
            yCoor = initialY;
        }

        public void ChangePos(int x, int y)
        {
            xCoor = x;
            yCoor = y;
        }

        public int getXPos()
        {
            return xCoor;
        }

        public int getYPos()
        {
            return yCoor;
        }
    }
}
