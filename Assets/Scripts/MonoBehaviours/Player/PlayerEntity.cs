using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    
    public override void Kill()
    {
        //This needs to be fixed ASAP. I keep close eye on you you mmhhmh
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        base.GetHealth();
    }
}
