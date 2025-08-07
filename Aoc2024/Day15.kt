// kotlinc Day15.kt -include-runtime -d Day15.jar
// java -jar Day15.jar

import java.io.File

data class Position(val x: Int, val y: Int) {
    operator fun plus(other: Position) = Position(x + other.x, y + other.y)
}

data class DoubleNode(val left: Position, val right: Position) {
    fun contains(pos: Position): Boolean {
        return pos.x >= left.x && pos.x <= right.x && pos.y >= left.y && pos.y <= right.y
    }
}

class Day15(private val input: String) {
    private val boxes = mutableListOf<Position>()
    private val boxesBig = mutableListOf<DoubleNode>()
    private val walls = mutableListOf<Position>()
    private val wallsBig = mutableListOf<DoubleNode>()
    private var robot = Position(0, 0)
    private val moves = mutableListOf<Char>()
    
    private val directions = mapOf(
        '<' to Position(-1, 0),
        '>' to Position(1, 0),
        'v' to Position(0, 1),
        '^' to Position(0, -1)
    )
    
    fun part1(): Long {
        parseInputPart1(input)
        
        for (move in moves) {
            val moveDir = directions[move]!!
            val robotMoved = robot + moveDir
            
            if (walls.contains(robotMoved)) {
                continue
            }
            
            if (boxes.contains(robotMoved)) {
                var nextMove = robotMoved + moveDir
                while (boxes.contains(nextMove)) {
                    nextMove = nextMove + moveDir
                }
                if (walls.contains(nextMove)) {
                    continue
                }
                boxes.remove(robotMoved)
                boxes.add(nextMove)
            }
            robot = robotMoved
        }
        
        return countEndScore(boxes)
    }
    
    fun part2(): Long {
        parseInputPart2(input)
        
        for (move in moves) {
            val moveDir = directions[move]!!
            val robotMoved = robot + moveDir
            
            if (wallsBig.any { it.contains(robotMoved) }) {
                continue
            }
            
            val nextBox = boxesBig.firstOrNull { it.contains(robotMoved) }
            var canMove = true
            
            if (nextBox != null) {
                val boxesToMove = mutableSetOf(nextBox)
                val queue = ArrayDeque<DoubleNode>()
                queue.add(nextBox)
                
                while (queue.isNotEmpty() && canMove) {
                    val box = queue.removeFirst()
                    val leftbox = box.left + moveDir
                    val rightbox = box.right + moveDir
                    
                    if (wallsBig.any { it.contains(leftbox) } || wallsBig.any { it.contains(rightbox) }) {
                        canMove = false
                        break
                    }
                    
                    val nextLeftBox = boxesBig.firstOrNull { it.contains(leftbox) && !boxesToMove.contains(it) }
                    if (nextLeftBox != null && boxesToMove.add(nextLeftBox)) {
                        queue.add(nextLeftBox)
                    }
                    
                    val nextRightBox = boxesBig.firstOrNull { it.contains(rightbox) && !boxesToMove.contains(it) }
                    if (nextRightBox != null && boxesToMove.add(nextRightBox)) {
                        queue.add(nextRightBox)
                    }
                }
                
                if (canMove) {
                    for (box in boxesToMove) {
                        boxesBig.remove(box)
                        boxesBig.add(DoubleNode(box.left + moveDir, box.right + moveDir))
                    }
                    robot = robotMoved
                }
            } else {
                robot = robotMoved
            }
        }
        
        return countEndScore(boxesBig.map { it.left })
    }
    
    private fun countEndScore(boxes: List<Position>): Long {
        return boxes.sumOf { (it.y * 100 + it.x).toLong() }
    }
    
    private fun parseInputPart1(input: String) {
        boxes.clear()
        walls.clear()
        moves.clear()
        
        val lines = input.split('\n')
        var i = 0
        
        while (i < lines.size && lines[i].isNotEmpty()) {
            for (j in lines[i].indices) {
                when (lines[i][j]) {
                    '#' -> walls.add(Position(j, i))
                    'O' -> boxes.add(Position(j, i))
                    '@' -> robot = Position(j, i)
                }
            }
            i++
        }
        
        i++ // Skip empty line
        while (i < lines.size) {
            moves.addAll(lines[i].toCharArray().toList())
            i++
        }
    }
    
    private fun parseInputPart2(input: String) {
        boxesBig.clear()
        wallsBig.clear()
        moves.clear()
        
        val lines = input.split('\n')
        var i = 0
        
        while (i < lines.size && lines[i].isNotEmpty()) {
            for (j in lines[i].indices) {
                when (lines[i][j]) {
                    '#' -> {
                        val wall = DoubleNode(Position(j * 2, i), Position(j * 2 + 1, i))
                        wallsBig.add(wall)
                    }
                    'O' -> {
                        val box = DoubleNode(Position(j * 2, i), Position(j * 2 + 1, i))
                        boxesBig.add(box)
                    }
                    '@' -> robot = Position(j * 2, i)
                }
            }
            i++
        }
        
        i++ // Skip empty line
        while (i < lines.size) {
            moves.addAll(lines[i].toCharArray().toList())
            i++
        }
    }
}

fun main() {
        val input = File("bin\\Debug\\net9.0\\day15-input.txt").readText()
        val day15 = Day15(input)
        
        println("Part 1: ${day15.part1()}")
        println("Part 2: ${day15.part2()}")
      }