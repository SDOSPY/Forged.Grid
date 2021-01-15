namespace Forged.Grid
{
    public class GridRow<T> : IGridRow<T>
    {
        public T Model { get; }
        public int Index { get; }

        public GridHtmlAttributes? Attributes { get; set; }

        public GridRow(T model, int index)
        {
            Index = index;
            Model = model;
        }
    }
}
