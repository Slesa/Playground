using System;
using System.Collections.Generic;

namespace BugTracker.Model
{
    public interface IBugRepository : IEnumerable<Bug>
    {
        event EventHandler<BugEvent> BugSaved;
        event EventHandler<BugEvent> BugDeleted;

        void Save(Bug bug);
        void Delete(Bug bug);
    }
}