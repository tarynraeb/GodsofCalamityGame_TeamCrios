/* Author:  Cong Trinh (Team Crios)
 * Class:   Cpts421 Senior Design
 * Project: Microsoft Eye-Tracking Game; "Gods of Calamity"
 *
 * Desc:    DamageHealthSystem will calculate and return the target's new health
 *              after recieving damage from a hostile.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Systems
{
    public class DamageHealthSystem : ISystem
    {   /*Reads entity's current/max health and (curHealth - damageDealt = newHealth)*/

        public DamageHealthSystem() { }

        //public string InflictDamage(string targetHealth, int hostileDamage)
        public string InflictDamage(string targetHealth, Components.DamageComponent hostileDamage)
        {
            int curHealth = Int32.Parse(targetHealth);
            Components.HealthComponent villageHealth = new Components.HealthComponent(curHealth);   //Creates health component

            int curDamage = hostileDamage.getInflictableDamage();
            //int curHealth = targetHP.getHealth();
            //int curDamage = hostileDamage;

            int newHP = curHealth - curDamage;  //Calculates new health of entity

            /*  //Edit: Prototype won't use HealthComponent (for now)
            if (newHP > 0)
                targetHP.setHealth(newHP);  //Sets new health
            else
                targetHP.setHealth(-1);     // (!!!) TEMPORARY: Replace w/ Destroy Entity Function
            return targetHP;
            */

            if (newHP <= 0)
                newHP = 0;  //Resets Health to 0, avoid negative value

            return newHP.ToString();
        }

    }
}
