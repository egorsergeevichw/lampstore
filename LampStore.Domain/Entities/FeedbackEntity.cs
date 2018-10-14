using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LampStore.Domain.Entities
{
    [Table("Feedbacks")]
    public class FeedbackEntity
    {
        [Key, ForeignKey("User")]
        public Guid FeedbackId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public UserEntity User { get; set; }
    }
}
