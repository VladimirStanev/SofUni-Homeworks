using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private string[] allowedRobotTypes = new string[] 
        { "DomesticAssistant", "IndustrialAssistant" };

        private string[] allowedSupplimentTypes = new string[]
        { "SpecializedArm", "LaserRadar" };

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        private SupplementRepository supplements;
        private RobotRepository robots;

        public string CreateRobot(string model, string typeName)
        {
            if(!allowedRobotTypes.Contains(typeName))
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);    
            }

            Robot robot = null;
            if(typeName == typeof(DomesticAssistant).Name)
            {
                robot = new DomesticAssistant(model);
            }
            if (typeName == typeof(IndustrialAssistant).Name)
            {
                robot = new IndustrialAssistant(model);
            }

            robots.AddNew(robot);

            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }


        public string CreateSupplement(string typeName)
        {
            if(!allowedSupplimentTypes.Contains(typeName))
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            Supplement supplement = null;
            if(typeName == typeof(SpecializedArm).Name)
            {
                supplement = new SpecializedArm();
            }
            if (typeName == typeof(LaserRadar).Name)
            {
                supplement = new LaserRadar();
            }

            supplements.AddNew(supplement);

            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }


        public string UpgradeRobot(string model, string supplementTypeName)
        {
            var supplement = supplements.Models().First(x => x.GetType().Name == supplementTypeName);

            var selectedRobots = this.robots.Models().Where(x => x.Model == model);
            var newRobots = selectedRobots.Where(x => x.InterfaceStandards.Contains(supplement.InterfaceStandard));

            if(selectedRobots.Count() == newRobots.Count())
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            var upgradeNeeded = selectedRobots.First(x => !x.InterfaceStandards.Contains(supplement.InterfaceStandard));

            upgradeNeeded.InstallSupplement(supplement);
            this.supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName); 
        }


        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            throw new NotImplementedException();

            var workingRobots = robots.Models().Where(x => x.InterfaceStandards.Contains(intefaceStandard)).ToList();
                //FindAll(x => x.InterfaceStandards.Contains(intefaceStandard)).ToList();

            if(workingRobots.Count == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            workingRobots = workingRobots.OrderByDescending(x => x.BatteryLevel).ToList();
            //Robot robot = null;
            //robot.FindByStandard 
            int sum = workingRobots.Sum(x => x.BatteryLevel);

            if(sum < totalPowerNeeded)
            {
                int powerNeeded = totalPowerNeeded - sum;
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, powerNeeded);
            }

            int counter = 0;
            foreach(IRobot robot in workingRobots)
            {
                if(robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    counter++;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    counter++;
                }
            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);

        }

        public string Report()
        {
            var toPrint = robots.Robots;

            StringBuilder sb = new StringBuilder();

            foreach(var bla in toPrint)
            {
                sb.AppendLine(bla.ToString());
            }

            return sb.ToString();
        }

        public string RobotRecovery(string model, int minutes)
        {
            throw new NotImplementedException();
        }
    }
}
