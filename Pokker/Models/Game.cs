using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokker.Models
{
    [Table("games")]
    public class Game
    {
        [Key]
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("player_id")]
        public int PlayerId { get; set; }
        [Column("chips")]
        public int Chips { get; set; }
        [Column("result")]
        public byte Result { get; set; }
    }
}