using Template.Domain.Declare;

namespace Template.Domain.Entities
{
    public class PushAddEntity
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public int KeyPart { get; set; }

        /// <summary>
        /// Токен приложения 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ID платформы   
        /// </summary>
        public Status PlatformId { get; set; }
    }
}