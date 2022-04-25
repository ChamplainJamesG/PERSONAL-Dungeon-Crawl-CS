using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    public int Id;
    public string Name;
    public bool Negative;
    public int Length;
}

/// <summary>
/// Only affects things that specifically are outgoing. 
/// Will be used immediately after an ability is cast. 
/// (For example +50% attack damage is an outgoing status)
/// </summary>
public class OutgoingStatus : Status
{

}

/// <summary>
/// Will only affect things that are specifically incoming. 
/// Is used right as an ability is taken.
/// For example, +50% healing recieved is an incoming status.
/// </summary>
public class IncomingStatus : Status
{

}

// Unsure if necessary at the moment. 
/// <summary>
/// A two way status affects anything that is outgoing *and* incoming.
/// It happens as an ability is cast, and when ability is received. 
/// </summary>
public class TwoWayStatus : Status
{

}
