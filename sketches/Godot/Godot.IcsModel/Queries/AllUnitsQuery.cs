using System;
using System.Collections.Generic;
using System.Linq;

using Godot.IcsModel.Entities;
using Godot.Model;

using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class RecipeByRecipeNumber: IDomainQuery<Recipe>
    {
        readonly int _number;

        public RecipeByRecipeNumber(int number)
        {
            _number = number;
        }

        public Recipe Execute(ISession session)
        {
            return session.Load<Recipe>(_number);
        }
    }
    
    public class AllUnitsQuery : IDomainQuery<IEnumerable<Unit>>
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>();
        }
    }

    public interface ILoadUnitsForRecipes : IDomainQuery<IEnumerable<Unit>>
    {
        ILoadUnitsForRecipes MatchPrefix(string prefix);
    }

    public class UnitsForRecipes : ILoadUnitsForRecipes
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>().Where(x => x.Reciping);
        }

        public ILoadUnitsForRecipes MatchPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }

    public class AlexVersionOfUnitsForRecipies : ILoadUnitsForRecipes
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>().Where(x => x.Reciping && x.Parent != null);
        }

        public ILoadUnitsForRecipes MatchPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }

    internal class UnitsStartingWith : ILoadUnitsForRecipes
    {
        string _prefix;

        public UnitsStartingWith(string prefix)
        {
            _prefix = prefix;
        }

        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>().Where(x => x.Name.StartsWith(_prefix));
        }

        public ILoadUnitsForRecipes MatchPrefix(string prefix)
        {
            throw new NotImplementedException();
        }
    }
    
    internal class UnitsStartingWithInterfaceOnly : ILoadUnitsForRecipes
    {
        string _prefix;

        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>().Where(x => x.Name.StartsWith(_prefix));
        }

        public ILoadUnitsForRecipes MatchPrefix(string prefix)
        {
            _prefix = prefix;
            return this;
        }
    }

    internal class Aufruf
    {
        public void MethodName()
        {
            IDbConversation db = null;
            IEnumerable<Unit> enumerable = db.Query(new AlexVersionOfUnitsForRecipies());
            enumerable = db.Query(new UnitsStartingWith("42"));

            var q = CreateQueryFor<ILoadUnitsForRecipes>();
            q.MatchPrefix("42");
            db.Query(CreateQueryFor<ILoadUnitsForRecipes>().MatchPrefix("42"));
        }

        static ILoadUnitsForRecipes CreateQueryFor<TQuery>()
        {
            // Resolve TQuery from the container!
            return new UnitsStartingWithInterfaceOnly();
        }
    }
}