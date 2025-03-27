using Client.Components;
using Client.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems
{
    public class DetectEnemySystem: IEcsRunSystem
    {
        private readonly int _layerMask = ~LayerMask.GetMask("bullet");
        private readonly EcsFilterInject<Inc<DetectedEnemyPosition, RigidbodyView, Team, AttackRange>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            RaycastHit hit;

            EcsPool<DetectedEnemyPosition> detectEnemyPool = _filter.Pools.Inc1;
            EcsPool<RigidbodyView> rbPool = _filter.Pools.Inc2;
            EcsPool<Team> teamPool = _filter.Pools.Inc3;
            EcsPool<AttackRange> attackRangePool = _filter.Pools.Inc4;

            foreach (var i in _filter.Value)
            {
                Rigidbody rb = rbPool.Get(i).Value;

                Vector3 pos = rb.position;
                Vector3 forward = rb.transform.forward;
                
                TeamType team = teamPool.Get(i).Value;
                float attackRange = attackRangePool.Get(i).Value;

                Ray ray = new Ray(pos, forward);
                
                ref DetectedEnemyPosition detectedEnemyPosition = ref detectEnemyPool.Get(i);
                
                if (Physics.Raycast(ray, out hit, attackRange, _layerMask))
                {
                    detectedEnemyPosition.Value = false;
                    
                    if(hit.transform.TryGetComponent(out Entity entity) == false) continue;
                    if(entity.TryGetData(out Team teamData) == false) continue;
                    if(teamData.Value == team) continue;

                    detectedEnemyPosition.Value = true;
                    
#if UNITY_EDITOR
                    Debug.DrawRay(pos, forward * attackRange);
#endif
                }
                else
                {
                    detectedEnemyPosition.Value = false;
                }
            }
        }
    }
}