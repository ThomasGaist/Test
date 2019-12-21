using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRDSObjectCreator : IRDSObject
{
    IRDSObject rdsCreateInstance();
}
