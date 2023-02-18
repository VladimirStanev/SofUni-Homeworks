using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniKindergarten
{
    public class Kindergarten
    {
        public Kindergarten(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Registry = new List<Child>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Child> Registry { get; set; }
        
        public int ChildrenCount => Registry.Count;

        public bool AddChild(Child child)
        {
            if (Capacity > Registry.Count)
            {
                Registry.Add(child);
                return true;
            }
            return false;
        }
        public bool RemoveChild(string childFullName)
        {
            string[] firstNlastNames = childFullName.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            string firstName = firstNlastNames[0];
            string lastName = firstNlastNames[1];
            Child childToRemove = Registry.Where(x => x.FirstName == firstName).Where(x => x.LastName == lastName).FirstOrDefault();
            if(childToRemove != null)
            {
                Registry.Remove(childToRemove);
                return true;
            }
            return false;
        }
        public Child GetChild(string childFullName)
        {
            string[] firstNlastNames = childFullName.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            string firstName = firstNlastNames[0];
            string lastName = firstNlastNames[1];
            Child childToReturn = Registry.Where(x => x.FirstName == firstName).Where(x => x.LastName == lastName).FirstOrDefault();
            if(childToReturn != null)
            {
                return childToReturn;
            }
            return null;
        }
        public string RegistryReport()
        {
            StringBuilder sb = new();

            var orderedChildren = Registry.OrderByDescending(x => x.Age)
                .ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            sb.AppendLine($"Registered children in {Name}:");
            foreach (var child in orderedChildren)
            {
                sb.AppendLine(child.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
