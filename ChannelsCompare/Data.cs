namespace ConsoleApp1
{
    struct Data
    {
        public string Name;
        int MonthlySalary;
        int PercentageBonus;

        public Data(string name, int monthlySalary, int percentageBonus)
        {
            Name = name;
            MonthlySalary = monthlySalary;
            PercentageBonus = percentageBonus;
        }
    }
}
