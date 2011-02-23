using Caliburn.Micro;
using NightOwl.Core;

namespace NightOwl.Test.Module.ViewModels
{
    public class TestViewModel : Screen, IModule
    {
        public TestViewModel()
        {
            DisplayName = "Test Module";
        }

    }
}