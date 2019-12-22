using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDSObject : IRDSObject
{
    #region CONSTRUCTORS

    public RDSObject()
        :this(0)
    {
    }

    public RDSObject(double probability)
        :this(probability, false, false, true)
    { }


    public RDSObject(double probability, bool unique, bool always, bool enabled)
    {
        rdsProbability = probability;
        rdsUnique = unique;
        rdsAlways = always;
        rdsEnabled = enabled;
        rdsTable = null; 
    }

    #endregion

    #region EVENTS

    public event EventHandler rdsPreResultEvaluation;

    public event EventHandler rdsHit;

    public event ResultEventHandler rdsPostResultEvaluation;

    public virtual void OnRDSPreResultEvaluation(EventArgs e)
    {
        if (rdsPreResultEvaluation != null) rdsPreResultEvaluation(this, e);
    }

    public virtual void OnRDSHit(EventArgs e)
    {
        if (rdsHit != null) rdsHit(this, e);
    }

    public virtual void OnRDSPostResultEvaluation(ResultEventArgs e)
    {
        if (rdsPostResultEvaluation != null) rdsPostResultEvaluation(this, e);
    }

    #endregion

    #region IRDSObject members

    public double rdsProbability { get; set; }

    public bool rdsUnique { get; set; }

    public bool rdsAlways { get; set; }

    public bool rdsEnabled { get; set; }

    public RDSTable rdsTable { get; set; }

    #endregion

    #region TOSTRING

    public override string ToString()
    {
        return ToString(0);
    }

    public string ToString(int indentationlevel)
    {
        string indent = "".PadRight(4 * indentationlevel,' ');

        return string.Format(indent + "(RDSObject){0} Prob:{1}, UAE:{2}{3}{4}",
            this.GetType().Name, rdsProbability,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"));
    }

    #endregion
}
