namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            //商業邏輯, 複雜邏輯
            if (this.Price > 1000 && this.Stock > 100) {
                yield return new ValidationResult(
                    "本商品價格與庫存量不合理",new string[] {"Price", "Stock"});
            }

            if (this.ProductName.Contains("Vicky")) {
                yield return new ValidationResult(
                    "本名字不可重複", new string[] { "ProductName" });
            }
            yield break;
            //throw new NotImplementedException();
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        [Range(0,9999)]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool Is刪除 { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
