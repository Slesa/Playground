using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

namespace BugTracker.Model
{
    public class BugRepository : IBugRepository
    {
        private const string BugStore = @"C:\Bugs";
        private IList<Bug> _bugs;

        public BugRepository()
        {
            Load();
        }

        #region IBugRepository Members

        public IEnumerator<Bug> GetEnumerator()
        {
            return _bugs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event EventHandler<BugEvent> BugSaved;
        public event EventHandler<BugEvent> BugDeleted;

        public void Save(Bug bug)
        {
            if (bug.Id == 0)
            {
                bug.Id = _bugs.Any() ? _bugs.Max(x => x.Id) + 1 : 1;
                bug.CreatedOn = DateTime.Now;
                _bugs.Add(bug);
            }
            else
            {
                _bugs.Add(bug);
            }
            Persist(bug);

            if (BugSaved != null)
            {
                var timer = new Timer(2000);
                timer.Elapsed += (sender, args) => BugSaved(this, new BugEvent(bug));
                timer.Start();
            }
        }

        public void Delete(Bug bug)
        {
            if (_bugs.Remove(bug) && BugDeleted != null)
            {
                BugDeleted(this, new BugEvent(bug));
                File.Delete(GetFile(bug));
            }
        }

        #endregion

        private string GetFile(Bug bug)
        {
            return Path.Combine(BugStore, bug.Id + ".dat");
        }

        private void Persist(Bug bug)
        {
            EnsureBugStore();
            using (FileStream stream =
                File.Open(GetFile(bug), FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, bug);
            }
        }

        private void Load()
        {
            EnsureBugStore();
            _bugs = new List<Bug>();
            foreach (string bugFile in Directory.GetFiles(BugStore))
            {
                var formatter = new BinaryFormatter();
                using (FileStream stream =
                    File.Open(bugFile, FileMode.Open))
                {
                    try
                    {
                        var bug = (Bug)formatter.Deserialize(stream);
                        _bugs.Add(bug);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void EnsureBugStore()
        {
            if (!Directory.Exists(BugStore))
                Directory.CreateDirectory(BugStore);
        }
    }
}