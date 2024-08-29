using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHp
{
    int hp { get; }
    int hpMax { get; }
    void Damage(int amount);
    void Heal(int amount);
    event Action<int, int> onHpChanged; // before, after
}
