--Reads and parses the input file
function  ReadInput(filepath)
  local file = io.open(filepath,"r")
  local rows = 1
  local cols = 0
  local antennae = {}
  
  if not file then
    return
  end

  for line in file:lines() do 
    cols = #line
    for i=1, #line do 
      local ch = line:sub(i,i)
      if ch ~= '.' then
        if not antennae[ch] then
          antennae[ch] = {}
        end
      table.insert(antennae[ch],{rows,i}) -- dictionary
      end
    end
    rows = rows + 1
  end
  file:close()
  return antennae,rows-1,cols --antennae positions and grid parameters
end

-- Check if the position (r,c) is in bounds of the grid
function InBounds(r,c,rows,cols)
  return r >= 1 and r<= rows and c >= 1 and c <= cols
end

--Checks for 1 antinodes in each direction for part 1 
function CheckForAntinodes(antennae,antinodes,key,rows,cols)
  local pos = antennae[key]
  for i=1, #pos do
    for j=i+1,#pos do
      local dr = pos[i][1] - pos[j][1] --row difference of the 2 antennas
      local dc = pos[i][2] - pos[j][2] --col difference of the 2 antennas

      if InBounds(pos[i][1] + dr,pos[i][2] + dc,rows,cols) then
        local anKey = tostring(pos[i][1]+dr)..","..tostring(pos[i][2]+dc)
        antinodes[anKey] = true
      end

      if InBounds(pos[j][1] - dr,pos[j][2] - dc,rows,cols) then
        local anKey = tostring(pos[j][1]-dr)..","..tostring(pos[j][2]-dc)
        antinodes[anKey] = true
      end

    end
  end
end

-- checks for antinodes untill they are out of bounds
local function CheckForAntinodesContinious(antennae,antinodes,key,rows,cols)
   local pos = antennae[key]
  for i=1, #pos do
    local anKey = tostring(pos[i][1])..","..tostring(pos[i][2])
    antinodes[anKey] =true      --add initial node
    for j=i+1,#pos do
      local anKey = tostring(pos[j][1])..","..tostring(pos[j][2])
      antinodes[anKey] =true    --add initial node
      
      local dr = pos[i][1] - pos[j][1] --row difference of the 2 antennas
      local dc = pos[i][2] - pos[j][2] --col difference of the 2 antennas

      --the antennae position in the first direction
      local row = pos[i][1] + dr  
      local col = pos[i][2] + dc  
      
      while InBounds(row,col,rows,cols) do
        local anKey = tostring(row)..","..tostring(col)
        antinodes[anKey] = true
        row = row + dr
        col = col + dc
      end
      
      --the antennae position in the second direction
      local row = pos[j][1] - dr
      local col = pos[j][2] - dc
      while InBounds(row,col,rows,cols) do
        local anKey = tostring(row)..","..tostring(col)
        antinodes[anKey] = true
        row = row - dr
        col = col - dc
      end
      
    end
  end
end

local function countKeys(table)
  local count = 0
  for _ in pairs(table) do
    count = count+1
  end
  return count
end



local function p1()
  local antennae, rows, cols = ReadInput("bin\\Debug\\net9.0\\day08-input.txt")
  local antinodes = {}
  if antennae then
    for key,_ in pairs(antennae) do
      CheckForAntinodes(antennae,antinodes,key,rows,cols)
    end
  end
  print("Part 1:" ..countKeys(antinodes))
end



local function p2()
  local antennae, rows, cols = ReadInput("bin\\Debug\\net9.0\\day08-input.txt")
  local antinodes = {}
  if antennae then
    for key,_ in pairs(antennae) do
      CheckForAntinodesContinious(antennae,antinodes,key,rows,cols)
    end
  end
  print("Part 1:"..countKeys(antinodes))
end


p1()
p2()