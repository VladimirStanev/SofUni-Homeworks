using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> supplementsList;

        public SupplementRepository()
        {
            supplementsList = new List<ISupplement>();
        }

        public List<ISupplement> Supplements { get { return supplementsList; } }

        public IReadOnlyCollection<ISupplement> Models()
        {
            return supplementsList.AsReadOnly();
        }

        public void AddNew(ISupplement model)
        {
            supplementsList.Add(model);
        }
        public bool RemoveByName(string typeName)
        {
            ISupplement supplement = supplementsList.FirstOrDefault(x => x.GetType().Name == typeName);
            if(supplement != null)
            {
                supplementsList.Remove(supplement);
                return true;
            }

            return false;
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            ISupplement supplement = supplementsList.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);

            if(supplement == null)
            {
                return null;
            }

            return supplement;
        }
    }
}
