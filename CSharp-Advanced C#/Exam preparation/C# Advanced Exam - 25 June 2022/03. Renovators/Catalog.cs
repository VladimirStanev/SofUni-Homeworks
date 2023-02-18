using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renovators
{
    public class Catalog
    {
        List<Renovator> renovators = new List<Renovator>();

        public Catalog(string name, int neededRenovators, string project)
        {
            Name = name;
            NeededRenovators = neededRenovators;
            Project = project;
        }

        public string Name { get; set; }
        public int NeededRenovators { get; set; }
        public string Project { get; set; }

        public int Count => renovators.Count;
        public string AddRenovator(Renovator renovator)
        {
            if (string.IsNullOrEmpty(renovator.Name) || string.IsNullOrEmpty(renovator.Type))
            {
                return "Invalid renovator's information.";
            }
            if (Count == NeededRenovators)
            {
                return "Renovators are no more needed.";
            }
            if (renovator.Rate > 350)
            {
                return "Invalid renovator's rate.";
            }
            renovators.Add(renovator);
            return $"Successfully added {renovator.Name} to the catalog.";
        }
        public bool RemoveRenovator(string name)
        {
            var targettedRenovator = renovators.FirstOrDefault(x => x.Name == name);
            if (targettedRenovator == null)
            {
                return false;
            }
            renovators.Remove(targettedRenovator);
            return true;
        }
        public int RemoveRenovatorBySpecialty(string type)
        {
            var result = 0;
            Renovator renovator = renovators.FirstOrDefault(x => x.Type == type);
            if (renovator != null)
            {
                foreach (Renovator currRenovater in renovators)
                {
                    if (currRenovater.Type == type)
                    {
                        result++;
                    }
                };
                renovators.RemoveAll(x => x.Type == type);
            }
            return result;
        }
        public Renovator HireRenovator(string name)
        {
            Renovator renovator = renovators.FirstOrDefault(x => x.Name == name);
            if (renovator != null)
            {
                renovator.Hired = true;
                return renovator;
            }
            return null;
        }
        public List<Renovator> PayRenovators(int days)
        {
            List<Renovator> paidRenovators = renovators.Where(x => x.Days >= days).ToList();
            return paidRenovators;
        }
        public string Report()
        {
            List<Renovator> notHiredRenovators = new List<Renovator>();
            foreach(Renovator renovator1 in renovators)
            {
                if(renovator1.Hired == false)
                {
                    notHiredRenovators.Add(renovator1);
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Renovators available for Project {Project}:");
            foreach(Renovator renovator in notHiredRenovators)
            {
                stringBuilder.AppendLine(renovator.ToString());
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
