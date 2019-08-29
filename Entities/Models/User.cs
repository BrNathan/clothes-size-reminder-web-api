using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("user")]
    public class User
    {

        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(320, ErrorMessage = "Email can't be longer than 320 characters")]
        public string Email { get; set; }

        [Column("password")]
        [MaxLength(255, ErrorMessage = "Password can't be longer than 255 charcters")]
        public string Password { get; set; }

        [Column("first_name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Column("gender_id")]
        public int? GenderId { get; set; }
    }
}
