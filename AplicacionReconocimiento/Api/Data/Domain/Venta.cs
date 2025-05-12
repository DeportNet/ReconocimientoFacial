using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DeportNetReconocimiento.Api.Data.Domain
{
    [Table("ventas")]
    public class Venta
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("branch_member_id")]
        public int? BranchMemberId {  get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        [Column("is_sale_item")]
        [DisallowNull]
        public char IsSaleItem { get; set;}

        [Column("period")]
        [AllowNull]
        public int? Period { get; set; }
        
        [Column("days")]
        [AllowNull]
        public int? Days { get; set; }


        [Column("date")]
        [DisallowNull]
        public DateTime Date { get; set; }

        [Column("synchronized")]
        [DisallowNull]
        public string Synchronized { get; set; }

        [Column("syncronized_date")]
        [AllowNull]
        public DateTime? syncronized_date{ get; set; }

        public Venta(){}

        public Venta(int itemId, int? branchMemberId, char isSaleItem, int? period, int? days)
        {
            ItemId = itemId;
            BranchMemberId = branchMemberId;
            IsSaleItem = isSaleItem;
            Period = period;
            Days = days;
            Date = DateTime.Now;
            Synchronized = "F";
        }

        public Venta(int itemId ,int? branchMemberId, char isSaleItem)
        {
            ItemId = itemId;
            BranchMemberId = branchMemberId;
            IsSaleItem = isSaleItem;
            Date = DateTime.Now;
            Synchronized = "F";
        }
    }
}
