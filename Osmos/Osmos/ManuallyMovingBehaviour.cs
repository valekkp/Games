using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    class ManuallyMovingBehaviour : IMovable
    {
        private readonly Cell source;
        public ManuallyMovingBehaviour(Cell source)
        {
            this.source = source;
        }

        public void Move()
        {
            
        }
    }
}
