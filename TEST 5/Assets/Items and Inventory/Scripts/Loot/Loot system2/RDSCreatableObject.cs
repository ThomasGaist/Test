using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RDSCreatableObject : RDSObject, IRDSObjectCreator
{
    public double rdsProbability { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool rdsUnique { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool rdsAlways { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool rdsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event EventHandler rdsPreResultEvaluation;
    public event EventHandler rdsHit;
    public event ResultEventHandler rdsPostResultEvaluation;

    public void OnRDSHit(EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnRDSPostResultEvaluation(ResultEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnRDSPreResultEvaluation(EventArgs e)
    {
        throw new NotImplementedException();
    }


    public string ToString(int indentationLevel)
    {
        throw new NotImplementedException();
    }


    public virtual IRDSObject rdsCreateInstance()
    {
        return (IRDSObject)Activator.CreateInstance(this.GetType());
    }

}
