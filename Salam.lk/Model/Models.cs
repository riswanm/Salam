using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Salam.lk.Model
{
    public partial class Entity
    {
        public List<EntityData> EntityData { get; set; }
    
        

    }

    public partial class EntityData
    {
        public string FieldName { get; set; }
    }

    
}