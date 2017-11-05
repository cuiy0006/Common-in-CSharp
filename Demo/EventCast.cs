using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class EventCast
    {
        public static void ShowEvent()
        {
            Heater h = new Heater(80);
            Cooler c = new Cooler(60);

            Thermostat Thermo = new Thermostat();
            Thermo.OnTemperatureChanged += h.OnTemperatureChanged;
            Thermo.OnTemperatureChanged += c.OnTemperatureChanged;

            Thermo.Temperature = 50;
        }
    }

    class Cooler
    { 
        public Cooler(float temperature)
        {

            _Temperature = temperature;
        }

        private float _Temperature;
        public float Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        }

        public void OnTemperatureChanged(float Temperature)
        {
            if (Temperature > _Temperature)
                Console.WriteLine("Cooler off");
            else if (Temperature < _Temperature)
                Console.WriteLine("Cooler on");
            else
                Console.WriteLine("Cooler keep Stat");
        }
    }

    class Heater
    {
        public Heater(float temperature)
        {

            _Temperature = temperature;
        }

        private float _Temperature;
        public float Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        }

        public void OnTemperatureChanged(float Temperature)
        {
            if (Temperature > _Temperature)
                Console.WriteLine("Heater on");
            else if (Temperature < _Temperature)
                Console.WriteLine("Heater off");
            else
                Console.WriteLine("Heater keep Stat");
        }
    }


    class Thermostat
    {
        //public Action<float> OnTemperatureChanged;

        public delegate void TemperatureChangedHandler(float Temperature);
        public event TemperatureChangedHandler OnTemperatureChanged;

        private float _Temperature;
        public float Temperature
        {
            get { return _Temperature; }
            set 
            { 
                if(_Temperature !=value)
                {
                    _Temperature = value;

                    var LocalDelegate = OnTemperatureChanged;
                    //OnTemperatureChanged = null;     LocalDelegate will still has value.
                    if (LocalDelegate != null)
                        LocalDelegate(_Temperature);
                }

            }
        }

    
    }




}
