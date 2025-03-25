using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public class UnitInstaller: EntityInstaller
    {
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private Rigidbody rb;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new MoveDirection { Value = Vector2.zero });
            entity.AddData(new MoveSpeed { Value = moveSpeed });
            entity.AddData(new RigidbodyView { Value = rb});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}