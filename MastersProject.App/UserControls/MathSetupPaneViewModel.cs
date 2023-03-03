using MastersProject.App.CoordinateSystem;
using MastersProject.App.Infrastructure;

namespace MastersProject.App.UserControls;

internal class MathSetupPaneViewModel : PropertyChangedBase
{
    public MathSetupPaneViewModel()
    {
        Graph = new(1023, 90);

    }

    public string Title { get; init; }
    public CoordinateSystemViewModel Graph { get; }
}