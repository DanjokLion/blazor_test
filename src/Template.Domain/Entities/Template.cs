using Template.Domain.Declare;

namespace Template.Domain.Entities
{
    public class TemplateEntity
    {
        public int Id { get; set; }
        public string Uuid { get; set; }

        public Status Status { get; set; }

    }
}