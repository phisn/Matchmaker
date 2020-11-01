using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaker.Actions
{
    public abstract class Action
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Usage { get; }

        public abstract bool Call(string[] arguments);
    }
}
