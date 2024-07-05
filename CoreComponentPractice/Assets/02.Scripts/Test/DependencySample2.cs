using System.Collections.Generic;

namespace Test.DependencySample2
{
    class SwordMan
    {
        public SwordMan()
        {
            _swordRepository = new SwordRepository();
        }

        public int lv
        {
            get
            {
                return _lv;
            }
            set
            {
                if (_lv == value)
                    return;

                _lv = value;
                sword = _swordRepository.GetProperSword(value);
            }
        }

        private int _lv;
        public ISword sword { get; set; }
        private SwordRepository _swordRepository;


        public void Attack()
        {
            sword.Attack();
        }
    }

    public class SwordRepository
    {
        public SwordRepository()
        {
            _swords = new List<ISword>
            {
                new SwordForNewbie(),
                new SwordForIntermediate(),
                new SwordForProfessional(),
                new SwordForLegend(),
            };
        }

        List<ISword> _swords;

        public ISword GetProperSword(int lv)
        {
            int index = lv / 20;
            index = index < _swords.Count ? index : _swords.Count - 1;
            return _swords[index];
        }
    }

    public interface ISword
    {
        void Attack();
        void Skill();
    }

    class SwordForNewbie : ISword
    {
        public void Attack() { }
        public void Skill() { }
    }

    class SwordForIntermediate : ISword
    {
        public void Attack() { }
        public void Skill() { }
    }

    class SwordForProfessional : ISword
    {
        public void Attack() { }
        public void Skill() { }
    }
    class SwordForLegend : ISword
    {
        public void Attack() { }
        public void Skill() { }
    }
}
