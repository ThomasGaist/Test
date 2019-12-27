using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu]
public class Item : ScriptableObject, IRDSObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public double rdsProbability { get; set; }
    public bool rdsUnique { get; set; }
    public bool rdsAlways { get; set; }
    public bool rdsEnabled { get; set; }
    public RDSTable rdsTable { get; set; }

    public string ItemName;
    public Sprite Icon;
    public int Rarity;

    public event EventHandler rdsPreResultEvaluation;
    public event EventHandler rdsHit;
    public event ResultEventHandler rdsPostResultEvaluation;

    private void Awake()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

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

    public override string ToString()
    {
        return ToString(0);
    }

    public string ToString(int indentationlevel)
    {
        string indent = "".PadRight(4 * indentationlevel, ' ');

        return string.Format(indent + "(RDSObject){0} Prob:{1}, UAE:{2}{3}{4}",
            this.GetType().Name, rdsProbability,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"));
    }
}
 