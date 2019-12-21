using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RDSTable : IRDSTable
{
    #region CONSTRUCTORS
    public RDSTable()
        : this(null, 1, 1, false, false, true)
    {
    }

    public RDSTable(IEnumerable<IRDSObject> contents, int count, double probability, bool unique, bool always, bool enabled)
    {
        if (contents != null)
            mcontents = contents.ToList();
        else
            ClearContents();
        rdsCount = count;
        rdsProbability = probability;
        rdsUnique = unique;
        rdsAlways = always;
        rdsEnabled = enabled;
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

    #region COUNT

    public int rdsCount { get; set; }
    #endregion

    #region CONTENTS

    public IEnumerable<IRDSObject> rdsContents
    {
        get { return mcontents; }
    }
    private List<IRDSObject> mcontents = null;

    public virtual void ClearContents()
    {
        mcontents = new List<IRDSObject>();
    }

    public virtual void AddEntry(IRDSObject entry)
    {
        mcontents.Add(entry);
        entry.rdsTable = this; 
    }

    public virtual void AddEntry(IRDSObject entry, double probability)
    {
        mcontents.Add(entry);
        entry.rdsProbability = probability;
        entry.rdsTable = this; 
    }

    public virtual void AddEntry(IRDSObject entry, double probability, bool unique, bool always, bool enabled)
    {
        mcontents.Add(entry);
        entry.rdsProbability = probability;
        entry.rdsUnique = unique;
        entry.rdsAlways = always;
        entry.rdsEnabled = enabled;
        entry.rdsTable = this;
    }

    public virtual void RemoveEntry(IRDSObject entry)
    {
        mcontents.Remove(entry);
        entry.rdsTable = null; 
    }

    public virtual void RemoveEntry(int index)
    {
        IRDSObject entry = mcontents[index];
        entry.rdsTable = null;
        mcontents.RemoveAt(index);
    }
    #endregion

    #region RESULT
    private List<IRDSObject> uniquedrops = new List<IRDSObject>();

    private void AddToResult(List<IRDSObject> rv, IRDSObject o)
    {
        if(!o.rdsUnique || !uniquedrops.Contains(o))
        {
            if (o.rdsUnique)
                uniquedrops.Add(o);
            if (!(o is RDSNullValue))
            {
                if (o is IRDSTable)
                {
                    rv.AddRange(((IRDSTable)o).rdsResult);
                }
                else
                {
                    IRDSObject adder = o;
                    if (o is IRDSObjectCreator)
                        adder = ((IRDSObjectCreator)o).rdsCreateInstance();
                    rv.Add(adder);
                    o.OnRDSHit(EventArgs.Empty);
                }
            }
            else
                o.OnRDSHit(EventArgs.Empty);
        }
    }

    #endregion
}