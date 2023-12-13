--UPDATE [me].[Expense] SET Amount=1 WHERE Amount IS NULL
--ALTER TABLE me.Expense ALTER COLUMN Amount int not null
--EXEC sp_rename 'me.Expense.Value', 'Price', 'COLUMN';
--ALTER TABLE me.Expense Add [Value] as Amount*Price


