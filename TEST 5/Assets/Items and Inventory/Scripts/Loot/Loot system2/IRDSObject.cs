using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRDSObject
{
    double rdsProbability { get; set; }
    bool rdsUnique { get; set; }
    bool rdsAlways { get; set; }
    bool rdsEnabled { get; set; }
}
