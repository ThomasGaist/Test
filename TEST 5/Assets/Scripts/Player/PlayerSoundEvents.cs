using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvents: MonoBehaviour
{
  public void FootStep()
    {
        GameEvents.current.PlayerFootstep();
    }
}
