using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    [Table("games")]
    public class Game
    {
        [Column("game_id")]
        public uint GameId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("player_id")]
        public uint PlayerId { get; set; }
        [Column("chips")]
        public uint Chips { get; set; }
        [Column("result")]
        public byte Result { get; set; }
    }
}