using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IObservedGO
    {
        public void Attach(IGOObserver observer);

        public void Detach(IGOObserver observer);

        public void Notify();
    }
}
