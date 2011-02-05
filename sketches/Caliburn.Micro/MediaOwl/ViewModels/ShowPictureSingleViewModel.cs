using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using MediaOwl.Core;

namespace MediaOwl.ViewModels
{
    [Export(typeof(ShowPictureSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShowPictureSingleViewModel : Screen, IChildScreen
    {

        #region Properties & Backingfields

        private BitmapImage picture;
        public BitmapImage Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                NotifyOfPropertyChange(() => Picture);
            }
        }

        #endregion

        #region Methods

        public void WithPicture(BitmapImage bitmapImage, string title)
        {
            Picture = bitmapImage;
            DisplayName = "Picture of " + title;
        }

        #endregion

        #region Implementation of IChildScreen

        public string ScreenId
        {
            get { return DisplayName; }
        }

        #endregion
    }
}