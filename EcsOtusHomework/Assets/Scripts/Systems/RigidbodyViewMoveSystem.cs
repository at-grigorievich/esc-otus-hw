using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class RigidbodyViewMoveSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView, MoveDirection, MoveSpeed>> _filter;

        public void Run(IEcsSystems systems)
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc1;
            EcsPool<MoveDirection> moveDirectionPool = _filter.Pools.Inc2;
            EcsPool<MoveSpeed> moveSpeedPool = _filter.Pools.Inc3;

            foreach (var i in _filter.Value)
            {
                float speed = moveSpeedPool.Get(i).Value;
                Vector3 direction = moveDirectionPool.Get(i).Value;
                Quaternion rotation = Quaternion.LookRotation(direction);
                
                
                ref RigidbodyView rb = ref rbPool.Get(i);
                
                rb.Value.MovePosition(rb.Value.position + direction * speed * fixedDeltaTime);
                rb.Value.MoveRotation(Quaternion.Slerp(rb.Value.rotation, rotation, speed * fixedDeltaTime));
            }
        }
    }
}