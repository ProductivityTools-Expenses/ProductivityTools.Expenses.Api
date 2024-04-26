update e set e.CategoryID=n.newCategoryId
from me.Expense e
inner join(
SELECT e.ExpenseID as expenseId
      ,e.[CategoryID] as oldCategoryId
      ,e.[BagID] as bagId
	  ,c1.Name as Name
	  ,c2.CategoryID as newCategoryId
  FROM [PTExpenses].[me].[Expense] e
  inner join me.Category c1 on e.CategoryID=c1.CategoryID
  inner join me.Category c2 on c1.Name=c2.Name 
  where c1.CategoryID!=c2.CategoryID
  and c2.BagId=e.BagID) n
  on n.expenseId=e.ExpenseID