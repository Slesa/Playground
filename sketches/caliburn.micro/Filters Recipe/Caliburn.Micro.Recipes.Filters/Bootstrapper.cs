namespace Caliburn.Micro.Recipes.Filters {
    using Framework;
    using ViewModels;

    class Bootstrapper : Bootstrapper<CalculatorViewModel> {
        protected override void Configure() {
            FilterFramework.Configure();
        }
    }
}