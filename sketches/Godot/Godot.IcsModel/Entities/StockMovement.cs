using System;
using System.Collections.Generic;
using Godot.Model;

namespace Godot.IcsModel.Entities
{
    public class StockMovement : DomainEntity
    {
        readonly IList<StockMoveItem> _moveItems = new List<StockMoveItem>();

        public virtual Stock OfStock { get; set; }
        public virtual DateTime ExecutedAt { get; set; }

        public virtual IEnumerable<StockMoveItem> MoveItems
        {
            get { return _moveItems; }
        }

        public virtual void AddMoveItem(StockMoveItem moveItem)
        {
            moveItem.StockMovement = this;
            _moveItems.Add(moveItem);
        }

        public virtual void RemoveMoveItem(StockMoveItem moveItem)
        {
            _moveItems.Remove(moveItem);
        }
    }
}