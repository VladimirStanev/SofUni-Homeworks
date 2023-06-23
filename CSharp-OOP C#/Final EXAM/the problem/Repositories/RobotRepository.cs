using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robotsList;

        public RobotRepository()
        {
            robotsList = new List<IRobot>();           
        }

        public List<IRobot> Robots => robotsList;

        public void AddNew(IRobot model)
        {
            robotsList.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return this.robotsList.FirstOrDefault(x => x.InterfaceStandards.Contains(interfaceStandard));    
        }

        public IReadOnlyCollection<IRobot> Models()
        {
            return robotsList.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            IRobot robot = robotsList.FirstOrDefault(x => x.Model == typeName);
            if (robot != null)
            {
                robotsList.Remove(robot);
                return true;
            }

            return false;
        }
    }
}
