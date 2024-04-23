 insert into me.Category(BagId,Name)
 SELECT bc.BagId,c.Name
  FROM [PTExpenses].[me].[BagCategory] bc
  join me.Category  c on bc.CategoryId=c.CategoryID