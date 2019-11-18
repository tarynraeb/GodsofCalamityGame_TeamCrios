using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamityBeta.ECSComponents
{
    class StatusComponent
    {
        public Game1.EntityType Type { get; set; }
        public bool IsDestroyed { get; set; }

        /// Add any game-statuses you want for entities in here

        StatusComponent()
        {
            IsDestroyed = false;
        }

        public StatusComponent(bool isDestroyed, Game1.EntityType Type )
        {
            this.IsDestroyed = isDestroyed;
            this.Type = Type;
        }
    }
}
