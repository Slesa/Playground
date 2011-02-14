using System;
using System.Xml.Linq;

namespace MediaOwl.Model.LastFm
{
    public abstract class EntityBase
    {
        protected EntityBase(XElement xelement)
        {
            
        }

        protected EntityBase() { }

        public virtual void FromXml(XElement xelement)
        {
            
        }
    }
}