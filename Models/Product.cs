using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    [Table("table_products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        //[Column("id")]
        public required string BookTitle { get; set; }
        public required string Author { get; set; }
        public required string PublisherDate { get; set; }
        public required int CountPages { get; set; }
        public required int Price { get; set; }

        [Column("publisher_house_id")]
        public Guid PublisherHouseID { get; set; }
        public PublisherHouse publisherHouse { get; set; }
    }
}
