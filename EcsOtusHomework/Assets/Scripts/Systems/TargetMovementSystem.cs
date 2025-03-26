﻿using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public class TargetMovementSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RigidbodyView, Direction, Target>> _filter;
        private readonly EcsPoolInject<DetectedEnemyPosition> _detectedEnemiesPool;
        
        public void Run(IEcsSystems systems)
        {
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc1;
            EcsPool<Direction> moveDirectionPool = _filter.Pools.Inc2;
            EcsPool<Target> targetPool = _filter.Pools.Inc3;
            
            foreach (var i in _filter.Value)
            {
                RigidbodyView rb = rbPool.Get(i);
                Target target = targetPool.Get(i);
                
                ref Direction direction = ref moveDirectionPool.Get(i);
                        
                direction.Value = (target.Value.position - rb.Value.position).normalized;
            }
        }
    }
}