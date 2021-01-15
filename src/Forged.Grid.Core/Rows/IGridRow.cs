namespace Forged.Grid
{
    public interface IGridRow<out T>
    {
        T Model { get; }
        int Index { get; }

        GridHtmlAttributes? Attributes { get; set; }
    }
}
