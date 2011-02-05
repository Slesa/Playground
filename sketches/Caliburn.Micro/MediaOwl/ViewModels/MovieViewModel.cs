using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Resources;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MovieViewModel))]
    public class MovieViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region Constructor
        [ImportingConstructor]
        public MovieViewModel([ImportMany(AllowRecomposition = true)] IEnumerable<IChildScreen<MovieViewModel>> childScreens)
        {
            DisplayName = AppStrings.MovieTitle;
            FillItems(childScreens);
        }

        #endregion

        #region Methods

        internal void FillItems<TParent>(IEnumerable<IChildScreen<TParent>> childScreens)
            where TParent : IConductor
        {
            if (childScreens == null || childScreens.Count() == 0)
                return;

            var homeScreens = childScreens.Where(
                x => x.GetType().Name.ToString().Contains("HomeViewModel")).OrderBy(x => x.Order);

            foreach (var home in homeScreens)
            {
                if (home.Order != null && home.Order < Items.Count)
                    Items.Insert((int) home.Order, home);
                else
                    Items.Add(home);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if (Items.Count > 0)
                ActivateItem(Items.FirstOrDefault());
        }

        public override void ActivateItem(IScreen item)
        {
            base.ActivateItem(CheckIfScreenExists(item));
        }

        private IScreen CheckIfScreenExists(IScreen item)
        {
            if (item.IsSingleScreen())
            {
                foreach (var i in Items)
                {
                    if (((IChildScreen)item).ScreenId == ((IChildScreen)i).ScreenId)
                        return i;
                }
            }

            return item;
        }
        #endregion
      
    }
}