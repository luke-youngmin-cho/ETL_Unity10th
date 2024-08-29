using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : NetworkBehaviour, IHp
{
    // Networked : StateAuthority 가 쓰기(송신), 그외 읽기(수신)
    [Networked]
    [field: SerializeField] public int hp { get; set; }
    [Networked]
    [field: SerializeField] public int hpMax { get; set; }

    public event Action<int, int> onHpChanged;

    public void Damage(int amount)
    {
        int previousHp = hp;
        hp -= amount;
        hp = Mathf.Clamp(hp, 0, hpMax);
        onHpChanged?.Invoke(previousHp, hp);
        Debug.Log($"{name} : Damaged.. changed hp {previousHp} -> {hp}");
    }

    public void Heal(int amount)
    {
        int previousHp = hp;
        hp += amount;
        hp = Mathf.Clamp(hp, 0, hpMax);
        onHpChanged?.Invoke(previousHp, hp);
    }
}
