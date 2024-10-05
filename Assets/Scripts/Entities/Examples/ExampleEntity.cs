using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEntity : Entity
{
    //Derives basic behaviour from the Entity class.

    //When we want we can override the parent class behaviour by overriding the functions
    public override void Damage(global::System.Single damage)
    {
        base.Damage(damage);
    }
}
