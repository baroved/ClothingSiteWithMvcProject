using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        [Required]
        public int OwnerID { get; set; }
        public User User { get; set; }
        public int? UserID { get; set; }
        [Required(ErrorMessage = "Title cannot be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Short description cannot be empty")]
        [StringLength(500)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Long description cannot be empty")]
        [StringLength(4000)]
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "Date cannot be empty")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Price cannot be empty")]
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        public byte[] PictureOne { get; set; }
        public byte[] PictureTwo { get; set; }
        public byte[] PictureThree { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public DateTime AddedToCart { get; set; }
    }
    public enum State
    {
        Sold,
        Aviable,
        InCart
    }
}
