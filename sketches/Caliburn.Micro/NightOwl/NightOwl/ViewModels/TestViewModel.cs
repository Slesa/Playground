using Caliburn.Micro;
using NightOwl.Core;

namespace NightOwl.Views
{
    public class TestViewModel : Screen, IModule
    {
        public TestViewModel()
        {
            DisplayName = "Test Module";
        }
    }
}