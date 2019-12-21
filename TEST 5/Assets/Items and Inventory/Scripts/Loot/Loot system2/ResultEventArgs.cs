using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultEventArgs : EventArgs
{
   
  public ResultEventArgs(IEnumerable<IRDSObject> result)
    {
        Result = result;
    }

  public IEnumerable<IRDSObject> Result { get; private set; } 
}

public delegate void ResultEventHandler(object sender, ResultEventArgs e);
