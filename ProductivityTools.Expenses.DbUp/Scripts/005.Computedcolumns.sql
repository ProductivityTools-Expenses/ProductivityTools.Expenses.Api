update [me].[expense] set amount=1 where amount is null
alter table me.expense alter column amount int not null
exec sp_rename 'me.expense.value', 'price', 'column';
alter table me.expense add [value] as amount*price


