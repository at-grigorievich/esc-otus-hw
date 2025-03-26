using Client.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems
{
    public class BulletCollisionSystem: IEcsRunSystem
    {
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld(EcsWorlds.EVENTS).Filter<OnCollisionEnterEvent>().End();
            var pool = systems.GetWorld(EcsWorlds.EVENTS).GetPool<OnCollisionEnterEvent>();
            
            foreach (var entity in filter)
            {
                ref var eventData = ref pool.Get(entity);
                
                if(eventData.senderGameObject == null) continue;
                
                if (eventData.senderGameObject.TryGetComponent(out Entity sourceEntity))
                {
                    if (sourceEntity.HasData<DestroyOnCollision>())
                    {
                        sourceEntity.AddData(new DestroyRequest());
                    }
                }
                
                pool.Del(entity);
                
                //if (eventData.collider.TryGetComponent(out Entity hitEntity) == false) continue;
                
                //if(sourceEntity.TryGetData(out Attack attack) == false) continue;
                //if(sourceEntity.TryGetData(out Team team) == false) continue;
                //if(hitEntity.TryGetData(out Team hittedTeam) == false) continue;
                
                //hitEntity.Dispose();
                
                //if(team.Value == hittedTeam.Value) continue;
                
                //_entityManager.Value.Destroy(sourceEntity.Id);
            }
        }
    }
}