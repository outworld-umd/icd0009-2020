using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain {

    public class Remark : DomainEntity {
        [ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }
        public Person Sender { get; set; }
        [ForeignKey(nameof(Recipient))]
        public Guid RecipientId { get; set; }
        public Person Recipient { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(200)]
        public string Text { get; set; }
        public Guid RemarkTypeId { get; set; }
        public RemarkType RemarkType { get; set; }
    }

}