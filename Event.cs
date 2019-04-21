using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Event
    {
        private double time;
        private string type;

        public Event(double time, string type)
        {
            this.time = time;
            this.type = type;
        }

        public double Time
        {
            get { return time; }
            set { this.time = value; }
        }

        public string Type
        {
            get { return type; }
            set { this.type = value; }
        }

    }
}
