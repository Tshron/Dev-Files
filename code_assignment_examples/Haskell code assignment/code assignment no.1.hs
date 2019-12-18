-- Tamir Sharon
-- 203999396

import Data.Char

--Q1a
byteSub :: Char -> Char
byteSub 'a' = 'h'
byteSub 'g' = 'b'
byteSub 'z' = 'a'
byteSub letter = if ( (ord letter) > 97 && (ord letter) < 122 ) 
 then chr (ord letter + 1)
 else '0'

--Q1b
shiftIn :: Int -> [Char] -> [Char]
shiftIn num [] = []
shiftIn num xs =let len = length(xs) in drop (len-num `mod` len) xs ++ take (len-num `mod` len) xs

--Q1c
shiftRows :: [[Char]]->[[Char]]
shiftRows list = let len = length(list) in shiftRowsRec len len list

shiftRowsRec :: Int -> Int -> [[Char]] -> [[Char]]
shiftRowsRec 0 _ x = x
shiftRowsRec _ _ [] = []
shiftRowsRec n m (x:xs) = [shiftIn (n-m) x] ++ shiftRowsRec n (m-1) xs  

--Q1d
roundKey :: (Char->Char)->[[Char]]->[[Char]]
roundKey f [] = []
roundKey f (row:rows) = [map f row] ++ roundKey f rows

--Q1e
simpleAES :: (Char->Char)->[[Char]]->String
simpleAES f matrix = conca (roundKey f (shiftRows matrix))

-- this method convert the matrix's rows into String 
conca :: [[Char]] -> String
conca [] = []
conca (row:rows) = row ++ conca rows 

--Q2
data JValue = JStr String | JNum Double | JBool Bool |JNull | JObj [(String, JValue)] | JArray [JValue]

jsonToString :: JValue -> String
jsonToString (JStr text) = text
jsonToString (JNull ) = "null"
jsonToString (JBool True) = "true"
jsonToString (JBool False) = "false"
jsonToString (JNum num) = show num
jsonToString (JArray arr) = "[" ++  parseJsonArr (JArray arr) ++ "]"
jsonToString (JObj obj) = "{" ++ parseJsonObj (JObj obj) ++ "}"

--Parsing func for JArray type
parseJsonArr :: JValue -> String
parseJsonArr (JArray []) = []
parseJsonArr (JArray [x]) =  jsonToString x 
-- in order to prevent double "curly brackets" when handling JObj inside JArray 
parseJsonArr (JArray (JObj obj:arr)) = jsonToString (JObj obj) ++ "," ++ parseJsonArr (JArray arr) 
parseJsonArr (JArray (el:arr)) =  jsonToString el ++ "," ++ parseJsonArr (JArray arr)

--Parsing func for JObj type
parseJsonObj :: JValue -> String
parseJsonObj (JObj []) = []
parseJsonObj (JObj [x]) = fst x ++ ": " ++ jsonToString (snd x)
parseJsonObj (JObj (pair:pairs)) = fst pair ++ ": " ++ jsonToString (snd pair) ++ ", " ++ parseJsonObj (JObj pairs)



--Q3a
myReverse :: [a] -> [a]
myReverse = foldr (\x xs -> xs ++ [x]) [] 

--Q3bf
myMap:: (a->b) -> [a]->[b]
myMap f = foldr ((:).f) []

--Q3c
myNegate :: [Int] -> [Int]
myNegate list = map (\x -> if abs x == x then negate x else x) list 


--Q4
setElement :: Int -> a -> [a] -> [a]
setElement n x list = if n > (length(list)-1) || n < 0 then list
else take n list ++ [x] ++ drop (n+1) list

setElements :: [(Int, a)] -> [a] -> [a]
setElements pairs list = foldl (flip(\(n, x) -> setElement n x)) list pairs
