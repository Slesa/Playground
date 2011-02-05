using System;
using System.IO;
using Castle.Windsor;
using Godot.Infrastructure;
using Godot.Model;
using Godot.PmsMatrix.Persistence;
using Godot.PmsModel.Entities;
using Godot.PmsModel.Queries;

namespace Godot.PmsMatrix
{
    public class ImportMatrixFilesOnStartup : IPrepareStartup
    {
        readonly IWindsorContainer _container;
        readonly IDbConversation _dbConversation;

        public ImportMatrixFilesOnStartup(IWindsorContainer container, IDbConversation dbConversation)
        {
            _container = container;
            _dbConversation = dbConversation;
        }

        public void Prepare()
        {
            PrepareSalesItems();
            PrepareCostcenters();
        }

        void PrepareCostcenters()
        {
            _dbConversation.UsingTransaction(()=>
                {
                    IMatrixFileLoader<Costcenter> costcentersLoader;
                    if (!UpdatedNeededForTable(out costcentersLoader)) /*return*/;

                    _dbConversation.Query(new DropCostcentersQuery());

                    var costcenters = costcentersLoader.Elements;
                    foreach (var kvp in costcenters)
                    {
                        var item = new Costcenter();
                        _dbConversation.InsertObjectOnCommit(item.SetInternalId(kvp.Key));
                    }
                });
        }

        void PrepareSalesItems()
        {
            _dbConversation.UsingTransaction(() =>
                {
                    IMatrixFileLoader<SalesItem> salesItemLoader;
                    if (!UpdatedNeededForTable(out salesItemLoader)) /*return*/;

                    _dbConversation.Query(new DropSalesItemsQuery());

                    var salesItems = salesItemLoader.Elements;
                    foreach (var kvp in salesItems)
                    {
                        var item = new SalesItem(); 
                        _dbConversation.InsertObjectOnCommit(item.SetInternalId(kvp.Key));
                    }
                });
        }

        bool UpdatedNeededForTable<TLoadingType>(out IMatrixFileLoader<TLoadingType> fileLoader) where TLoadingType : DomainEntity
        {
            fileLoader = _container.Resolve<IMatrixFileLoader<TLoadingType>>();
            var fileName = fileLoader.FullFileName;
            if (!File.Exists(fileName))
                return false;
            var fileModification = _dbConversation.Query(new FileModificationQuery(fileName));
            var fileInfo = new FileInfo(fileName);
            if (fileModification != null)
            {
                if (fileInfo.LastWriteTime - fileModification.LastModified  < TimeSpan.FromSeconds(1.0))
                    return false;
            }
            else
            {
                fileModification = new FileModificationDate {FileName = fileName };
            }
            fileModification.LastModified = fileInfo.LastWriteTime;
            _dbConversation.InsertObjectOnCommit(fileModification);
            return true;
        }
    }
}