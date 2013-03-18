using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A.I.NXT
{
    public class NxtControl
    {
        public NxtControl();
        public bool startOperation(int m1, int m2);
        public bool activeConnectionOne();
        public bool activeConnectionTwo();
        public bool magnetControl(bool magnet);
    }
}
