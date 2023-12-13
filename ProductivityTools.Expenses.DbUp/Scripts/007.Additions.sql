update me.expense set Additions=0 where additions is null

ALTER TABLE me.Expense alter column Additions DECIMAL(7,2) not null