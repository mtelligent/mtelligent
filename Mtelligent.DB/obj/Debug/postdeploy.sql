Select * from Cohorts where Type = 'Mtelligent.Entities.Cohorts.AllUsersCohort, Mtelligent.Entities';

if (@@ROWCOUNT = 0)
begin

Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values ('All Users', 'All users', 'Mtelligent.Entities.Cohorts.AllUsersCohort, Mtelligent.Entities', getDate(), 'System')

end

Select * from Cohorts where Type = 'Mtelligent.Entities.Cohorts.AuthenticatedUsersCohort, Mtelligent.Entities';

if (@@ROWCOUNT = 0)
begin

Insert into Cohorts (Name, SystemName, Type, Created, CreatedBy) Values ('Authenticated Users', 'Authenticated users', 'Mtelligent.Entities.Cohorts.AuthenticatedUsersCohort, Mtelligent.Entities', getDate(), 'System')

end

GO
