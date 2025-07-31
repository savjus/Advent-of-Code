//LEARN THEN DELETE I DIDNT MAKE THIS



import * as fs from 'fs';

interface ClawMachine {
    buttonA: { x: number; y: number };
    buttonB: { x: number; y: number };
    prize: { x: number; y: number };
}

function parseInput(input: string): ClawMachine[] {
    const machines: ClawMachine[] = [];
    const lines = input.trim().split('\n');
    
    for (let i = 0; i < lines.length; i += 4) {
        const buttonAMatch = lines[i].match(/Button A: X\+(\d+), Y\+(\d+)/);
        const buttonBMatch = lines[i + 1].match(/Button B: X\+(\d+), Y\+(\d+)/);
        const prizeMatch = lines[i + 2].match(/Prize: X=(\d+), Y=(\d+)/);
        
        if (buttonAMatch && buttonBMatch && prizeMatch) {
            machines.push({
                buttonA: { x: parseInt(buttonAMatch[1]), y: parseInt(buttonAMatch[2]) },
                buttonB: { x: parseInt(buttonBMatch[1]), y: parseInt(buttonBMatch[2]) },
                prize: { x: parseInt(prizeMatch[1]), y: parseInt(prizeMatch[2]) }
            });
        }
    }
    
    return machines;
}

function solveMachine(machine: ClawMachine, prizeOffset: number = 0): number | null {
    const { buttonA, buttonB, prize } = machine;
    const targetX = prize.x + prizeOffset;
    const targetY = prize.y + prizeOffset;
    
    // Using Cramer's rule to solve the system of linear equations:
    // a * buttonA.x + b * buttonB.x = targetX
    // a * buttonA.y + b * buttonB.y = targetY
    
    const determinant = buttonA.x * buttonB.y - buttonA.y * buttonB.x;
    
    if (determinant === 0) {
        return null; // No unique solution
    }
    
    const a = (targetX * buttonB.y - targetY * buttonB.x) / determinant;
    const b = (buttonA.x * targetY - buttonA.y * targetX) / determinant;
    
    // Check if solutions are non-negative integers
    if (a >= 0 && b >= 0 && Number.isInteger(a) && Number.isInteger(b)) {
        return 3 * a + b; // Cost: 3 tokens for A, 1 token for B
    }
    
    return null;
}

function part1(machines: ClawMachine[]): number {
    let totalTokens = 0;
    
    for (const machine of machines) {
        const cost = solveMachine(machine);
        if (cost !== null) {
            totalTokens += cost;
        }
    }
    
    return totalTokens;
}

function part2(machines: ClawMachine[]): number {
    let totalTokens = 0;
    const prizeOffset = 10000000000000; // 10^13
    
    for (const machine of machines) {
        const cost = solveMachine(machine, prizeOffset);
        if (cost !== null) {
            totalTokens += cost;
        }
    }
    
    return totalTokens;
}

function main() {
    try {
        // Try to read from input file, otherwise use example
        let input: string;
             input = fs.readFileSync('./bin/Debug/net9.0/day13-input.txt', 'utf8');
        
        const machines = parseInput(input);
        
        const result1 = part1(machines);
        console.log('Part 1:', result1);
        
        const result2 = part2(machines);
        console.log('Part 2:', result2);
        
    } catch (error) {
        console.error('Error:', error);
    }
}

// Run if this file is executed directly
if (require.main === module) {
    main();
}

export { parseInput, solveMachine, part1, part2 };