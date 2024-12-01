namespace CalculatorApp.Models
{
    public class CalculatorModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public required String Operation { get; set; }
        public double Result { get; set; }
    }
}