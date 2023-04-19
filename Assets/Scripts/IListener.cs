using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EVENT_TYPE
{
    GAME_INIT, GAME_END, HEALTH_CHANGE, DEAD, CLASH

}
public interface IListener
{
    void OnEvent(EVENT_TYPE Event_Type, Component Sender, Object Param = null);
}


