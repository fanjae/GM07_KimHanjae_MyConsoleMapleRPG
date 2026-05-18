namespace MyConsoleMapleRPG.Map
{
    // 맵 이동 정보를 담는 데이터 클래스
    // 현재 맵의 특정 좌표에서 TargetMap의 TargetX,Y로 이동할 때 사용한다.
    internal class MapPortal
    {
        public MapData TargetMap { get; }
        public int TargetX { get; }
        public int TargetY { get; }

        public MapPortal(MapData targetMap, int targetX, int targetY)
        {
            TargetMap = targetMap;
            TargetX = targetX;
            TargetY = targetY;
        }
    }
}
