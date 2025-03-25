using System;
using Client.Data;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct Team
    {
        public TeamType Value;
    }
    
    
    
    
    
    [Serializable]
    public struct Position
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }

    [Serializable]
    public struct MoveDirection
    {
        public Vector3 Value;
    }

    [Serializable]
    public struct MoveSpeed
    {
        public float Value;
    }

    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }

    // Marker
    [Serializable]
    public struct FireRequest
    {
    }

    
    // Marker
    [Serializable]
    public struct SpawnRequest
    {
    }

    [Serializable]
    public struct Prefab
    {
        public Entity Value;
    }

    [Serializable]
    public struct BulletWeapon
    {
        public Transform FirePoint;
        public Entity BulletPrefab;
    }
}