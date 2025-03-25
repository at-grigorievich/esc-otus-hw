using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class TargetMovementSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView, MoveDirection, Target>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc1;
            EcsPool<MoveDirection> moveDirectionPool = _filter.Pools.Inc2;
            EcsPool<Target> targetPool = _filter.Pools.Inc3;
            
            foreach (var i in _filter.Value)
            {
                RigidbodyView rb = rbPool.Get(i);
                Target target = targetPool.Get(i);
                
                ref MoveDirection moveDirection = ref moveDirectionPool.Get(i);
                
                moveDirection.Value = (target.Value.position - rb.Value.position).normalized;
            }
        }
    }
}