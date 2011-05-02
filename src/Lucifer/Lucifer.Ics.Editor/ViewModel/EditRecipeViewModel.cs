using System.ComponentModel;
using System.Globalization;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class EditRecipeViewModel : EditItemViewModel<RecipeModel>, IDataErrorInfo
    {
        public EditRecipeViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator) 
            : base(dbConversation, eventAggregator)
        {
            DisplayName = Strings.EditRecipeView_NewRecipe;
            Title = Strings.EditRecipeView_TitleNew;
            ToolTip = Strings.AllRecipesView_New_ToolTip;
        }

        public EditRecipeViewModel(int id, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(id, dbConversation, eventAggregator)
        {
            DisplayName = string.Format(CultureInfo.CurrentCulture, Strings.EditRecipeView_RecipeIs, Element.Plu);
            ToolTip = Strings.AllRecipesView_Edit_ToolTip;
        }

        public string Title { get; private set; }

        public int Plu
        {
            get { return Element.Plu; }
            set
            {
                if (value == Element.Plu) return;
                Element.Plu = value;
                NotifyOfPropertyChange(() => Plu);
            }
        }

        public void Save()
        {
            if (!SuccessfullySaved(() => DbConversation.InsertOnCommit(Element.Recipe))) 
                return;

            EventAggregator.Publish(new RecipeChangedEvent(Element.Recipe));
            TryClose();
        }

 
        #region Module information

        public string ModuleName { get { return DisplayName; } }

        public static string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Recipe.png"; }
        }

        public string ToolTip { get; private set; }

        #endregion

        protected override RecipeModel CreateNewElementModel()
        {
            return new RecipeModel(new Recipe());
        }

        protected override RecipeModel CreateElementModel(int elementId)
        {
            RecipeModel model = null;
            DbConversation.UsingTransaction(() =>
                { model =new RecipeModel(DbConversation.GetById<Recipe>(elementId));
                });
            return model;
        }
    }
}