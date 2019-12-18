-- Tamir Sharon
-- 203999396

--Q1a
naturals :: [Int]
naturals = 1 : map (+1) naturals

--Q1b
squares :: [Int]
squares = map (\x -> x*x) naturals  

--Q1c
threes :: [Int]
threes = map (\x -> x*3) naturals

--Q1d
res :: [Int]
res = res' naturals squares threes

res' :: ([Int]) -> ([Int]) -> ([Int]) -> [Int]
res' (x:xs) (y:ys) (z:zs) = [x] ++ [y] ++ [z] ++ res' xs ys zs

--Q1h
switch :: [a] -> [a]
switch [] = []
switch (x:y:list) = [y] ++ [x] ++ switch list 


--Q2
data BinaryTree a = Nil | BNode a (BinaryTree a) (BinaryTree a) deriving Show

--Q2a
infTree :: a -> BinaryTree a
infTree a = BNode a (infTree a) (infTree a) 

--Q2b
treeMap :: (a -> b) -> BinaryTree a -> BinaryTree b
treeMap f Nil = Nil
treeMap f (BNode val left right) = BNode (f val) (treeMap f left) (treeMap f right)

--Q2c
type Depth = Int

treeTake :: Depth -> BinaryTree a -> BinaryTree a
treeTake 0 _ = Nil
treeTake n Nil = Nil
treeTake n (BNode val left right) = BNode val (treeTake (n-1) left) (treeTake (n-1) right)

--Q2d
treeSort :: BinaryTree t -> [t]
treeSort Nil = []
treeSort (BNode val l r) = treeSort l ++ [val] ++ treeSort r
