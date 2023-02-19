using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSP_OrbitManeuvers.Objects
{
    class StarSystem
    {
        public List<Star> Stars { get; set; }
        public string SystemName { get; set; }

        public StarSystem()
        {
            Stars = new List<Star>();
        }
    }
    
}
