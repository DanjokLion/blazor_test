namespace Template.UI.Data
{
    /// <summary>
    /// Модель для рендеринга таблицы с автомобилями
    /// </summary>
    public struct AutoDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Colour { get; set; }
        public string Company { get; set; }
        public string Desctiption { get; set; }
    }
}
