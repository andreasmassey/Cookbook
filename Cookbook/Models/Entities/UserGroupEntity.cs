using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class UserGroupEntity
    {
        public int UserGroupID { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
