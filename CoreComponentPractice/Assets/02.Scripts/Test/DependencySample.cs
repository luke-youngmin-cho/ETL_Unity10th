using System;
using System.Collections;
using UnityEngine;

public class TestDummy
{
    public event Func<int, bool> func;
    public event Func<int, float, bool> func2;
    public event Action action;

    public void Test()
    {
        func += IsBiggerThan0;
        func += a =>
        {
            bool result = false;

            if (a > 0)
                result = true;

            return result;
        };
        func += IsBiggerThan3;
        func += a => a > 3;
        func2 += (x, y) => x > (int)y;
        action += () => Console.WriteLine("Hi");
    }

    bool IsBiggerThan0(int a)
    {
        bool result = false;

        if (a > 0)
            result = true;

        return result;
    }

    bool IsBiggerThan3(int a)
    {
        return a > 3;
    }

    IEnumerator C_DoSomething()
    {
        yield return null;
        yield return new WaitUntil(() => IsBiggerThan3(5));
    }
}




namespace Test.DependencySample
{
    class SwordMan
    {
        public int lv
        {
            get
            {
                return _lv;
            }
            set
            {
                _lv = value;
            }
        }

        private int _lv;
        private SwordForNewbie _swordForNewbie;
        private SwordForIntermediate _swordForIntermediate;
        private SwordForProfessional _swordForProfessional;
        private SwordForLegend _swordForLegend;

        public void Attack()
        {
            if (_lv > 70)
                _swordForLegend.Attack();
            else if (_lv > 50)
                _swordForProfessional.Attack();
            else if (_lv > 30)
                _swordForIntermediate.Attack();
            else
                _swordForNewbie.Attack();
        }
    }

    class SwordForNewbie
    {
        public void Attack() { }
        public void Skill() { }
    }

    class SwordForIntermediate
    {
        public void Attack() { }
        public void Skill() { }
    }

    class SwordForProfessional
    {
        public void Attack() { }
        public void Skill() { }
    }
    class SwordForLegend
    {
        public void Attack() { }
        public void Skill() { }
    }
}
