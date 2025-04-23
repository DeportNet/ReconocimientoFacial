using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public Venta(){}

        public Venta(int itemId, int? branchMemberId, char isSaleItem, int? period, int? days)
        {
            ItemId = itemId;
            BranchMemberId = branchMemberId;
            IsSaleItem = isSaleItem;
            Period = period;
            Days = days;
        }

        public Venta(int itemId ,int? branchMemberId, char isSaleItem)
        {
            ItemId = itemId;
            BranchMemberId = branchMemberId;
            IsSaleItem = isSaleItem;
        }
    }
}
