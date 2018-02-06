namespace de.inc47.AchievementPlanner.ViewModel
{
  internal class TabViewModel : ITabViewModel
  {
    public TabViewModel(string header, IViewModelBase content)
    {
      Header = header;
      Content = content;
    }

    public string Header { get; }
    public IViewModelBase Content { get; }
  }
}