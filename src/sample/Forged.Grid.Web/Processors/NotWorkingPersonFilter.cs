using Forged.Grid.Web.Models;

using System.Linq;

namespace Forged.Grid.Web.Processors
{
    public class NotWorkingPersonFilter : IGridProcessor<Person>
    {
        public GridProcessorType ProcessorType { get; set; }

        public NotWorkingPersonFilter()
        {
            ProcessorType = GridProcessorType.Pre;
        }

        public IQueryable<Person> Process(IQueryable<Person> items)
        {
            return items.Where(item => item.IsWorking == false);
        }
    }
}
