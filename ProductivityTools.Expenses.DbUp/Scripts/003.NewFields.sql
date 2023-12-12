ALTER TABLE me.Expense ADD Amount INT
ALTER TABLE me.Expense ADD Additions DECIMAL(7,2)
ALTER TABLE me.Expense DROP COLUMN ValueAfterDiscount
EXEC sp_rename 'me.Expense.Discount', 'Deductions', 'COLUMN';