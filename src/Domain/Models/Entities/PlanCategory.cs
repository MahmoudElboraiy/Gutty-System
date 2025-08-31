using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class PlanCategory 
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PlanId))]
        public Guid PlanId { get; set; }
        public Plan Plan { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public uint NumberOfMeals { get; set; }
        public uint ProteinGrams { get; set; }
        public decimal PricePerGram { get; set; }
        public bool AllowProteinChange { get; set; }
        public uint MaxProteinGrams { get; set; }



        public decimal GetCategoryPrice()
        {
            return NumberOfMeals * ProteinGrams * PricePerGram;
        }
    }
}
