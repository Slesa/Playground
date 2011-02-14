using System;
using Godot.Model;

namespace Godot.PmsModel.Entities
{
    public class FileModificationDate : DomainEntity
    {
        public virtual string FileName { get; set; }
        public virtual DateTime LastModified { get; set; }
    }
}