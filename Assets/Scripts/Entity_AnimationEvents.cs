using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets() => entity.DamageTargets();

    private void DisableMovementAndJump() => entity.EnableMovement(false);
    private void EnableMovementAndJump() => entity.EnableMovement(true);


        


    // private void AttackStarted()
    // {
    // Call method from 'player' script
    // That method should stop movement of the player game object
    // Debug.Log("Attack started!");
    // }
}
