using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class RefreshTokenInfo
    {
        public int Id { get; set; }
        public string? Token { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users? Users { get; set; }
        
    }
}
