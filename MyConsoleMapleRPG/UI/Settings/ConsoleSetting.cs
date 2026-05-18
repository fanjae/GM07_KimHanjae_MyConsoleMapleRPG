using System.Runtime.InteropServices;

namespace MyConsoleMapleRPG.UI.Settings
{
    // 콘솔 출력 환경을 설정하는 유틸리티 클래스
    // 화면 크기, 버퍼 크기, 출력 인코딩, 커서 표시 여부 설정
    internal static class ConsoleSetting
    {
        // 콘솔 창 크기 조절 불가능 하게 만드는 방법
        // 참고 링크 : https://stackoverflow.com/questions/38426338/c-sharp-console-disable-resize


        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;//resize

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static void ApplyMain() // Wrapping Method
        {
            Apply(Layout.MainWidth, Layout.MainHeight);
        }

        public static void ApplyCharacterSelect() // Wrapping Method
        {
            Apply(Layout.CharacterSelectWidth, Layout.CharacterSelectHeight);
        }
        public static void Apply(int width, int height)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            if (width > Console.BufferWidth || height > Console.BufferHeight)
            {
                Console.SetBufferSize(Math.Max(width, Console.BufferWidth),Math.Max(height, Console.BufferHeight));
            }

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public static void ConsoleLock() // 콘솔 창을 임의로 조절하지 못하게 처리
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND); //resize
            }

        }
    }
}
