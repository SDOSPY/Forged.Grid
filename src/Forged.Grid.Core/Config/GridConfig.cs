using System;

namespace Forged.Grid
{
    public class GridConfig
    {
        public string Name { get; set; }
        public GridColumnConfig[] Columns { get; set; }

        public GridConfig()
        {
            Name = "";
            Columns = Array.Empty<GridColumnConfig>();
        }
    }
}
