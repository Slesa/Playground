using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model;
using MediaOwl.Model.LastFm;
using MediaOwl.Resources;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicTagHomeViewModel))]
    [Export(typeof(IChildScreen<MusicViewModel>))]
    public class MusicTagHomeViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields

        private readonly ILastFmService service;
        private readonly LastFmRepository repository;

        #endregion

        #region Constructor

        [ImportingConstructor]
        public MusicTagHomeViewModel(ILastFmService service, LastFmRepository repository)
        {
            DisplayName = AppStrings.MusicTagHomeTitle;
            this.service = service;
            this.repository = repository;
        }

        #endregion

        #region Properties & Backingfields

        public BindableCollection<Tag> TopTags
        {
            get { return repository.TopTags; }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> OpenTag(object selectedItem)
        {
            var tag = selectedItem as Tag;
            if (tag != null)
                yield return Show.Child<MusicTagSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithTag(tag));
        }

        public IEnumerator<IResult> FetchTagData()
        {
            if (IsActive && TopTags.Count == 0)
            {
                yield return Show.Busy(IoC.Get<MusicViewModel>());
                yield return service.TopTags();
                NotifyOfPropertyChange(() => TopTags);
                yield return Show.NotBusy(IoC.Get<MusicViewModel>());
            }
        }

        #endregion

        #region Implementation of IChildScreen

        public string ScreenId
        {
            get { return GetType().Name; }
        }
        public int? Order { get { return 3; } }

        #endregion
    }
}