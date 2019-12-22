using System;
using System.Linq; 
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class RDSRandom
{
    #region CRYPTOGRAPHIC IMPLEMENTATION (DISABLED CODE - FOR FUTURE USE - DID NOT WANT TO DELETE IT)
    //private RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
    //private byte[] mbuffer = new byte[4];

    ///// <summary>
    ///// Retrieves the next random value from the random number generator.
    ///// The result is always between 0.0 and the given max-value (excluding).
    ///// </summary>
    ///// <param name="max">The maximum value.</param>
    ///// <returns></returns>
    //public double GetValue(double max)
    //{
    //    rnd.GetBytes(mbuffer);
    //    UInt32 rand = BitConverter.ToUInt32(mbuffer, 0);
    //    double dbl = rand / (1.0 + UInt32.MaxValue);
    //    return dbl * max;
    //}
    #endregion

    #region TYPE INITIALIZER
    private static System.Random rnd = null;

    static RDSRandom()
    {
        SetRandomizer(null);
    }
    #endregion

    #region SETRANDOMIZER METHOD

    public static void SetRandomizer(System.Random randomizer)
    {
        if (randomizer == null)
            rnd = new System.Random();
        else
            rnd = randomizer;
    }
    #endregion

    #region DOUBLE VALUES

    public static double GetDoubleValue(double max)
    {
        return rnd.NextDouble() * max;
    }

    public static double GetDoubleValue(double min, double max)
    {
        return rnd.NextDouble() * max - min;
    }

    #endregion


    #region INTEGER VALUES

    public static int GetIntValue(int max)
    {
        return rnd.Next(max);
    }
    public static int GetIntValue(int min, int max)
    {
        return rnd.Next(min, max);
    }
    #endregion

    #region ROLLDICE METHOD

    public static IEnumerable<int> RollDice(int dicecount, int sidesperdice)
    {
        List<int> rv = new List<int>();
        for (int i = 0; i < dicecount; i++)
            rv.Add(GetIntValue(1, sidesperdice + 1));
        rv.Insert(0, rv.Sum());
        return rv;
    }
    #endregion

    #region ISPERCENTHIT METHOD

    public static bool IsPercentHit(double percent)
    {
        return (rnd.NextDouble() < percent);
    }


    #endregion
}
