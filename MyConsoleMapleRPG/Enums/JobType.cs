namespace MyConsoleMapleRPG.Enums
{
    [Flags]
    internal enum JobType
    {
        None = 0,
        Warrior = 1,
        Mage = 2,
        All = Warrior | Mage
    }
}
