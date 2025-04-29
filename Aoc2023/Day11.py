import re
import itertools

# Get the added space value
AddSpace = int(input("Distance per empty row/col: "))

# Read and clean the galaxy map
with open("Aoc2023_11_input.txt", "r") as file:
    galaxy = [line.strip() for line in file.readlines()]

verticals, horizontals, stars = [], [], []

# Find empty vertical columns
for i in range(len(galaxy[0])):
    if all(row[i] == "." for row in galaxy):
        verticals.append(i)

# Find empty horizontal rows and star positions
for j, line in enumerate(galaxy):
    if all(char == "." for char in line):
        horizontals.append(j)
    else:
        stars += [[match.start(), j] for match in re.finditer("#", line)]

total = 0

# Calculate pairwise distances
for a, b in itertools.combinations(stars, 2):
    x = sorted((a[0], b[0]))
    y = sorted((a[1], b[1]))
    total += (x[1] - x[0]) + (y[1] - y[0])

    # Check for crossed empty columns
    total += sum(AddSpace for ver in verticals if x[0] < ver < x[1])
    total += sum(AddSpace for hor in horizontals if y[0] < hor < y[1])

print(total)
