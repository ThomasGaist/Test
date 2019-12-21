using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDSNullValue : RDSValue<object>
{
 public RDSNullValue(double probability)
        : base(null, probability, false, false, true) { }
}
