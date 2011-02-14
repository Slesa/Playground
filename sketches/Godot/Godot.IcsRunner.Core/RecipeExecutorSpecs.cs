using System;
using System.Collections.Generic;
using System.Linq;
using Godot.IcsModel;
using Godot.IcsModel.Entities;
using Godot.Model;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsRunner.Core
{
    [Subject(typeof(RecipeExecutor))]
    public class When_executing_simple_recipe
    {
        Establish context = () =>
            {
                var unitConverter = MockRepository.GenerateMock<IUnitConverter>();

                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                dbConversation
                    .Stub(x => x.UsingTransaction(null))
                    .IgnoreArguments()
                    .WhenCalled(x =>
                        {
                            var action = (Action) x.Arguments.First();
                            action();
                        });

                var recipe = new Recipe();
                var purchaseItem = new PurchaseItem();
                recipe.AddRecipeItem(new RecipeItem{Quantity=1m, Recipe = recipe, RecipeableItem = purchaseItem});

                var recipeFinder = MockRepository.GenerateStub<IRecipeFinder>();
                recipeFinder.Stub(x => x.FindRecipes(null)).IgnoreArguments().Return(new List<Recipe> {recipe});

                _stock = new Stock();
                var stockFinder = MockRepository.GenerateStub<IStockFinder>();
                stockFinder.Stub(x => x.FindStockFor(null, null)).IgnoreArguments().Return(_stock);

                _recipeExecutor = new RecipeExecutor(dbConversation, recipeFinder, stockFinder, new StockBooker(unitConverter));

            };

        Because of = () => _recipeExecutor.Execute(new RecipeJob{Quantity = 1m});

        It should_book_item_off_the_stock = () => _stock.StockItems.ShouldNotBeEmpty();

        static Stock _stock;
        static RecipeExecutor _recipeExecutor;
    }


    [Subject(typeof(RecipeExecutor))]
    public class When_executing_recipe_with_unknown_stock
    {
        Establish context = () =>
        {
            var unitConverter = MockRepository.GenerateMock<IUnitConverter>();

            var dbConversation = MockRepository.GenerateStub<IDbConversation>();
            var recipeFinder = MockRepository.GenerateStub<IRecipeFinder>();
            var stockFinder = MockRepository.GenerateStub<IStockFinder>();
            _recipeExecutor = new RecipeExecutor(dbConversation, recipeFinder, stockFinder, new StockBooker(unitConverter));
        };

        Because of = () => _recipeExecutor.Execute(new RecipeJob());

        It should_append_error_pool = () => _recipeExecutor.ShouldBeNull();

        static RecipeExecutor _recipeExecutor;
    }

    [Subject(typeof(RecipeExecutor))]
    public class When_executing_unknown_recipe
    {
        Establish context = () =>
            {
                var unitConverter = MockRepository.GenerateMock<IUnitConverter>();

                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                var recipeFinder = MockRepository.GenerateStub<IRecipeFinder>();
                var stockFinder = MockRepository.GenerateStub<IStockFinder>();
                _recipeExecutor = new RecipeExecutor(dbConversation, recipeFinder, stockFinder, new StockBooker(unitConverter));
            };

        Because of = () => _recipeExecutor.Execute(new RecipeJob());

        It should_append_error_pool = () => _recipeExecutor.ShouldBeNull();

        static RecipeExecutor _recipeExecutor;
    }
}
