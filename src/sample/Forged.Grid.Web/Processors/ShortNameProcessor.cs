using Forged.Grid.Web.Models;

using System.Linq;

namespace Forged.Grid.Web.Processors
{
    public class ShortNameProcessor : IGridProcessor<Person>
    {
        public GridProcessorType ProcessorType { get; set; }

        public ShortNameProcessor()
        {
            ProcessorType = GridProcessorType.Post;
        }

        public IQueryable<Person> Process(IQueryable<Person> items)
        {
            return items.Select(person => new Person(person.Id, person.Name.Substring(0, 1) + ". " + person.Surname, person.Surname)
            {
                Age = person.Age,
                Birthday = person.Birthday,
                IsWorking = person.IsWorking,
                MaritalStatus = person.MaritalStatus,
                Children = person.Children
            });
        }
    }
}
