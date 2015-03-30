using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    [Table("players")]
    public class Player
    {
        [Key]
        [Column("player_id")]
        public int PlayerId { get; set; }
        [Column("reg_date")]
        public string RegDate { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("pass")]
        public string Pass { get; set; }
        [Column("chips")]
        public int Cash { get; set; }
    }
}