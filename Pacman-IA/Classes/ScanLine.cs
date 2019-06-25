
namespace Pacman_IA.Classes
{
    public class ScanLine
    {
        public bool ghostFound;
        public bool ghostCollisionCourse;

        public bool pacmanFound;

        public bool hasFood;
        public int totalFood;
        public int foodDistance;

        public int xSize;
        public int ySize;

        public ScanLine()
        {
            ghostFound = false;
            ghostCollisionCourse = false;

            hasFood = false;
            totalFood = 0;
            foodDistance = 0;

            xSize = GameMap.wallLevel.GetLength(1);
            ySize = GameMap.wallLevel.GetLength(0);
        }
    }
}
