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
