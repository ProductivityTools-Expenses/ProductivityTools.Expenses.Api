update me.expense set Additions=0 where additions is null

ALTER TABLE me.Expense drop column Cost
ALTER TABLE me.Expense drop column value

ALTER TABLE me.Expense alter column Additions DECIMAL(7,2) not null

ALTER TABLE me.Expense Add Cost as Amount*Price+Additions-Deductions
alter table me.expense add [Value] as amount*price
