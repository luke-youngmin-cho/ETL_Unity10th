using System;
using System.Collections.Generic;

namespace ETL10.GameElements.StatSystems
{
    public class Stat
    {
        public Stat(StatType type, int value)
        {
            this._modifiers = new List<StatModifier>();
            this.type = type;
            this.value = value;
        }

        public Stat(StatType type, int value, int modifiersCapacity)
        {
            this._modifiers = new List<StatModifier>(modifiersCapacity);
            this.type = type;
            this.value = value;
        }


        public StatType type { get; private set; }
        public int value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                int before = _value;
                _value = value;
                onValueChanged?.Invoke(before, value);
                UpdateValueModified();
            }
        }
        public int valueModified
        {
            get => _valueModified;
            set
            {
                if (_valueModified == value)
                    return;

                int before = _valueModified;
                _valueModified = value;
                onValueModifiedChanged?.Invoke(before, value);
            }
        }

        private int _value;
        private int _valueModified;
        private List<StatModifier> _modifiers;

        public event Action<int, int> onValueChanged;
        public event Action<int, int> onValueModifiedChanged;


        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            UpdateValueModified();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            UpdateValueModified();
        }

        private void UpdateValueModified()
        {
            int sumAddFlat = 0;
            double sumAddPercent = 0;
            double sumMulPercent = 0;

            foreach (StatModifier modifier in _modifiers)
            {
                switch (modifier.modType)
                {
                    case StatModType.None:
                        break;
                    case StatModType.AddFlat:
                        {
                            sumAddFlat += modifier.modValue;
                        }
                        break;
                    case StatModType.AddPercent:
                        {
                            sumAddPercent += modifier.modValue / 100.0;
                        }
                        break;
                    case StatModType.MulPercent:
                        {
                            sumMulPercent *= modifier.modValue / 100.0;
                        }
                        break;
                    default:
                        break;
                }
            }

            valueModified =
                (int)((_value + sumAddFlat) + (_value * sumAddPercent) + (_value * sumMulPercent));
        }
    }
}
