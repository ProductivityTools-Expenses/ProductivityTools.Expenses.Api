
ALTER TABLE me.Expense drop column Cost
ALTER TABLE me.Expense drop column value

update [me].[Expense] set price=0 where price is null
ALTER TABLE me.Expense alter column price DECIMAL(7,2) not null
EXEC sp_rename 'me.Expense.price', 'Price', 'COLUMN';

ALTER TABLE me.Expense Add Cost as Amount*Price+Additions-Deductions
alter table me.expense add [Value] as Amount*Price
