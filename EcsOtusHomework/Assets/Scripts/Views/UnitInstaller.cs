using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Views
{
    public class UnitInstaller: EntityInstaller
    {
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private int health = 10;
        [SerializeField] private float attackRange = 10f;
        [Space(5)]
        [SerializeField] private Rigidbody rb;
        [Space(5)] 
        [Header("Weapon setup")] 
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Entity bulletPrefab;
        [SerializeField] private float reloadDelay;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new Direction { Value = Vector2.zero });
            entity.AddData(new MoveSpeed { Value = moveSpeed });
            entity.AddData(new RigidbodyView { Value = rb });
            entity.AddData(new Health { Value = health });
            entity.AddData(new AttackRange { Value = attackRange });
            entity.AddData(new DetectedEnemyPosition());
            entity.AddData(new FireDelay
            {
                Limit = reloadDelay, 
                Value = reloadDelay
            }); 
            entity.AddData(new BulletWeapon()
            {
                FirePoint = bulletSpawnPoint, 
                BulletPrefab = bulletPrefab
            });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}