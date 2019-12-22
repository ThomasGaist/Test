using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRDSObject
{
    #region variables
    double rdsProbability { get; set; }
    bool rdsUnique { get; set; }
    bool rdsAlways { get; set; }
    bool rdsEnabled { get; set; }
    RDSTable rdsTable { get; set; }
    #endregion

    #region events
    event EventHandler rdsPreResultEvaluation;

    event EventHandler rdsHit;

    event ResultEventHandler rdsPostResultEvaluation;

    #endregion

    #region methods

    void OnRDSPreResultEvaluation(EventArgs e);
    void OnRDSHit(EventArgs e);
    void OnRDSPostResultEvaluation(ResultEventArgs e);

    //ToString with indentation

    string ToString(int indentationLevel);

    #endregion
}
