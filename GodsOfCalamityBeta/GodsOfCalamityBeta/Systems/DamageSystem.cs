using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace GodsOfCalamityBeta.Systems
{
    class DamageSystem : MonoGame.Extended.Entities.Systems.EntityUpdateSystem
    {
        private int damagevalue = 10;
        private int fireDamageValue = 1;
        private int ElapsedTime = 0;
        private int firedamagetick { get; set; }
        Entity currentEntity;
        private Entity VillageEntity;
        public DamageSystem(int newdamagetick) : base(Aspect.All(typeof(ECSComponents.CollisionBoxComponet)))
        {
            firedamagetick = newdamagetick;
        }

        public override async void Update(GameTime gameTime)
        {
            VillageEntity = GetEntity(Game1.VillageID);
            if (VillageEntity != null)
            {
                var Villagebox = VillageEntity.Get<ECSComponents.CollisionBoxComponet>();
                foreach (var entity in ActiveEntities)
                {
                    if (Villagebox != null)
                    {
                        currentEntity = GetEntity(entity);
                        if (currentEntity.Id == VillageEntity.Id)
                        {
                            continue;
                        }
                        var entitybox = currentEntity.Get<ECSComponents.CollisionBoxComponet>();
                        if (entitybox.HitBox.Intersects(Villagebox.HitBox) && currentEntity.Get<ECSComponents.StatusComponent>().IsDestroyed == false)
                        {
                            if (currentEntity.Get<ECSComponents.StatusComponent>().Type == Game1.EntityType.Fire)
                            {
                                if (ElapsedTime == firedamagetick)
                                {
                                    ElapsedTime = 0;
                                    VillageEntity.Get<ECSComponents.HealthComponent>().Health -= fireDamageValue;
                                    if (VillageEntity.Get<ECSComponents.HealthComponent>().Health <= 0)
                                    {
                                        VillageEntity.Get<ECSComponents.StatusComponent>().IsDestroyed = true;
                                    }
                                }
                                else
                                {
                                    ElapsedTime++;
                                }
                            }
                            else
                            {
                                currentEntity.Get<ECSComponents.StatusComponent>().IsDestroyed = true;
                                VillageEntity.Get<ECSComponents.HealthComponent>().Health -= damagevalue;
                                if (VillageEntity.Get<ECSComponents.HealthComponent>().Health <= 0)
                                {
                                    VillageEntity.Get<ECSComponents.StatusComponent>().IsDestroyed = true;
                                    
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                                () =>
                                                {
                                                    Game1.myGamePage.GameOver();
                                                }
                                                );
                                    Game1.isGameOver = true;
                                    
                                }
                            }

                        }
                    }
                }
            }
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            
        }
    }
}
