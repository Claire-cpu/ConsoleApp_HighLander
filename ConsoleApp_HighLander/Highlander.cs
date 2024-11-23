using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public abstract class Highlander
    {
        private int _id;
        private string _name;
        private int _powerLevel;
        private int[] _position;
        private int _age;
        private bool _isAlive;
        private BehaviorStrategy _behavior;

        public Highlander(int id, string name, int age, int powerLevel, int[] position, bool isAlive) { 
            _id = id;
            _name = name;
            _age = age;
            _powerLevel = powerLevel;
            _position = position;
            _isAlive = isAlive;
        }
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public int PowerLevel { get { return _powerLevel; } set { _powerLevel = value; } }
        public int[] Position { get { return _position; } set { _position = value; } }
        public int Age { get { return _age; } set { _age = value; } }
        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        public BehaviorStrategy Behavior
        {
            get { return _behavior; }
            set { _behavior = value; }
        }
        public abstract bool IsGood
        {
            get;
            set;
        }  

    }
}
