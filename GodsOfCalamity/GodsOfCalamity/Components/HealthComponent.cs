using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class HealthComponent : Component
    {
        public int currentHealth { get; private set; }

        public int maxHealth { get; }

        public HealthComponent() : base(ComponentType.Health) { }

        public HealthComponent(int initialHealth) : base(ComponentType.Health)
        {
            currentHealth = initialHealth;
        }

        public int getHealth()
        {
            return currentHealth;
        }

        public void setHealth(int newHealth)
        {
            currentHealth = newHealth;
        }

    }
}
