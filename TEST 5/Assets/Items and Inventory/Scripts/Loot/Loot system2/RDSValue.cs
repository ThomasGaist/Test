using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDSValue<T> : IRDSValue<T>
{

    #region Constructor

    public RDSValue(T value, double probability)
        :this(value, probability, false, false, true)
    { }

    public RDSValue(T value, double probability, bool unique, bool always, bool enabled)
    {
        mvalue = value;
        rdsProbability = probability;
        rdsUnique = unique;
        rdsAlways = always;
        rdsEnabled = enabled;
        rdsTable = null; 

    }
    #endregion

    #region Events

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

    #region IRDSValue<T> Members

    public virtual T rdsValue
    {
        get { return mvalue; }
        set { mvalue = value; }
    }

    private T mvalue;

    #endregion

    #region IRDSObject Members

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

    public string ToString(int indentationLevel)
    {
        string indent = "".PadRight(4 * indentationLevel, ' ');

        string valstr = "(null)";
        if (rdsValue != null)
            valstr = rdsValue.ToString();
        return string.Format(indent + "(RDSValue){0} \"{1}\", Prob:{2}, UAE:{3}{4}{5}",
            this.GetType().Name, valstr, rdsProbability,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"));

    }
    #endregion
}
