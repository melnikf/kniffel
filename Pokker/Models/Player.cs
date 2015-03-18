using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    [Table("players")]
    public class Player
    {
        [Column("player_id")]
        public uint PlayerId { get; set; }
        [Column("reg_date")]
        public string RegDate { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("pass")]
        public string Pass { get; set; }
        [Column("chips")]
        public uint Chips { get; set; }
    }
}