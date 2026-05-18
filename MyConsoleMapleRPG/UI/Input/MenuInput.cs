internal static class MenuInput
{
    // MenuInput
    // - 메뉴 선택 인덱스 이동을 공통 처리하는 입력 유틸리티
    // - 좌우 방향키 입력에 따라 선택 인덱스를 순환시킨다.

    public static int MoveHorizontal(int selectedIndex, int itemCount, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.LeftArrow: // 왼쪽 이동시 selectIndex 감소 처리
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = itemCount - 1;
                break;

            case ConsoleKey.RightArrow: // 오른쪽 이동시 SelectIndex 증가 처리
                selectedIndex++;
                if (selectedIndex >= itemCount)
                    selectedIndex = 0;
                break;
        }

        return selectedIndex;
    }
}