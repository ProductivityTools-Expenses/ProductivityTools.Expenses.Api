
update e set e.CategoryID=n.newCategoryId
from me.TagGroupCategory e
inner join(
SELECT e.TagGroupCategoryId as TagGroupCategoryId
      ,e.[CategoryID] as oldCategoryId
	  ,c1.Name as Name
	  ,c2.CategoryID as newCategoryId
  FROM [PTExpenses].[me].TagGroupCategory e
  inner join me.Category c1 on e.CategoryID=c1.CategoryID
  inner join me.Category c2 on c1.Name=c2.Name 
  where c1.CategoryID!=c2.CategoryID
  and c1.BagId is null) n
  on n.TagGroupCategoryId=e.TagGroupCategoryId