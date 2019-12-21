using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RDSCreatableObject : RDSObject, IRDSObjectCreator
{
   

    public virtual IRDSObject rdsCreateInstance()
    {
        return (IRDSObject)Activator.CreateInstance(this.GetType());
    }

}
