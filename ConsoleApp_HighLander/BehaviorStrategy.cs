using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HighLander
{
    public interface BehaviorStrategy
    {
        void execute(ConsoleApp app, Highlander self, Highlander opponent=null);
    }
}
