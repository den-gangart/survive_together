namespace SurviveTogether.UI
{
    public interface ISelectableItem
    {
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }
}