using StockApp.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockApp.Domain.Entities
{
    public class Product
    {
        #region Atributos
        public int Id { get; set; }

        [Required(ErrorMessage ="Invalid name, name is required")]
        [MinLength(3, ErrorMessage = "Invalid name, too short, minimal 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Invalid description, description is required")]
        [MinLength(5 , ErrorMessage = "Invalid description, too short, minimum 5 characters.")]
        public string Description { get; set; }

        [Required (ErrorMessage = "Invalid Price, Price is required")]
        [Range(0 , double.MaxValue, ErrorMessage = "Invalid price, price negative value")]
        public decimal Price { get; set; }

        [Required (ErrorMessage = "Invalid stock, stock is required")]
        [Range(0 , int.MaxValue , ErrorMessage = "Invalid stock, stock negative value")]
        public int Stock { get; set;}

        [MaxLength(250 , ErrorMessage = "Image invalid, too long, Image max 250 characters")]
        public string Image { get; set; }
        public int CategoryId { get; set; }
        #endregion

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Update Invalid Id value");
            Id= id;
            ValidateDomain(name, description, price, stock, image);
        }



        public Category Category { get; set; }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name, name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid description, name is required.");

            DomainExceptionValidation.When(description.Length < 5,
                "Invalid description, too short, minimum 5 characters.");

            DomainExceptionValidation.When(price < 0, "Invalid price negative value.");

            DomainExceptionValidation.When(stock < 0, "Invalid stock negative value.");

            DomainExceptionValidation.When(image.Length > 250, "Invalid image name, too long, maximum 250 characters.");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
    }
}