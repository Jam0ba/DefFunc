using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyShoot", story: "Enemy [Shoot] [Player] When In Range", category: "Action", id: "38c37af23266066c0b9154cfb04ac167")]
public partial class EnemyShootAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyShoot> Shoot;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    protected override Status OnUpdate()
    {

        return Shoot.Value.Test() ? Status.Success : Status.Failure;
    }
}

