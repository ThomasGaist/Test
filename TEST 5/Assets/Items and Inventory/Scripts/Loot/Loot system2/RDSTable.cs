using System.Text;
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

    public virtual IEnumerable<IRDSObject> rdsResult
    {
        get
        {
            List<IRDSObject> rv = new List<IRDSObject>();
            uniquedrops = new List<IRDSObject>();

            foreach (IRDSObject o in mcontents)
                o.OnRDSPreResultEvaluation(EventArgs.Empty);

            foreach (IRDSObject o in mcontents.Where(e => e.rdsAlways && e.rdsEnabled))
                AddToResult(rv, o);

            int alwayscnt = mcontents.Count(e => e.rdsAlways && e.rdsEnabled);
            int realdropcnt = rdsCount - alwayscnt;

            if(realdropcnt > 0)
            {
                for (int dropcount = 0; dropcount < realdropcnt; dropcount++)
                {
                    IEnumerable<IRDSObject> dropables = mcontents.Where(e => e.rdsEnabled && !e.rdsAlways);

                    double hitvalue = RDSRandom.GetDoubleValue(dropables.Sum(e => e.rdsProbability));

                    double runningvalue = 0;
                    foreach (IRDSObject o in dropables)
                    {
                        runningvalue += o.rdsProbability;
                        if(hitvalue < runningvalue)
                        {
                            AddToResult(rv, o);
                            break; 
                        }
                    }
                }
            }

            ResultEventArgs rea = new ResultEventArgs(rv);
            foreach (IRDSObject o in rv)
                o.OnRDSPostResultEvaluation(rea);

            return rv; 

        }
    }

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

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(indent + "(RDSTable){0} Entries:{1}, Prob:{2}, UAE:{3}{4}{5}{6}",
            this.GetType().Name, mcontents.Count, rdsProbability,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"), (mcontents.Count > 0 ? "\r\n" : ""));

        foreach (IRDSObject o in mcontents)
            sb.AppendLine(indent + o.ToString(indentationLevel + 1));
        return sb.ToString();
        

    }
    #endregion
}