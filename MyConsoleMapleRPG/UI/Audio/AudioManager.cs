using System.Media;
namespace MyConsoleMapleRPG.UI.Audio
{
    internal static class AudioManager // 오디오 매니저
    {
        private static SoundPlayer? currentBgm;
        private static string? currentPath;

        public static void PlayBgm(string path)
        {
            if (currentPath == path)
                return;

            StopBgm(); // 기존 재생 중인 Bgm은 끔

            currentBgm = new SoundPlayer(path);
            currentBgm.PlayLooping();
            currentPath = path;
        }

        public static void StopBgm()
        {
            currentBgm?.Stop();
            currentBgm = null;
            currentPath = null;
        }

        public static void PlaySfx(string path)
        {
            SoundPlayer sfx = new SoundPlayer(path);
            sfx.Play();
        }
    }
}
