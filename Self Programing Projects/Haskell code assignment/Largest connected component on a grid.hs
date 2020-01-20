--Tamir Sharon
-- 203999396



type Color = Int
type Index = (Int, Int)
type Cells = [(Index, Int)]

getCell :: Cells -> Int -> (Index,Color)
getCell cells n =  cells !! n

-- --This method finds all four neighnors (horizontal and vertical) of a given index
findNeighbors :: Cells -> (Index,Color) -> [(Index,Color)]
findNeighbors [] _  = []
findNeighbors cells index = let x = fst(fst(index))
                                y = snd(fst(index)) in if x > 0 && x < 5 && y > 0 && y < 7 then
                               [getCell cells ((x * 8) + (y-1))] ++ [getCell cells ((x * 8) + (y + 1))] ++ [getCell cells (((x-1) * 8) + y)] ++ [getCell cells ((x +1) * 8 + y)] else
                                 if (x == 0) then if (y == 7) then [getCell cells ((x*8) + (y-1))] ++ [getCell cells (((x +1) * 8) + y)] else  [getCell cells ((x * 8) + (y + 1))] ++ [getCell cells (((x + 1) * 8) + y)]
                                 else if (y == 7) then [getCell cells ((x * 8) + (y - 1))] ++ [getCell cells (((x - 1) * 8) + y)] else [getCell cells ((x * 8) + ( y + 1))] ++ [getCell cells (((x - 1) * 8) + y)]




buildComponent :: Cells -> (Index,Color) -> [(Index,Color)] -> [(Index,Color)] -> [(Index,Color)]
buildComponent cells index comp acc = concat(map
                                    (\x -> if (isInList index acc) == True then []
                                    else if isGoodNeighbors index x == True then (x:comp) ++ buildComponent cells x comp (index:acc)
                                                   else []) (findNeighbors cells index))

buildComponentTwo :: Cells -> (Index,Color) -> [(Index,Color)]
buildComponentTwo cells index = index : buildComponent cells index [] []


--This method return true if two neighbors have the same color, false otherwise
isGoodNeighbors :: (Index,Color) -> (Index,Color) -> Bool
isGoodNeighbors cell neighnor = snd(cell) == snd(neighnor)


isInList :: (Index,Color) -> [(Index,Color)] -> Bool
isInList elem [] = False
isInList elem list = if (fst(fst(list !! 0)) == fst(fst(elem)) && snd(fst(list !! 0)) == snd(fst(elem))) then True else isInList elem (tail list)

removeColor :: [(Index,Color)] -> [Index]
removeColor [] = []
removeColor (x:xs) = [fst x] ++ removeColor xs

removeDuplicatedCells :: [Index] -> [Index]
removeDuplicatedCells [] = []
removeDuplicatedCells (x:xs) = if x `elem` xs then removeDuplicatedCells xs else x : removeDuplicatedCells xs 

buildAllComponents :: Cells -> [[Index]]
buildAllComponents cells = map (\x -> removeDuplicatedCells(removeColor(buildComponentTwo cells x))) cells

findMaxComponent :: [[Index]] -> [Index]
findMaxComponent [] = []
findMaxComponent [x,[]] = x
findMaxComponent [x,y] = if length x >= length y then x else y
findMaxComponent (x:y:comps) = if length x >= length y then findMaxComponent (x:comps) else findMaxComponent (y:comps)  

getLCC :: Cells -> [Index]
getLCC cells = findMaxComponent(buildAllComponents cells)