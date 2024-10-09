using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    public override void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
