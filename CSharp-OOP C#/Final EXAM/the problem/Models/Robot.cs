using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        protected Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = BatteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;

            interfaceStandards = new List<int>();
        }


        private string model;
        public string Model
        {
            get { return model; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }

                model = value;
            }
        }


        private int batteryCapacity;
        public int BatteryCapacity
        {
            get { return batteryCapacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }

                batteryCapacity = value;
            }
        }

        private int batteryLevel;
        public int BatteryLevel { get; private set; }


        private int comvertionCapacityIndex;
        public int ConvertionCapacityIndex { get; private set; }

        //Намерих разлика при Robot .
        //В условието пише, че конструктора приема "int conversionCapacityIndex",
        //а в скелета е предварително създадено, като "ConvertionCapacityIndex".
        //Това правописна грешка ли е или да го оставяме така?


        private List<int> interfaceStandards;
        public IReadOnlyCollection<int> InterfaceStandards
        {
            get { return interfaceStandards.AsReadOnly(); }
        }
        public void Eating(int minutes)
        {
            int convertedFoodToEnergy = ConvertionCapacityIndex * minutes;

            int increasedLevel = BatteryLevel + convertedFoodToEnergy;

            BatteryLevel = increasedLevel;

            if (increasedLevel >= BatteryCapacity)
            {
                BatteryLevel = BatteryCapacity;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if(BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"{nameof(Robot)} {Model}:")
                .AppendLine($"--Maximum battery capacity: {BatteryCapacity}")
                .AppendLine($"--Current battery level: {BatteryLevel}");               

            if(interfaceStandards.Count > 0)
            {
                sb.AppendLine($"--Supplements installed: {string.Join(" ", interfaceStandards)}");
            }
            else
            {
                sb.AppendLine("--Supplements installed: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
