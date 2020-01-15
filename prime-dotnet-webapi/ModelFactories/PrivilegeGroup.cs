using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    public class PrivilegeGroup : IDefinable
    {
        [Key]
        public int Id ,

        public string Name ,

        public ICollection<Privilege> Privileges ,

    }
}
