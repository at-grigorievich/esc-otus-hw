using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour 
    {
        private EcsWorld _world;
        private EcsWorld _events; 
        private IEcsSystems _systems;
        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = new EntityManager();
            
            _world = new EcsWorld();
            _events = new EcsWorld();
            
            _systems = new EcsSystems (_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);
            
            _systems
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                .Add(new ExampleSystem())
                .Add(new MovementSystem())
                .Add(new FireRequestSystem())
                .Add(new SpawnRequestSystem())
                
                .Add(new TransformViewSystem())
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS));
#endif
        }
        
        private void Start ()
        {
            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
            _systems.Init ();
        }

        private void Update () 
        {
            // process systems here.
            _systems?.Run ();
        }

        private void OnDestroy () 
        {
            if (_systems != null) 
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}