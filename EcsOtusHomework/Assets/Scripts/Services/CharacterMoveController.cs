using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Services
{
    public class CharacterMoveController: MonoBehaviour
    {
        [SerializeField] private Entity character;

        private void Update()
        {
            Vector3 direction = MoveInput.GetDirection();
            ref MoveDirection moveDirection = ref character.GetData<MoveDirection>();
            moveDirection.Value = direction;
        }
    }
}