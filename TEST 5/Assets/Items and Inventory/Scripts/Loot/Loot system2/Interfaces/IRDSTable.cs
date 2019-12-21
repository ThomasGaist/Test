using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRDSTable: IRDSObject
{
	#region variables

    int rdsCount { get; set; }

    IEnumerable<IRDSObject> rdsContents { get; }
    IEnumerable<IRDSObject> rdsResult { get; }


	#endregion
}
