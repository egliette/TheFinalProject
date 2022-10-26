using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterState
{
    IMonsterState DoState();
    string StateName();
}
