using Client.Data;
using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour
    {
        public SceneData sceneData;
        
        private EcsWorld _world;

        private IEcsSystems _systems;
        private IEcsSystems _fixedSystems;
        
        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = new EntityManager();
            
            _world = new EcsWorld();
            
            _systems = new EcsSystems (_world);
            _fixedSystems = new EcsSystems (_world);
            
            _systems.AddWorld(new EcsWorld(), EcsWorlds.EVENTS);
            
            _systems
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                //.Add(new ExampleSystem())
                //.Add(new MovementSystem())
                //.Add(new FireRequestSystem())
                //.Add(new SpawnRequestSystem())
                .Add(new SpawnUnitRequestSystem())
                .Add(new SpawnUnitSystem())
                .Add(new TargetMovementSystem())
                //.Add(new TransformViewSystem())
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS));
#endif

            _fixedSystems
                .Add(new RigidbodyViewMoveSystem());
        }
        
        private void Start ()
        {
            _entityManager.Initialize(_world);
            _systems
                .Inject(_entityManager)
                .Inject(sceneData);
            _fixedSystems.Inject();
            _systems.Init ();
            _fixedSystems.Init();
        }

        private void Update () 
        {
            // process systems here.
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
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