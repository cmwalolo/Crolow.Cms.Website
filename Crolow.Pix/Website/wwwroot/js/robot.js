class Solution {
    uniquePathsWithObstacles(obstacleGrid) {
        this.h = obstacleGrid.length;
        this.w = obstacleGrid[0].length;
        this.grid = obstacleGrid;
        this.pathes = 0;

        this.findPath(0, 0);
        return this.pathes;
    }

    findPath(posx, posy) {
        if (posx >= this.w || posy >= this.h) {
            return false;
        }

        if (this.grid[posx][posy] == 1) {
            return false;
        }

        if (posx == this.w - 1 && posy == this.h - 1) {
            this.pathes++;
            return false;
        }

        var res1 = this.findPath(posx + 1, posy);
        var res2 = this.findPath(posx, posy + 1);

        /* if (res1 || res2) {
             return true;
         }
 
         return false;*/
    }
}