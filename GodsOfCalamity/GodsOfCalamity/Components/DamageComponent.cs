using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class DamageComponent : Component
    {
        public int inflictableDamage { get; private set; }

        public DamageComponent() : base(ComponentType.Damage) { }

        public DamageComponent(int initialInflictableDamage) : base(ComponentType.Damage)
        {
            inflictableDamage = initialInflictableDamage;
        }

        public void setInflictableDamage(int dam)
        {
            inflictableDamage = dam;
        }

        public int getInflictableDamage()
        {
            return inflictableDamage;
        }

    }
}
