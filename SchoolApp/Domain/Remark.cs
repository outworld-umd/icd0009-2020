using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {

    public class Remark {
        public int RemarkId { get; set; }
        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }
        public Person Sender { get; set; }
        [ForeignKey(nameof(Recipient))]
        public int RecipientId { get; set; }
        public Person Recipient { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(200)]
        public string Text { get; set; }
        public int RemarkTypeId { get; set; }
        public RemarkType RemarkType { get; set; }
    }

}