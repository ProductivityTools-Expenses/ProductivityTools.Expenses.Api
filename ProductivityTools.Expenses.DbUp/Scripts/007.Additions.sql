update me.expense set Additions=0 where additions is null

ALTER TABLE me.Expense ADD Additions DECIMAL(7,2) not null