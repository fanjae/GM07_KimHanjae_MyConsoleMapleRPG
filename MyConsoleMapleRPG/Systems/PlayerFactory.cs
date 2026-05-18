using MyConsoleMapleRPG.Character;
using MyConsoleMapleRPG.Enums;

namespace MyConsoleMapleRPG.Systems
{
    // 직업 선택 결과를 실제 Player 객체로 바꿔주는 생성 전용 클래스
    // 화면/컨트롤러가 구체적인 클래스를 알지 않게 하기 위한 Factory
    internal static class PlayerFactory
    {
        private static readonly Func<Player>[] factories =
        {
            () => new Warrior(),
            () => new Mage()
        };

        public static Player Create(int jobIndex)
        {
            if (jobIndex < 0 || jobIndex >= factories.Length) throw new ArgumentOutOfRangeException(nameof(jobIndex), "존재하지 않는 직업 인덱스입니다.");

            return factories[jobIndex]();
        }
        public static Player Create(JobType jobType)
        {
            return jobType switch
            {
                JobType.Warrior => new Warrior(),
                JobType.Mage => new Mage(),
                _ => throw new ArgumentException($"지원하지 않는 직업입니다: {jobType}")
            };
        }
    }
}