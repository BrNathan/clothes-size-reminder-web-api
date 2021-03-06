﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("user")]
    public class User : IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(320, ErrorMessage = "Email can't be longer than 320 characters")]
        public string Email { get; set; }

        [Column("password")]
        [MaxLength(255, ErrorMessage = "Password can't be longer than 255 characters")]
        public string Password { get; set; }

        [Column("first_name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Column("gender_id")]
        public int? GenderId { get; set; }

        //public User(User user)
        //{
        //    this.Id = user.Id;
        //    this.Email = user.Email;
        //    this.Password = user.Password;
        //    this.FirstName = user.FirstName;
        //    this.LastName = user.LastName;
        //    this.GenderId = user.GenderId;
        //}
    }
}
